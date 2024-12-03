using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Chalk : MonoBehaviour
{
    private Image _image;
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CardGameManager.Instance.CurrentPhase == Phase.TargetMode)
        {
            _image.color = Color.red;
        }
        else
            _image.color = Color.white;
    }
}
