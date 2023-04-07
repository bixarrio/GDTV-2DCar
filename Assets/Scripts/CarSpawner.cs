using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] OtherCarController[] _carPrefabs;
    [SerializeField] CarSpawnPoint[] _carSpawnPoints;
    [SerializeField] float _spawnInterval = 1f;
    [SerializeField] float _startDelay = 1f;

    private bool _isActive = false;
    private float _spawnCooldown = 0f;

    private void Start()
    {
        StartSpawning();
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
        // If we're not active, do nothing
        if (!_isActive) return;

        // Increase the cooldown
        _spawnCooldown += Time.deltaTime;
        // If it's still in cooldown, do nothing
        if (_spawnCooldown < _spawnInterval) return;
        // Spawn a car
        SpawnRandomCar();
        // Restart cooldown
        _spawnCooldown = 0f;
    }

    private void StartSpawning()
    {
        Invoke(nameof(SetActive), _startDelay);
    }

    private void SetActive()
    {
        _isActive = true;
    }

    private void SpawnRandomCar()
    {
        // Pick a random car
        var chosenCar = _carPrefabs[Random.Range(0, _carPrefabs.Length)];
        // Pick a random spawner and ask it to spawn a car
        _carSpawnPoints[Random.Range(0, _carSpawnPoints.Length)].SpawnCar(transform, chosenCar);
    }

    private void OnPlayerCrashed()
    {
        _isActive = false;
    }

    private void OnPlayerRecovered()
    {
        StartSpawning();
    }

}
