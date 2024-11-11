using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Base Effect", menuName = "Effects/Base Effect")]
public class CardEffect : ScriptableObject
{
    protected CardData _cardsData;

    [SerializeField] private bool _requireTarget;
    public bool RequiresTarget { get { return _requireTarget; } }
    
    public void GetData(CardData data)
    {
        _cardsData = data;
    }
    public virtual void Effect(TimeSlot target = null) 
    {
        Debug.Log("card ability activates");    
    }
    

}
