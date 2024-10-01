using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlotController : MonoBehaviour
{
    [SerializeField] private TimeSlot[] _timeSlots = new TimeSlot[4];
    private bool _isPlaying = false;
    private void Awake()
    {
        CardGameManager.Instance.Events.PlayCard.AddListener(AddCardToTimeSlot);
        CardGameManager.Instance.Events.PrepPhaseEndEvent.AddListener(AddEnemyToTimeSlots);
        CardGameManager.Instance.Events.ResolutionPhaseEndEvent.AddListener(ResolveEffects);
        CardGameManager.Instance.Events.EffectEnded.AddListener(CardResolved);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CardResolved()
    {
        _isPlaying = false;
    }
    public void ResolveEffects()
    {
        StartCoroutine(Resolve());
    }

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
        StartCoroutine(EnemyEffects());
    }

    IEnumerator EnemyEffects()
    {
        foreach(TimeSlot slot in _timeSlots)
        {
            slot.AddEnemyEffect(EnemyManager.Instance.PlayAbility());
            yield return new WaitForSeconds(2);
        }
        CardGameManager.Instance.PlayPhaseStart();
        yield return null;
    }

    IEnumerator Resolve()
    {
        foreach(TimeSlot slot in _timeSlots)
        {
            _isPlaying = true;
            slot.ResolvePlayerEffects();
            yield return new WaitUntil(() => _isPlaying == false);   
        }
        CardGameManager.Instance.CleanupPhaseStart();
        yield return null;
    }
}
