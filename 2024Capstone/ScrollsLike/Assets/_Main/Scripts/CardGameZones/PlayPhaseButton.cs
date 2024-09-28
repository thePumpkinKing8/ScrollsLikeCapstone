using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPhaseButton : MonoBehaviour
{
    private void Update()
    {
        if(CardGameManager.Instance.CurrentPhase == Phase.PlayPhase)
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }

    public void Clicked()
    {
        CardGameManager.Instance.ResolutionPhaseStart();
    }
}
