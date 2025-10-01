using System;

public static class SoundFXEvents
{
    public static event Action OnGrabSound;
    public static event Action OnThrowSound;
    public static event Action OnDeliverySound;
    public static event Action OnVictorySound;
    public static event Action OnDefeatSound;

    public static void TriggerGrabSound() => OnGrabSound?.Invoke();
    public static void TriggerThrowSound() => OnThrowSound?.Invoke();
    public static void TriggerDeliverySound() => OnDeliverySound?.Invoke();
    public static void TriggerVictorySound() => OnVictorySound?.Invoke();
    public static void TriggerDefeatSound() => OnDefeatSound?.Invoke();

}