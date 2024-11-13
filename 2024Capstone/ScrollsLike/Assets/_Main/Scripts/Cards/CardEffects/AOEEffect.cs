using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AOE Effect", menuName = "Effects/AOE Effect")]
public class AOEEffect : CardEffect
{
    public int Damage
    {
        get
        {
            return _baseDamage;// + Modifier;
        }
    }
    [SerializeField] private int _baseDamage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
