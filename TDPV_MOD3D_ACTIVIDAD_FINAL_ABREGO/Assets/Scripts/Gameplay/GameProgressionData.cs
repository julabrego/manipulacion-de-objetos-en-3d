using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameProgressionData", menuName = "ScriptableObjects/GameProgressionData", order = 1)]
public class GameProgressionData : ScriptableObject
{
    [Header("Player configuration")]
    [Tooltip("Collectable item label")]
    [SerializeField] private string collectableItemLabel;

    [SerializeField]
    [Tooltip("Collectable items quantity to win")]
    private int collectableItemsToWin;

    [SerializeField]
    [Tooltip("Collected items")]
    private int collectedItems;

    public string CollectableItemLabel { get => collectableItemLabel; set => collectableItemLabel = value; }
    public int CollectableItemsToWin { get => collectableItemsToWin; set => collectableItemsToWin = value; }
    public int CollectedItems { get => collectedItems; set => collectedItems = value; }
}
