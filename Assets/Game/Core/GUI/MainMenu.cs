using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MenuBase
{
    [SerializeField]
    private Button _playBtn;

    private void Start()
    {
        _playBtn.onClick.AddListener(GameMode.Instance.StartGame);
    }
}
