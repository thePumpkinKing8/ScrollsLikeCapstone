using System.Collections;
using System;
using UnityEngine;
using TMPro;
public class TimeSlotController : MonoBehaviour
{
    [SerializeField] private TimeSlot[] _timeSlots = new TimeSlot[4];
    [SerializeField] private TextMeshProUGUI[] _enemyText = new TextMeshProUGUI[4];
  

    private void Awake()
    { 
        CardGameManager.Instance.Events.PrepPhaseEndEvent.AddListener(AddEnemyToTimeSlots);
       // CardGameManager.Instance.Events.ResolutionPhaseEndEvent.AddListener(ResolveEffects);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ResolveEffects()
    {
        //StartCoroutine(Resolve());
    }

    //adds a card to the earliest empty slot

    public void AddEnemyToTimeSlots()
    {
        // StartCoroutine(EnemyEffects());
        CardGameManager.Instance.PlayPhaseStart();
    }

    //adds enemy abilities to each timeslot
    IEnumerator EnemyEffects()
    {
        int loop = 0;
        foreach(TimeSlot slot in _timeSlots)
        {
            slot.AddEnemyEffect(EnemyManager.Instance.PlayAbility());
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
        }
       
        CardGameManager.Instance.CleanupPhaseStart();
        yield return null;
    }

  
}
