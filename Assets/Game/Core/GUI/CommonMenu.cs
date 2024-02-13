using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommonMenu : MenuBase
{
    [SerializeField]
    private TextMeshProUGUI _coinsTMP;

    private void OnEnable()
    {
        GameMode.Instance.OnCoinsChange += OnCoinsChange;
    }
    private void OnDisable()
    {
        GameMode.Instance.OnCoinsChange -= OnCoinsChange;

    }

    private void OnCoinsChange(int count)
    {
        _coinsTMP.text = count.ToString();
    }
}
