using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseMenu : MenuBase
{
    [SerializeField]
    private Button _retryBtn;
    [SerializeField]
    private Button _rewardBtn;

    private void OnEnable()
    {
        _rewardBtn.interactable = true;
    }
    void Start()
    {
        _retryBtn.onClick.AddListener(RetryGame);

#if COURSE_SERVICES_ADS 
      _rewardBtn.onClick.AddListener(() => {
          AdsManager.Instance.ShowRewardVideo();
          _rewardBtn.interactable = false;
           
        });
#else
        _rewardBtn.gameObject.SetActive(false);
#endif
       
    }

    private void RetryGame()
    {
        GameMode.Instance.ResetLevel();
        GameMode.Instance.StartGame();
    }
    
}
