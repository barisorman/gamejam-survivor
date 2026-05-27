using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerStats _stats;
    private Rigidbody2D _rb;
    private Vector2 _moveInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _stats = GetComponent<PlayerStats>();

        // No gravity or rotation in top-down 2D
        _rb.gravityScale = 0f;
        _rb.freezeRotation = true;
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Normalize prevents faster diagonal movement
        _moveInput = new Vector2(x, y).normalized;
    }

    private void FixedUpdate()
    {
        // MovePosition works with physics engine, avoids clipping
        _rb.MovePosition(_rb.position + _moveInput * _stats.MoveSpeed * Time.fixedDeltaTime);
    }
}