using Unity.Cinemachine;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance;

    [SerializeField] private CinemachineImpulseSource _impulseSource;

    private void Awake()
    {
        Instance = this;
    }

    public void Shake(float force = 0.3f)
    {
        _impulseSource.GenerateImpulse(force);
    }
}