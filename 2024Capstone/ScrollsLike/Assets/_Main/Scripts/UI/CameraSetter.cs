using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetter : MonoBehaviour
{
    //couldnt be bothered to set the screen space camera any other way
    void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

}
