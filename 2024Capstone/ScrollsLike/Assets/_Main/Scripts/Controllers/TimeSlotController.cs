using System.Collections;
using System;
using UnityEngine;
using TMPro;
public class TimeSlotController : MonoBehaviour
{
    [SerializeField] private TimeSlot[] _timeSlots = new TimeSlot[4];
    [SerializeField] private TextMeshProUGUI[] _enemyText = new TextMeshProUGUI[4];
    private bool _isPlaying = false; //prevents Resolve() from continuing throught the loop until the current effect is finished Playing

    private TimeSlot _activeSlot;
    private void Awake()
    {
        CardGameManager.Instance.Events.PlayCard.AddListener(AddCardToTimeSlot);
        CardGameManager.Instance.Events.PlayPhaseEndEvent.AddListener(StartPlayPhase);
        CardGameManager.Instance.Events.PrepPhaseEndEvent.AddListener(AddEnemyToTimeSlots);
        CardGameManager.Instance.Events.ResolutionPhaseEndEvent.AddListener(ResolveEffects);
        CardGameManager.Instance.Events.EffectEnded.AddListener(CardResolved);
        CardGameManager.Instance.Events.CleanupPhaseEndEvent.AddListener(ClearSlots);
        //CardGameManager.Instance.Events.MoveToNextSlot.AddListener();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //clears board during cleanup phase
    public void ClearSlots()
    {
        CardGameManager.Instance.DrawPhaseStart();
    }
    public void CardResolved()
    {
        _isPlaying = false;
    }
    public void ResolveEffects()
    {
        StartCoroutine(Resolve());
    }

    //adds a card to the earliest empty slot
    public void AddCardToTimeSlot(GameCard card)
    {
        _activeSlot.AddCard(card);
    }

    public void AddEnemyToTimeSlots()
    {
        // StartCoroutine(EnemyEffects());
        CardGameManager.Instance.PlayPhaseStart();
    }

    public void StartPlayPhase()
    {
        StartCoroutine(PlayPhase());
    }

    //adds enemy abilities to each timeslot
    IEnumerator EnemyEffects()
    {
        int loop = 0;
        foreach(TimeSlot slot in _timeSlots)
        {
            slot.AddEnemyEffect(EnemyManager.Instance.PlayAbility());
            _enemyText[loop].text = $"Enemy is {slot.EnemyCard.CardType.ToString()}ing";
            yield return new WaitForSeconds(2);
            loop++; 
        }
        CardGameManager.Instance.PlayPhaseStart();
        yield return null;
    }

    //resolves the effects of the played cards in each timeslot
    IEnumerator Resolve()
    {
        foreach(TimeSlot slot in _timeSlots)
        {
            _isPlaying = true;
            slot.ResolvePlayerEffects();
            yield return new WaitUntil(() => _isPlaying == false);
            slot.ResolveEnemyEffects();
        }
       
        CardGameManager.Instance.CleanupPhaseStart();
        yield return null;
    }

    IEnumerator PlayPhase()
    {
        var trigger = false;
        Action action = () => trigger = true;
        CardGameManager.Instance.Events.MoveToNextSlot.AddListener(action.Invoke);
        foreach(TimeSlot slot in _timeSlots)
        {
            _activeSlot = slot;
            yield return new WaitUntil(() => trigger);
        }
        
        CardGameManager.Instance.Events.MoveToNextSlot.RemoveListener(action.Invoke);
        yield return null;
    }

    
}
