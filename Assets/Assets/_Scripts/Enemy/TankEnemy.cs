using UnityEngine;

public class TankEnemy : BaseEnemy
{
    protected override void FixedUpdate()
    {
        if (CurrentState == EnemyState.Dead || PlayerTransform == null)
        {
            return;
        }

        Vector2 direction = ((Vector2)PlayerTransform.position - Rb.position).normalized;
        Rb.MovePosition(Rb.position + direction * Data.MoveSpeed * Time.fixedDeltaTime);

        // Flipped compared to other enemies due to sprite direction
        if (direction.x < 0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (direction.x > 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}