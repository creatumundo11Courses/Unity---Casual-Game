using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseMenu : MenuBase
{
    [SerializeField]
    private Button _retryBtn;
    void Start()
    {
        _retryBtn.onClick.AddListener(RetryGame);
    }

    private void RetryGame()
    {
        GameMode.Instance.ResetLevel();
        GameMode.Instance.StartGame();
    }
    
}
