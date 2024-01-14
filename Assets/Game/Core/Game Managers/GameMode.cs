using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    InGame, InPause, InMenu
}
[DefaultExecutionOrder(-9999)]
public class GameMode : MonoBehaviour
{
    public static GameMode Instance;
    public static GameObject Player;
    public static GameState GameState;

    [SerializeField]
    private GameObject _playerPrefab;
    private CharacterControllerPlayer _player;
    [SerializeField]
    private Transform _spawnT;

    [SerializeField]
    private LevelInstantiator _levelInstantiator;
    [SerializeField]
    private GameMenus _gameMenus;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        if (Player == null)
        {
            Player = Instantiate(_playerPrefab, _spawnT.position, Quaternion.identity);
            _player = Player.GetComponentInChildren<CharacterControllerPlayer>();
            _player.OnDead += OnPlayerDead;
        }
    }

    private void OnPlayerDead(LivingEntity entity)
    {
        //GameOver
    }

    public void MainMenuGame()
    {
        if (GameState == GameState.InGame)
        {
            ResetLevel();
        }
        _gameMenus.OpenMenu(GameMenus.ID_MAIN_MENU);
        GameState = GameState.InMenu;
    }

    public void StartGame()
    {
        _gameMenus.OpenMenu(GameMenus.ID_HUD_MENU);
        GameState = GameState.InGame;
    }

    public void ResetLevel()
    {
        _levelInstantiator.ResetLevel();
    }

    public void NextLevel()
    {
        _levelInstantiator.NextLevel();
    }
}
