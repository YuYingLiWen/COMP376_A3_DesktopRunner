
using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty", menuName = "ScriptableObjects/Difficulty", order = 1)]
public class DifficultySO : ScriptableObject
{
    [SerializeField] float holeChance;
    [SerializeField] float obsctacleChance;
    [SerializeField] float powerUpChance;
    [SerializeField] float upgradeChance;
    [SerializeField] float bearChance;

    public float HoleChance { get => holeChance; }
    public float ObsctacleChance { get => obsctacleChance; }
    public float PowerUpChance { get => powerUpChance; }
    public float UpgradeChance { get => upgradeChance; }
    public float BearChance { get => bearChance; }
}
