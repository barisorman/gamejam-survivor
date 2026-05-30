using UnityEngine;

[CreateAssetMenu(menuName = "SurvivorLite/Upgrade Data", fileName = "UpgradeData_")]
public class UpgradeData : ScriptableObject
{
    public string DisplayName;
    public string Description;
    public float Value;
    public UpgradeType Type;
}

public enum UpgradeType
{
    ProjectileDamage,
    ProjectileFireRate,
    ProjectileSpeed,
    MoveSpeed,
    MaxHP,
    MagnetRange
}