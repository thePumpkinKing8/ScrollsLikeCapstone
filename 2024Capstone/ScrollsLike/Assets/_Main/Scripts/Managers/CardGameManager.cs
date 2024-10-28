using System.Collections;
using System.Collections.Generic;
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
    protected override void Awake()
    {
        base.Awake();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    #region EventFunctions
    public void HandleCardDraw(CardData card) => Events.CardDrawnEvent.Invoke(card);//remove
    public void HandleCardDiscard(CardData card) => Events.CardDiscardedEvent.Invoke(card);//remove
  
    public void HandleShuffleToDeck(List<CardData> cards) => Events.ShuffleCardsToDeck.Invoke(cards);//remove
    public void DrawCard() => Events.DrawCardEvent.Invoke();//remove
    public void AddCardToHand(GameCard card) => Events.AddCardToHand.Invoke(card);//remove

    public void GameStart() => Events.GameStartEvent.Invoke();
    public void DrawPhaseStart()
    {
        CurrentPhase = Phase.DrawPhase;
        Events.DrawPhaseStartEvent.Invoke();
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
        SetUpEnemy();
        Events.PrepPhaseStartEvent.Invoke();
    }
    
    public void PrepPhaseEnd() => Events.PrepPhaseEndEvent.Invoke();
    public void PlayPhaseStart()
    {
        CurrentPhase = Phase.PlayPhase;
        _timeSlotIndex = 0;
        Debug.Log("play phase");
        Events.PlayPhaseStartEvent.Invoke();
    }
    public void PlayPhaseEnd() => Events.PlayPhaseEndEvent.Invoke();
    public void ResolutionPhaseStart() //trigger any effects waiting for this phase 
    {
        CurrentPhase = Phase.ResolutionPhase;
        _timeSlotIndex = 0;
        Events.ResolutionPhaseStartEvent.Invoke();        
    }
    public void ResolutionPhaseEnd()
    {
        ResolveSlot();
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
            slot.CleanUpPhase();
        }
        Events.CleanupPhaseEndEvent.Invoke();
        DrawPhaseStart();
    }
    
    public void PlayCardEvent(GameCard  card) => Events.PlayCard.Invoke(card);//remove
    public void EffectActivate(List<CardEffect> effects) => Events.EffectPlayed.Invoke(effects);//remove
    public void EffectDone() => Events.EffectEnded.Invoke();//remove?
   
    public void PlayerHit(int damage) => Events.PlayerHit.Invoke(damage);//remove int variable. event should be only for trigger animation and sfx
    public void EnemyHit(int damage) => Events.EnemyHit.Invoke(damage);//remove int variable. event should be only for trigger animation and sfx
    public void EnemyHeal(int heal) => Events.EnemyHeal.Invoke(heal);//remove int variable. event should be only for trigger animation and sfx
    public void StanceResolved(CardData data) => Events.StanceResolved.Invoke(data);//remove
    public void PlayerBlock(int block) => Events.PlayerGainsBlock.Invoke(block);//remove int variable. event should be only for trigger animation and sfx
    public void EnergyChange(int energy) => Events.EnergyChange.Invoke(energy);//remove int variable. event should be only for trigger animation and sfx
    #endregion

    private void Start()
    {
        GameManager.Instance.CardGameStart();
        Invoke("LateStart", 1);
    }

    //wait for all scripts to get their start and awake functions finished before starting the game
    private void LateStart()
    {
        GameStart();
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
    public void SetUpEnemy()
    {
        foreach(TimeSlot slot in _timeSlots)
        {
            slot.AddEnemyEffect(EnemyManager.Instance.PlayAbility());
        }
        PrepPhaseEnd();
        PlayPhaseStart();
    }

    #endregion 

    #region PlayPhase
    public void PlayCard(GameCard card)
    {
        if(CurrentPhase != Phase.PlayPhase)
            return;

        _timeSlots[_timeSlotIndex].AddCard(card);
        HandController.Instance.RemoveCard(card);
    }

    public void DiscardForEnergy(GameCard card)
    {
        HealthManager.Instance.ChangeEnergy(card.EnergyCost);
        HandController.Instance.RemoveCard(card);        
    }

    public void MoveToNextSlot()
    {
        if (CurrentPhase != Phase.PlayPhase)
            return;

        if(_timeSlotIndex >= _timeSlots.Length - 1)
        {
            ResolutionPhaseStart();
            return;
        }
        MoveToNext();
    }

    #endregion

    #region Resolution Phase

    public void ResolveSlot()
    {
        if(_timeSlotIndex >= _timeSlots.Length - 1)
        {
            CleanupPhaseStart();
            return;
        }
        _timeSlots[_timeSlotIndex].ResolvePlayerEffects();
    }


    public void MoveToNext()
    {
        _timeSlotIndex++;
    }
    #endregion
}

public enum Phase
{
    DrawPhase,
    PrepPhase,
    PlayPhase,
    ResolutionPhase,
    CleanupPhase
}
