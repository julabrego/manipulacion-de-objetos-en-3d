using System;

public static class SoundFXEvents
{
    public static event Action OnCollectCoin;

    public static void TriggerCollectCoin() => OnCollectCoin.Invoke();
}