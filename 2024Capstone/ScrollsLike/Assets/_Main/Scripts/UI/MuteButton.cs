using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MuteButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _image;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if(BlakesAudioManager.Instance.Muted)
        {
            _image.color = Color.white;
            _text.text = "OFF";
        }
        else
        {
            _image.color = Color.green;
            _text.text = "ON";
        }
    }

    public void OnClick()
    {
        if (!BlakesAudioManager.Instance.Muted)
        {
            _image.color = Color.white;
            _text.text = "OFF";
            BlakesAudioManager.Instance.Mute();
        }
        else
        {
            _image.color = Color.green;
            _text.text = "ON";
            BlakesAudioManager.Instance.UnMute();
        }
    }
}
