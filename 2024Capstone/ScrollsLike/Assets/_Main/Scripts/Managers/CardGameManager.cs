using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class CardGameManager : Singleton<CardGameManager>
{
    public Phase CurrentPhase { get; private set; }
    public CardEventData Events { get { return _cardEventData; } }
    [SerializeField] private CardEventData _cardEventData;
    [SerializeField] private DiscardPile _discardPile;
    [SerializeField] private DeckManager _deckManager;

    //play phase functions
    private int _timeSlotIndex;
    [SerializeField] private TimeSlot[] _timeSlots = new TimeSlot[4];
    public TimeSlot[] EnemySlot { get { return _timeSlots; } }

    public float DrawDelay
    {
        get { return _drawDelay; }
    }
    [Tooltip("time between cards drawn to hand")][SerializeField] private float _drawDelay = 0.5f;
    public TimeSlot EffectTarget { get; private set; }
    private bool _waitForTarget = false;

    protected override void Awake()
    {
        base.Awake();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    #region EventFunctions  
    
    public void DrawPhaseStart()
    {
        CurrentPhase = Phase.DrawPhase;
        DrawPhaseEnd();
    }
   
    public void DrawPhaseEnd()
    {
        DrawForTurn();
        Events.DrawPhaseEndEvent.Invoke();
    }
   
    public void PrepPhaseStart()
    {
        CurrentPhase = Phase.PrepPhase;
        Debug.Log("prep phase");
        EnemyDraw();
    }
    
    public void PrepPhaseEnd() => Events.PrepPhaseEndEvent.Invoke();
    public void PlayPhaseStart()
    {
        CurrentPhase = Phase.PlayPhase;
        Debug.Log("play phase");
        PlayPhaseEnd();
    }
    public void PlayPhaseEnd() => Events.PlayPhaseEndEvent.Invoke();
    public void ResolutionPhaseStart() //trigger any effects waiting for this phase 
    {
        CurrentPhase = Phase.ResolutionPhase;
        StartCoroutine(EnemyTurn());   
    }
    public void ResolutionPhaseEnd()
    {
        CleanupPhaseStart();
    }
    public void CleanupPhaseStart()
    {
        CurrentPhase = Phase.CleanupPhase;
        Events.CleanupPhaseStartEvent.Invoke();
        CleanupPhaseEnd();
    }
    public void CleanupPhaseEnd()
    {
        HealthManager.Instance.ChangeEnergy(-HealthManager.Instance.Energy);
        Events.CleanupPhaseEndEvent.Invoke();
        DrawPhaseStart();
    }
    #endregion
    #region EffectHandlers
    
    public void EffectPermission() => Events.EffectManagerPermission.Invoke();
    public void EffectDone()
    {
        Events.EffectEnded.Invoke();
    }
    public void WaitForEffects(Phase currentPhase = default)
    {
        currentPhase = currentPhase == default ? CurrentPhase : currentPhase;
        CurrentPhase = Phase.EffectMode;
        if (EffectManager.Instance.GetPermission())
        {
            CurrentPhase = currentPhase;
        }
        else
        {
            Action action = () => CurrentPhase = currentPhase;
            StartCoroutine(WaitForEffectsManager(action));
        }
            
        
    }

    IEnumerator WaitForEffectsManager(Action function)
    {
        var trigger = false;
        Action action = () => trigger = true;
        Events.EffectManagerPermission.AddListener(action.Invoke);
        CurrentPhase = Phase.EffectMode;
        yield return new WaitUntil(() => trigger);
        Debug.Log("end wait");
        function();
    }

    public void FunctionWait(Action action)
    {
        StartCoroutine(WaitForEffectsManager(action));
    }
    
    #endregion

    private void Start()
    {
        GameManager.Instance.CardGameStart();
        Invoke("GameStart", 1);
    }

    //wait for all scripts to get their start and awake functions finished before starting the game
    private void GameStart()
    {
        HandController.Instance.GameStart();
        SetUpEnemy();
    }

    public void SetUpEnemy()
    {
        foreach(TimeSlot slot in _timeSlots)
        {
            slot.SetUp(EnemyManager.Instance.EnemyHealth);
        }
    }

    #region DrawPhase
    public void DrawForTurn()
    {
        HandController.Instance.DrawPhase();
    }


    #endregion

    #region PrepPhase
    public void EnemyDraw()
    {
        StartCoroutine(SetEnemyCards());
    }

    IEnumerator SetEnemyCards()
    {
        foreach (TimeSlot slot in _timeSlots)
        {
            if (slot.Active)
            {
                slot.AddEnemyEffect(EnemyManager.Instance.PlayAbility());
                yield return new WaitForSeconds(_drawDelay);
            }                
        }
        PrepPhaseEnd();
        PlayPhaseStart();
        yield return null;
    }

    #endregion 

    #region PlayPhase
    public void PlayCard(GameCard card)
    {
        if(CurrentPhase != Phase.PlayPhase)
            return;

        #region CardTypeEventCalls
        if(card.CardsType == CardType.Strike)
        {
            Events.AttackPlayed.Invoke();
        }
        
        #endregion
        //go to target mode if needed
        foreach (CardEffect effect in card.ReferenceCardData.CardResolutionEffects)
        {
            if(effect.RequiresTarget)
            {
                Debug.Log("requires target");
                StartCoroutine(WaitForTargetSelect(card));
                return;
            }
        }
        EffectManager.Instance.ActivateEffect(card.ReferenceCardData.CardResolutionEffects);
        HandController.Instance.RemoveCard(card);
        if (card.CardsType != CardType.Stance)
        {
            DiscardCard(card.ReferenceCardData);
        }
        card.OnDeSpawn();
    }

    public void DiscardForEnergy(GameCard card)
    {
        DiscardCard(card.ReferenceCardData);
        HealthManager.Instance.ChangeEnergy(1);
        HandController.Instance.RemoveCard(card);        
    }

    public void EndTurn()
    {
        if(CurrentPhase == Phase.PlayPhase)
        {
            PlayPhaseEnd();
            ResolutionPhaseStart();
        }
        
    }

    IEnumerator WaitForTargetSelect(GameCard card)
    {
        CurrentPhase = Phase.TargetMode;
        _waitForTarget = true;
        yield return new WaitUntil(() => _waitForTarget == false);
        Debug.Log("target selected");
        EffectManager.Instance.ActivateEffect(card.ReferenceCardData.CardResolutionEffects, EffectTarget);
        DiscardCard(card.ReferenceCardData);
        card.OnDeSpawn();
        CurrentPhase = Phase.PlayPhase;
        yield return null;
    }

    public void SetTarget(TimeSlot target)
    {
        EffectTarget = target;
        _waitForTarget = false;
    }
    

    #endregion

    #region Resolution Phase


    IEnumerator EnemyTurn()
    {
        var trigger = false;
        Action action = () => trigger = true;
        Events.EffectManagerPermission.AddListener(action.Invoke);
        Debug.Log("EnemyTurn");
        foreach(TimeSlot slot in _timeSlots)
        {
            if(slot.Active)
            {
                yield return new WaitForSeconds(1);
                if(EffectManager.Instance.GetPermission())
                    slot.ResolveEnemyEffect();
                else
                {
                    yield return new WaitUntil(() => trigger == true);
                }
                yield return new WaitUntil(() => trigger == true);
                slot.ClearSlot();
                yield return new WaitForSeconds(DrawDelay);
            }
        }
        if(EffectManager.Instance.GetPermission() == false)
        {
            Debug.Log("wait");
            FunctionWait(() => ResolutionPhaseEnd());
        }
        else
        {
            Debug.Log("no wait");
            ResolutionPhaseEnd();
        }
        
        yield return null;
    }

    #endregion

    #region AdditionalEnemyLogic

    #endregion

    #region OtherGameFunctions
    public void HandleShuffleToDeck(List<CardData> cards)
    {
        _deckManager.ShuffleCardsIn(_discardPile.DiscardedCards);
        _discardPile.ShuffleCardsToDeck();
    }

    public void DiscardCard(CardData card)
    {
        _discardPile.AddCard(card);
    }

    public void DrawCard(int num = 1)
    {
        for(int i = 0; i < num; i++)
        {
            CardData card;
            card = _deckManager.DrawCard();
            HandController.Instance.CardDrawn(card);
        }
    }

    public void DrawFromDeckFailed() //shuffles discard pile into deck if there are no cards to draw from
    {
        _deckManager.ShuffleCardsIn(_discardPile.DiscardedCards);
        _discardPile.ShuffleCardsToDeck();
    }
    #endregion
}

public enum Phase
{
    Default,
    DrawPhase,
    PrepPhase,
    PlayPhase,
    ResolutionPhase,
    CleanupPhase,
    //below arent phases but break points in the middle of phases so the game wont continue until an action is taken
    TargetMode,
    EffectMode   
}
