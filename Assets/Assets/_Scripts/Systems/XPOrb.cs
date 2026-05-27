using UnityEngine;

public class XPOrb : MonoBehaviour
{
    private int _xpValue;
    private Transform _player;
    private float _magnetSpeed;
    private bool _attracted;

    public void Init(int xpValue, Transform player)
    {
        _xpValue = xpValue;
        _player = player;
        _attracted = false;
        _magnetSpeed = 0f;
    }

    private void Update()
    {
        if (_player == null || !_attracted)
        {
            return;
        }

        _magnetSpeed += 20f * Time.deltaTime;
        transform.position = Vector2.MoveTowards(
            transform.position,
            _player.position,
            _magnetSpeed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, _player.position) < 0.2f)
        {
            Collect();
        }
    }

    public void Attract()
    {
        _attracted = true;
    }

    private void Collect()
    {
        XPSystem.Instance.AddXP(_xpValue);
        PoolManager.Instance.ReturnXPOrb(this);
    }
}