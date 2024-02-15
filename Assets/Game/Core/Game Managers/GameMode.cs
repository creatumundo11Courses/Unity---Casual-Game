using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.Analytics;

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

    public static int Coins;
    public const string KEY_PP_COINS = "Coins";
    public event Action<int> OnCoinsChange;

    [SerializeField]
    private LevelInstantiator _levelInstantiator;
    [SerializeField]
    private GameMenus _gameMenus;

    [SerializeField]
    private AudioClip _ambientSound;
    [SerializeField]
    private AudioClip _winSound;
    [SerializeField]
    private AudioClip _loseSound;

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
            _gameMenus.OpenMenu(GameMenus.ID_MAIN_MENU);
            GameState = GameState.InMenu;
            _levelInstantiator.OnLevelChanged += OnLevelChanged;
            int coins = PlayerPrefs.GetInt(KEY_PP_COINS, 0);
            AddCoins(coins);

        }
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

    private void OnLevelChanged()
    {
        if (Player == null) return;
        GameAudio.StopAllSounds();
        _player.transform.position = _spawnT.position;
        _player.GenerateDefautPlayer();
        _player.Stop();
#if COURSE_SERVICES_ADS
        AdsManager.Instance.ShowInterstitialVideo();
#endif
        GameAudio.PlayAmbienceAudio(_ambientSound, 0.1f, true);
    }

    public void OnGoalReached()
    {
        GameAudio.StopAllSounds();
        _gameMenus.OpenMenu(GameMenus.ID_WIN_MENU);
#if COURSE_SERVICES_ANALYTICS
        CustomEvent myEvent = new CustomEvent("levelWin")
{
    { "userLevel", _levelInstantiator.CurrentLevelIndex }
};
        AnalyticsService.Instance.RecordEvent(myEvent);
#endif
        GameState = GameState.InMenu;
        _player.Stop();
        GameAudio.PlayEffectAudio(_winSound);

    }


    private void OnPlayerDead(LivingEntity entity)
    {
        GameAudio.StopAllSounds();
        _gameMenus.OpenMenu(GameMenus.ID_LOSE_MENU);
        GameState = GameState.InMenu;
#if COURSE_SERVICES_ANALYTICS
        CustomEvent myEvent = new CustomEvent("levelDied")
{
    { "userLevel", _levelInstantiator.CurrentLevelIndex },
    { "area", Mathf.FloorToInt(_player.transform.position.z / 60) }
};
        AnalyticsService.Instance.RecordEvent(myEvent);
        

#endif
        GameAudio.PlayEffectAudio(_loseSound);

    }
#if COURSE_SERVICES_ADS
    public void Grant30Characters()
    {
        Multiplicable multiplicable = _player.GetComponent<Multiplicable>();
        multiplicable.Generate(30, _player.transform);
        GameAudio.StopAllSounds();
        _gameMenus.OpenMenu(GameMenus.ID_HUD_MENU);
        GameState = GameState.InGame;
        GameAudio.PlayAmbienceAudio(_ambientSound, 0.1f, true);
    }

#endif

#if COURSE_SERVICES_IAP
    public void ShopGame()
    {
        _gameMenus.OpenMenu(GameMenus.ID_SHOP_MENU);
        GameState = GameState.InMenu;
    }
#endif
    public void AddCoins(int count)
    {
        Coins += count;
        PlayerPrefs.SetInt(KEY_PP_COINS, Coins);
        PlayerPrefs.Save();
        OnCoinsChange?.Invoke(Coins);
    }
    public void RemoveCoins(int count)
    {
        if (count > Coins) return;

        Coins -= count;
        PlayerPrefs.SetInt(KEY_PP_COINS, Coins);
        PlayerPrefs.Save();
        OnCoinsChange?.Invoke(Coins);
    }

}
