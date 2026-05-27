using UnityEngine;

// Creates asset via right-click > Create > SurvivorLite > Enemy Data
[CreateAssetMenu(menuName = "SurvivorLite/Enemy Data", fileName = "EnemyData_")]
public class EnemyData : ScriptableObject
{
    public string EnemyName;
    public float MaxHp = 30f;
    public float MoveSpeed = 2.5f;
    public float DamagePerSec = 8f;
    public int XpValue = 5;
}