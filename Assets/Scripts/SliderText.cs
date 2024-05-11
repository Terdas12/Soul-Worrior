using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private void Start()
    {
        OnSliderValueChanged();
    }

    public void OnSliderValueChanged()
    {
        _textMeshPro.text = $"{_slider.value}/{_slider.maxValue}";
    }
}
