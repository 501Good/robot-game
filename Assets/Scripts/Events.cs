using System;

public static class Events
{
    public static event Action<int> OnPlayerChangeHealth;
    public static void PlayerChangeHealth(int value) => OnPlayerChangeHealth?.Invoke(value);

    public static event Func<int> OnPlayerRequestHealth;
    public static int PlayerRequestHealth() => OnPlayerRequestHealth?.Invoke() ?? 0;
    
    public static event Func<PlayerController2D> OnRequestPlayerGameObject;
    public static PlayerController2D RequestPlayerGameObject() => OnRequestPlayerGameObject?.Invoke() ?? null;
    
    public static event Action<Cinemachine.CinemachineVirtualCamera> OnChangeToCamera;
    public static void ChangeToCamera(Cinemachine.CinemachineVirtualCamera value) => OnChangeToCamera?.Invoke(value);

    public static event Action OnPlayerDeath;
    public static void PlayerDeath() => OnPlayerDeath?.Invoke();

    public static event Action<bool> OnChangeAllowTransformation;
    public static void ChangeAllowTransformation(bool value) => OnChangeAllowTransformation?.Invoke(value);

    public static event Func<bool> OnRequestAllowTransformation;
    public static bool RequestAllowTransformation() => OnRequestAllowTransformation?.Invoke() ?? false;
}
