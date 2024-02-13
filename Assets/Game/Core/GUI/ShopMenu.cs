using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private Button _noAdsBtn;
   

    private void Start()
    {
        _backBtn.onClick.AddListener(GameMode.Instance.MainMenuGame);
#if COURSE_SERVICES_ADS && COURSE_SERVICES_IAP
        string adsPriceStr = IAPManager.Instance.GetAdsPrice();
        string coins200PriceStr = IAPManager.Instance.Get200CoinsPrice();
        string coins1000PriceStr = IAPManager.Instance.Get1000CoinsPrice();
        
        //ADS ***

        _noAdsBtn.interactable = AdsManager.Instance.IsAdsEnabled;
        _noAdsBtn.onClick.AddListener(IAPManager.Instance.PurchaseNoAds);
        _noAdsBtn.GetComponentInChildren<TextMeshProUGUI>().text = adsPriceStr;
        AdsManager.Instance.OnAdsEnabledChanged += ((bool state) => 
        { 
              _noAdsBtn.interactable = false;
              _noAdsBtn.onClick.RemoveAllListeners();
        });
        //***

        _200CoinsBtn.onClick.AddListener(IAPManager.Instance.Purchase200Coins);
        _200CoinsBtn.GetComponentInChildren<TextMeshProUGUI>().text = coins200PriceStr;
        _1000CoinsBtn.onClick.AddListener(IAPManager.Instance.Purchase1000Coins);
        _1000CoinsBtn.GetComponentInChildren<TextMeshProUGUI>().text = coins1000PriceStr;
#endif
    }
}
