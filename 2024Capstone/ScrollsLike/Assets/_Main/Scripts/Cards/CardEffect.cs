using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CreateAssetMenu(fileName = "Base Effect", menuName = "Effects/Base Effect")]
public class CardEffect : ScriptableObject
{

    public virtual void Effect() 
    {
        Debug.Log("card ability activates");    
    }
    

}
