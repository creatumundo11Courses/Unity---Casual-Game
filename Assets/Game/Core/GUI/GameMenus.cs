using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenus : MonoBehaviour
{
    public const string ID_HUD_MENU = "HudMenu";
    public const string ID_MAIN_MENU = "MainMenu";
    public const string ID_LOSE_MENU = "LoseMenu";
    public const string ID_WIN_MENU = "WinMenu";

    [SerializeField]
    private HUD _hudMenu;
    [SerializeField]
    private MainMenu _mainMenu;
    [SerializeField]
    private LoseMenu _loseMenu;
    [SerializeField]
    private WinMenu _winMenu;

    private Dictionary<string, MenuBase> _menus = new();

    private MenuBase _currentMenu;

    private void Awake()
    {
        _menus.Add(ID_HUD_MENU, _hudMenu);
        _menus.Add(ID_MAIN_MENU, _mainMenu);
        _menus.Add(ID_LOSE_MENU, _loseMenu);
        _menus.Add(ID_WIN_MENU, _winMenu);
        HideAll();
    }

    public void OpenMenu(string id)
    {
        if (_menus[id] == _currentMenu) return;

        if (_currentMenu != null)
        {
            _currentMenu.Hide();
        }

        _currentMenu = _menus[id];

        _currentMenu.Show();
    }

    private void HideAll()
    {
        foreach (var menu in _menus)
        {
            menu.Value.Hide();
        }
    }
}
