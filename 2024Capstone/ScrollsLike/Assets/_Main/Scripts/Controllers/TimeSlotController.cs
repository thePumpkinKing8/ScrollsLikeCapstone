using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlotController : MonoBehaviour
{
    [SerializeField] private TimeSlot[] _timeSlots = new TimeSlot[4];
    private void Awake()
    {
        CardGameManager.Instance.Events.PlayCard.AddListener(AddCardToTimeSlot);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
