using System;

public static class GameEvents
{
    public static event Action OnPause;
    public static event Action OnResume;
    public static event Action OnOpenNextLevel;
    public static event Action OnGameOver;
    public static event Action OnVictory;
    public static event Action OnAddItem;
    public static event Action OnSubstractItem;

    public static void TriggerPause() => OnPause?.Invoke();
    public static void TriggerResume() => OnResume?.Invoke();
    public static void TriggerOpenNextLevel() => OnOpenNextLevel?.Invoke();
    public static void TriggerGameOver() => OnGameOver?.Invoke();
    public static void TriggerVictory() => OnVictory?.Invoke();
    public static void TriggerAddItem() => OnAddItem?.Invoke();
    public static void TriggerSubstractItem() => OnSubstractItem?.Invoke();

}
