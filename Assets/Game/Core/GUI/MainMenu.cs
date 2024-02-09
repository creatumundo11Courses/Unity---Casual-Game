using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MenuBase
{
    [SerializeField]
    private Button _playBtn;
    [SerializeField]
    private Button _noAdsBtn;
    [SerializeField]
    private Button _shopBtn;

    private void Start()
    {
        _playBtn.onClick.AddListener(GameMode.Instance.StartGame);
        _shopBtn.onClick.AddListener(GameMode.Instance.ShopGame);
#if COURSE_SERVICES_ADS && COURSE_SERVICES_IAP
     _noAdsBtn.onClick.AddListener(IAPManager.Instance.Buy("NoAds"));
#else
     _noAdsBtn.gameObject.SetActive(false);
#endif
    }
}
