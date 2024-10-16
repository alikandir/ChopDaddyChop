using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class ComboController : MonoBehaviour
{
    private int _currentCombo = 0;
    public int CurrentCombo {get => _currentCombo;
        set 
        { 
            _currentCombo = value;
            UpdateComboUI();
        }  
    }
    [SerializeField] private TextMeshProUGUI _comboText;
    [SerializeField] private RectTransform _comboMeter;
    [SerializeField] private Image _comboImage;
    private void Start()
    {
        _comboImage.color = Color.cyan;
    }
    private void UpdateComboUI()
    {
        _comboText.text=_currentCombo.ToString()+"X Combo";
        if (_currentCombo > 0) ShowComboUI();
        else if (_currentCombo <= 0) HideComboUI();

        if (_currentCombo >= 6)
        {
            _comboMeter.DOShakeAnchorPos(1, 20, 5);
            _comboImage.color = Color.red;
        }
        
        else if (_currentCombo >= 3)
        {
            _comboMeter.DOShakeAnchorPos(1, 10, 5);
            _comboImage.color = Color.cyan;
        }
    }
    private void ShowComboUI() => _comboMeter.gameObject.SetActive(true);
    
    private void HideComboUI() => _comboMeter.gameObject.SetActive(false);
    
}
