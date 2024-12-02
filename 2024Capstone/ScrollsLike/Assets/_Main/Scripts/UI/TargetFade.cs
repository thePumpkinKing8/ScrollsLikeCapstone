using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TargetFade : MonoBehaviour
{
    private Image _image;
    public bool IsCard { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        IsCard = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CardGameManager.Instance.CurrentPhase == Phase.TargetMode)
        {
            if(!IsCard)
            {
                _image.color = Color.gray;
            }
            
        }
        else
        {
            _image.color = Color.white;
            IsCard = false;
        }
            
    }
}
