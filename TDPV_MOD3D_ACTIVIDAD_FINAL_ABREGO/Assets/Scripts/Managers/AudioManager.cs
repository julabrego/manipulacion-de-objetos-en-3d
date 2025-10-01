using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip grabSound;
    [SerializeField] private AudioClip throwSound;
    [SerializeField] private AudioClip deliverySound;
    [SerializeField] private AudioClip victorySound;
    [SerializeField] private AudioClip defeatSound;

    private void OnEnable()
    {
        SoundFXEvents.OnGrabSound += PlayGrabSound;
        SoundFXEvents.OnThrowSound += PlayThrowSound;
        SoundFXEvents.OnDeliverySound += PlayDeliverySound;
        SoundFXEvents.OnVictorySound += PlayVictorySound;
        SoundFXEvents.OnDefeatSound += PlayDefeatSound;
    }

    private void OnDisable()
    {
        SoundFXEvents.OnGrabSound -= PlayGrabSound;
        SoundFXEvents.OnThrowSound -= PlayThrowSound;
        SoundFXEvents.OnDeliverySound -= PlayDeliverySound;
        SoundFXEvents.OnVictorySound -= PlayVictorySound;
        SoundFXEvents.OnDefeatSound -= PlayDefeatSound;
    }

    private void PlayGrabSound()
    {
        sfxSource.PlayOneShot(grabSound);
    }

    private void PlayThrowSound()
    {
        sfxSource.PlayOneShot(throwSound);
    }

    private void PlayDeliverySound()
    {
        sfxSource.PlayOneShot(deliverySound);
    }

    private void PlayVictorySound()
    {
        sfxSource.PlayOneShot(victorySound);
    }

    private void PlayDefeatSound()
    {
        sfxSource.PlayOneShot(defeatSound);
    }

}
