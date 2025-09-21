using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "GameProgressionData", menuName = "ScriptableObjects/GameProgressionData", order = 1)]
public class GameProgressionData : ScriptableObject
{
    [Header("Score configuration")]

    [SerializeField]
    [Tooltip("Time record by level")]
    private TimeRecordByLevel[] _timeRecord;

    public TimeRecordByLevel[] TimeRecords { get => _timeRecord; set => _timeRecord = value; }

    [System.Serializable]
    public struct TimeRecordByLevel
    {
        public int level;
        public float timeRecord;
    }
}
