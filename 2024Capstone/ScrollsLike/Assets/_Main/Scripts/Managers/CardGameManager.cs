using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

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
    public void HandleCardDraw(CardData card) => HandController.Instance.CardDrawn(card);//remove
    public void HandleCardDiscard(CardData card) => _discardPile.AddCard(card);//remove

    public void HandleShuffleToDeck(List<CardData> cards)
    {
        _deckManager.ShuffleCardsIn(_discardPile.DiscardedCards);
        _discardPile.ShuffleCardsToDeck();
    }    
    public void DrawCard() => _deckManager.DrawCard();//remove
    public void AddCardToHand(GameCard card) => HandController.Instance.AddCard(card);//remove

    
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
        PrepPhaseEnd();
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
    }
    public void CleanupPhaseEnd()
    {
        foreach(TimeSlot slot in _timeSlots)
        {
            //slot.ClearSlot();
        }
        HealthManager.Instance.ChangeEnergy(-HealthManager.Instance.Energy);
        Events.CleanupPhaseEndEvent.Invoke();
        DrawPhaseStart();
    }

    #region effectHandlers
    public void EffectDone()
    {
        Events.EffectEnded.Invoke();
    }
    IEnumerator WaitForEffects(Phase currentPhase = default)
    {
        currentPhase = currentPhase == default ? CurrentPhase : currentPhase;
        var trigger = false;
        Action action = () => trigger = true;
        Events.EffectEnded.AddListener(action.Invoke);
        CurrentPhase = Phase.EffectMode;
        yield return new WaitUntil(() => trigger);
        CurrentPhase = currentPhase;
        yield return null;
    }

    public void Wait(Phase waitingPhase)
    {
        StartCoroutine(WaitForEffects());
    }
    #endregion
    /*
     public void PlayerHit() => Events.PlayerHit.Invoke(damage);// event should be only for trigger animation and sfx
     public void EnemyHit() => Events.EnemyHit.Invoke(damage);//. event should be only for trigger animation and sfx
     public void EnemyHeal() => Events.EnemyHeal.Invoke(heal);// event should be only for trigger animation and sfx
     public void StanceResolved() => Events.StanceResolved.Invoke(data);//event should be only for trigger animation and sfx
     public void PlayerBlock() => Events.PlayerGainsBlock.Invoke(block);//event should be only for trigger animation and sfx
     */
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

    #region DrawPhase/Card Draw
    public void DrawForTurn()
    {
        HandController.Instance.DrawPhase();
    }

    public void DrawFromDeckFailed()
    {
        _deckManager.ShuffleCardsIn(_discardPile.DiscardedCards);
     //   _discardPile.
    }

    public void DiscardCard(CardData card)
    {
        _discardPile.AddCard(card);
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
        //go to target mode if needed
        foreach(CardEffect effect in card.ReferenceCardData.CardResolutionEffects)
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
        HandleCardDiscard(card.ReferenceCardData);
        card.OnDeSpawn();
    }

    public void DiscardForEnergy(GameCard card)
    {
        HealthManager.Instance.ChangeEnergy(card.EnergyCost);
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
        HandleCardDiscard(card.ReferenceCardData);
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
        Events.EffectEnded.AddListener(action.Invoke);
        Debug.Log("EnemyTurn");
        foreach(TimeSlot slot in _timeSlots)
        {
            if(slot.Active)
            {
                slot.ResolveEnemyEffect();
                yield return new WaitUntil(() => trigger);
                slot.ClearSlot();
                yield return new WaitForSeconds(DrawDelay);
            }
        }
        ResolutionPhaseEnd();
        yield return null;
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
