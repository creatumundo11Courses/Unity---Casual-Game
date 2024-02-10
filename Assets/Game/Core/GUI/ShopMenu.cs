using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MenuBase
{
    [SerializeField]
    private Button _backBtn;
    [SerializeField]
    private Button _defaultPlayerBtn;
    [SerializeField]
    private Button _player2PlayerBtn;
    [SerializeField]
    private Button _200CoinsBtn;
    [SerializeField]
    private Button _1000CoinsBtn;
    [SerializeField]
    private Button _NoAdsBtn;
   

    private void Start()
    {
        _backBtn.onClick.AddListener(GameMode.Instance.MainMenuGame);
        _200CoinsBtn.onClick.AddListener(IAPManager.Instance.Purchase200Coins);
        _1000CoinsBtn.onClick.AddListener(IAPManager.Instance.Purchase1000Coins);
        _NoAdsBtn.onClick.AddListener(IAPManager.Instance.PurchaseNoAds);
    }
}
