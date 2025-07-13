using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PersistenceKeys", menuName = "ScriptableObjects/PersistanceKeys", order = 1)]
public class PersistenceKeys : ScriptableObject
{
    [Header("Persistance Keys")]

    [Tooltip("High Score key")]
    [SerializeField] private string highScoreKey = "HighScore";

    [Tooltip("Music enabled/disabled key")]
    [SerializeField] private string musicKey = "Music";

    [Tooltip("Sound enabled/disabled key")]
    [SerializeField] private string soundFxKey = "SoundFX";

    [Tooltip("Audio Volume key")]
    [SerializeField] private string volumeKey = "Volume";
    public string HighScoreKey { get => highScoreKey; private set => highScoreKey = value; }
    public string MusicKey { get => musicKey; private set => musicKey = value; }
    public string SoundFXKey { get => soundFxKey; private set => soundFxKey = value; }
    public string VolumeKey { get => volumeKey; private set => volumeKey = value; }
}