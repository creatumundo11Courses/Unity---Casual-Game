using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    InGame, InPause, InMenu
}

public class GameMode : MonoBehaviour
{
    public static GameObject Player;
    public static GameState GameState;

    [SerializeField]
    private GameObject _playerPrefab;
    private CharacterControllerPlayer _player;
    [SerializeField]
    private Transform _spawnT;
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
}
