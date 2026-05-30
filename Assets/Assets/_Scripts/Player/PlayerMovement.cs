using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerStats _stats;
    private Rigidbody2D _rb;
    private Animator _animator;
    private Vector2 _moveInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _stats = GetComponent<PlayerStats>();
        _animator = GetComponent<Animator>();

        _rb.gravityScale = 0f;
        _rb.freezeRotation = true;
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        _moveInput = new Vector2(x, y).normalized;

        bool isMoving = _moveInput.magnitude > 0f;
        _animator.SetBool("IsMoving", isMoving);

        // Flip sprite based on movement direction
        if (x < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (x > 0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _moveInput * _stats.MoveSpeed * Time.fixedDeltaTime);
    }
}