using UnityEngine;

public class BossEnemy : BaseEnemy
{
    protected override void Die()
    {
        GameManager.Instance.SetWin();
        Destroy(gameObject);
    }
}