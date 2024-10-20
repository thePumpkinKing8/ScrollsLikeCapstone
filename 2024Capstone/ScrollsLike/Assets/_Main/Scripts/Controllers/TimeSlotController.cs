using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimeSlotController : MonoBehaviour
{
    [SerializeField] private TimeSlot[] _timeSlots = new TimeSlot[4];
    [SerializeField] private TextMeshProUGUI[] _enemyText = new TextMeshProUGUI[4];
    private bool _isPlaying = false; //prevents Resolve() from continuing throught the loop until the current effect is finished Playing
    private void Awake()
    {
        CardGameManager.Instance.Events.PlayCard.AddListener(AddCardToTimeSlot);
        CardGameManager.Instance.Events.PlayPhaseEndEvent.AddListener(StartPlayPhase);
        CardGameManager.Instance.Events.PrepPhaseEndEvent.AddListener(AddEnemyToTimeSlots);
        CardGameManager.Instance.Events.ResolutionPhaseEndEvent.AddListener(ResolveEffects);
        CardGameManager.Instance.Events.EffectEnded.AddListener(CardResolved);
        CardGameManager.Instance.Events.CleanupPhaseEndEvent.AddListener(ClearSlots);
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
        foreach(TextMeshProUGUI text in _enemyText)
        {
            text.text = "";
        }

        foreach(TimeSlot slot in _timeSlots)
        {
            CardGameManager.Instance.HandleCardDiscard(slot.PlayersCard.ReferenceCardData);
            slot.DiscardCard();   
        }
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
        foreach(TimeSlot slot in _timeSlots)
        {
            if(slot.PlayersCard == null)
            {
                slot.AddCard(card);
                return;
            }
        }
        Debug.Log("no open slots");
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
        yield return null;
    }
}
