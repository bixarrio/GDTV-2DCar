using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static event Action PlayerCrashed;
    public static event Action PlayerRecovered;

    [SerializeField] float _moveSpeed = 10f;
    [Header("Crash")]
    [SerializeField] float _crashTime = 1f;
    [SerializeField] int _crashSpins = 3;

    bool _isActive = true;

    private void Update()
    {
        if (!_isActive) return;

        var moveVector = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        var position = transform.position;
        position += moveVector * _moveSpeed * Time.deltaTime;

        if (position.x <= -8.05f || position.x >= 8.05f || position.y <= -4.05f || position.y >= 4.05f) return;

        transform.position = position;
    }

    public void Crash()
    {
        // If we're already disable, do nothing
        if (!_isActive) return;
        // Do the crash spin
        StartCoroutine(CrashRoutine());
    }

    private IEnumerator CrashRoutine()
    {
        PlayerCrashed?.Invoke();

        // Disable control
        _isActive = false;

        // Spin the car
        var maxSpinAngle = 360f * _crashSpins;
        for (var timer = 0f; timer / _crashTime <= 1f; timer += Time.deltaTime)
        {
            var spinAngle = Mathf.Lerp(0f, maxSpinAngle, timer / _crashTime);
            transform.rotation = Quaternion.Euler(0f, 0f, spinAngle);
            yield return null;
        }

        // Reset the car angle
        transform.rotation = Quaternion.Euler(Vector3.zero);
        // Enable control
        _isActive = true;

        PlayerRecovered?.Invoke();
    }
}
