using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MenuBase
{
    [SerializeField]
    private Button _backBtn;

    private void Start()
    {
        _backBtn.onClick.AddListener(GameMode.Instance.MainMenuGame);
    }
}
