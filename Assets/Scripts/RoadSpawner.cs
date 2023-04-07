using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] float _spawnX;
    [SerializeField] float _destroyX;
    [SerializeField] float _roadWidth;
    [SerializeField] GameObject _roadPrefab;
    [SerializeField] float _acceleration = 0.001f;
    [SerializeField] float _maxSpeed = 1f;
    [SerializeField] float _displayMaxSpeed = 60f;

    private bool _isActive = false;
    private float _currentSpeed = 0f;

    private void Awake()
    {
        InitialiseRoad();
        _isActive = true;
    }

    private void OnEnable()
    {
        PlayerController.PlayerCrashed += OnPlayerCrashed;
        PlayerController.PlayerRecovered += OnPlayerRecovered;
    }

    private void OnDisable()
    {
        PlayerController.PlayerCrashed -= OnPlayerCrashed;
        PlayerController.PlayerRecovered -= OnPlayerRecovered;
    }

    private void Update()
    {
        var speedFactor = Mathf.InverseLerp(0f, _maxSpeed, _currentSpeed);
        Debug.Log($"Speed: {speedFactor * _displayMaxSpeed:0}km/h");

        if (!_isActive) return;

        _currentSpeed = Mathf.Min(_maxSpeed, _currentSpeed + _acceleration * Time.deltaTime);
        MoveRoad();
    }

    private void InitialiseRoad()
    {
        // This creates road pieces to cover the screen
        var offset = _destroyX;
        while (offset <= _spawnX)
        {
            var pos = new Vector3(offset, 0f, 1f);
            Instantiate(_roadPrefab, pos, Quaternion.identity, transform);
            offset += _roadWidth;
        }
    }

    private void MoveRoad()
    {
        var spawnPos = new Vector3(_spawnX, 0f, 1f);
        foreach (Transform child in transform)
        {
            // Move the child (road or car) to the left
            child.Translate(new Vector2(-_currentSpeed, 0f));
            // If it's a car, we're done
            if (child.CompareTag("Car")) continue;
            // If it's not a car and the road has moved off the left, move it back to the front
            if (child.position.x < _destroyX) child.position = spawnPos;
        }
    }

    private void OnPlayerCrashed()
    {
        _isActive = false;
        _currentSpeed = 0f;
    }

    private void OnPlayerRecovered()
    {
        _isActive = true;
    }
}
