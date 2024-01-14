using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private GameObject _joystick;
    [SerializeField]
    private Button _homeBtn;

    private void Start()
    {
        //Configure mobile btns
        _joystick.SetActive(Application.isMobilePlatform);
        //_homeBtn.onClick.AddListener();
    }
}
