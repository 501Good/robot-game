using System;

public static class Events
{
    public static event Action<PlayerController2D> OnSetCurrentCharacter;
    public static void SetCurrentCharacter(PlayerController2D character) => OnSetCurrentCharacter?.Invoke(character);

    public static event Action<Checkpoint> OnSetLastActiveCheckpoint;
    public static void SetLastActiveCheckpoint(Checkpoint checkpoint) => OnSetLastActiveCheckpoint?.Invoke(checkpoint);

    public static event Action OnRespawnPlayer;
    public static void RespawnPlayer() => OnRespawnPlayer?.Invoke();

    public static event Action<int> OnPlayerChangeHealth;
    public static void PlayerChangeHealth(int value) => OnPlayerChangeHealth?.Invoke(value);

    public static event Func<int> OnPlayerRequestHealth;
    public static int PlayerRequestHealth() => OnPlayerRequestHealth?.Invoke() ?? 0;
    
    public static event Func<PlayerController2D> OnRequestPlayerGameObject;
    public static PlayerController2D RequestPlayerGameObject() => OnRequestPlayerGameObject?.Invoke() ?? null;
    
    public static event Action<Cinemachine.CinemachineVirtualCamera> OnChangeToCamera;
    public static void ChangeToCamera(Cinemachine.CinemachineVirtualCamera value) => OnChangeToCamera?.Invoke(value);
    

}
