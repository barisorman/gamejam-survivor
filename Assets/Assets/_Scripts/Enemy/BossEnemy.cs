using UnityEngine;

public class BossEnemy : BaseEnemy
{
    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
    }

    protected override void Die()
    {
        ScreenShake.Instance.Shake(0.5f);
        GameManager.Instance.SetWin();
        Destroy(gameObject);
    }
}