using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsAnimation : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _animationTime;
    private void OnEnable()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.DOFade(1, _animationTime);
    }

    public void Disable()
    {
        StartCoroutine(AwaitToDisable());
    }

    private IEnumerator AwaitToDisable()
    {
        _canvasGroup.DOFade(0, _animationTime);
        yield return new WaitForSeconds(_animationTime);
        gameObject.SetActive(false);
    }
}
