using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinMenu : MenuBase
{
    [SerializeField]
    private Button _nextBtn;

    private void Start()
    {
        _nextBtn.onClick.AddListener(NextLevel);
    }

    private void NextLevel()
    {
        GameMode.Instance.NextLevel();
        GameMode.Instance.StartGame();
    }
}
