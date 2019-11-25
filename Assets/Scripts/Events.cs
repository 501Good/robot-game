using System;

public static class Events
{
    public static event Action<int> OnPlayerChangeHealth;
    public static void PlayerChangeHealth(int value) => OnPlayerChangeHealth?.Invoke(value);

    public static event Func<int> OnPlayerRequestHealth;
    public static int PlayerRequestHealth() => OnPlayerRequestHealth?.Invoke() ?? 0;

    public static event Action<Cinemachine.CinemachineVirtualCamera> OnChangeToCamera;
    public static void ChangeToCamera(Cinemachine.CinemachineVirtualCamera value) => OnChangeToCamera?.Invoke(value);
    /*
    public static event Action OnTowerSelected;
    public static void TowerSelected() => OnTowerSelected?.Invoke();

    public static event Action OnLevelWin;
    public static void LevelWin() => OnLevelWin?.Invoke();

    public static event Action OnLevelLose;
    public static void LevelLose() => OnLevelLose?.Invoke();

    //public static event Action<ScenarioData> OnStartGame;
    //public static void StartGame(ScenarioData data) => OnStartGame?.Invoke(data);

    public static event Action<int> OnChangeGold;
    public static void ChangeGold(int value) => OnChangeGold?.Invoke(value);

    public static event Func<int> OnRequestGold;
    public static int RequestGold() => OnRequestGold?.Invoke() ?? 0;

    public static event Action<int> OnChangeLives;
    public static void ChangeLives(int value) => OnChangeLives?.Invoke(value);

    public static event Func<int> OnRequestLives;
    public static int RequestLives() => OnRequestLives?.Invoke() ?? 0;
    */
}
