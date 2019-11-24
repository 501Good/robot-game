using System;

public static class Events
{
    public static event Action<int> OnPlayerChangeHealth;
    public static void PlayerChangeHealth(int value) => OnPlayerChangeHealth?.Invoke(value);

    public static event Func<int> OnPlayerRequestHealth;
    public static int PlayerRequestHealth() => OnPlayerRequestHealth?.Invoke() ?? 0;

    public static event Func<PlayerController2D> OnRequestPlayerGameObject;
    public static PlayerController2D RequestPlayerGameObject() => OnRequestPlayerGameObject?.Invoke() ?? null;
    
}
