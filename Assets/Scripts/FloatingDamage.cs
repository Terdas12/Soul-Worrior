using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingDamage : MonoBehaviour
{
    [HideInInspector] public int damage;
    [SerializeField] private TextMeshPro _textMeshPro;

    private void Start()
    {
        _textMeshPro = GetComponent<TextMeshPro>();
        _textMeshPro.text = "-" + damage;
    }

    public void OnAnimatorOver()
    {
        Destroy(gameObject);
    }
}
