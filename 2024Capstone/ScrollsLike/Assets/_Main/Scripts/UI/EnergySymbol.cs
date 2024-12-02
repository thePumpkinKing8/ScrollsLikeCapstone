using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergySymbol : MonoBehaviour
{
    [SerializeField] private Sprite _e0;
    [SerializeField] private Sprite _e1;
    [SerializeField] private Sprite _e2;
    [SerializeField] private Sprite _e3;
    private TextMeshProUGUI _text;
    private Image _image;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _image = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = HealthManager.Instance.Energy.ToString();
        PoolObject eff;
        switch (HealthManager.Instance.Energy)
        {          
            case 0:
                _image.sprite = _e0;
                break;
            case 1:
                if(_image.sprite != _e1)
                {
                    _image.sprite = _e1;
                    eff = PoolManager.Instance.Spawn("Energy");
                    eff.transform.parent = transform;
                    eff.transform.position = transform.position;
                }              
                break;
            case 2:
                if (_image.sprite != _e2)
                {
                    _image.sprite = _e2;
                    eff = PoolManager.Instance.Spawn("Energy");
                    eff.transform.parent = transform;
                    eff.transform.position = transform.position;
                }
                break;
            case >= 3:
                if (_image.sprite != _e3)
                {
                    _image.sprite = _e3;
                    eff = PoolManager.Instance.Spawn("Energy");
                    eff.transform.parent = transform;
                    eff.transform.position = transform.position;
                }
                break;
        }
    }
}
