using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] float _spawnX;
    [SerializeField] float _destroyX;
    [SerializeField] float _roadWidth;
    [SerializeField] GameObject _roadPrefab;
    [SerializeField] float _acceleration = 0.001f;
    [SerializeField] float _maxSpeed = 1f;

    private float _currentSpeed = 0f;

    private void Awake()
    {
        InitialiseRoad();
    }

    private void Update()
    {
        _currentSpeed = Mathf.Min(_maxSpeed, _currentSpeed + _acceleration * Time.deltaTime);
        MoveRoad();
    }

    public void Stop()
    {
        _currentSpeed = 0f;
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
            child.Translate(Vector2.left * _currentSpeed);
            // If it's a car, we're done
            if (child.CompareTag("Car")) continue;
            // If it's not a car and the road has moved off the right, move it back to the front
            if (child.position.x < _destroyX) child.position = spawnPos;
        }
    }
}
