using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MenuBase
{
    [SerializeField]
    private Button _playBtn;
    [SerializeField]
    private Button _shopBtn;

    private void Start()
    {
        _playBtn.onClick.AddListener(GameMode.Instance.StartGame);

#if COURSE_SERVICES_ADS && COURSE_SERVICES_IAP
        _shopBtn.onClick.AddListener(GameMode.Instance.ShopGame);
#else
        _shopBtn.gameObject.SetActive(false); 
#endif
    }
}
