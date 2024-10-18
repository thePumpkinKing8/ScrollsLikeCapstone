using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CreateAssetMenu(fileName = "Base Effect", menuName = "Effects/Base Effect")]
public class CardEffect : ScriptableObject
{
    protected CardData _cardsData;
    public void GetData(CardData data)
    {
        _cardsData = data;
    }
    public virtual void Effect() 
    {
        Debug.Log("card ability activates");    
    }
    

}
