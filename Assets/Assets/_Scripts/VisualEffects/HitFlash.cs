using System.Collections;
using UnityEngine;

public class HitFlash : MonoBehaviour
{
    [SerializeField] private float _flashDuration = 0.08f;

    private SpriteRenderer _sr;
    private Color _originalColor;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _originalColor = _sr.color;
    }

    public void Flash()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        _sr.color = Color.white;
        yield return new WaitForSeconds(_flashDuration);
        _sr.color = _originalColor;
    }
}