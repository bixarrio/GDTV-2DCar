using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] OtherCarController _carPrefab;
    [SerializeField] CarSpawnPoint[] _carSpawnPoints;
    [SerializeField] float _spawnInterval = 1f;

    private float _spawnCooldown = 0f;

    private void Update()
    {
        // Increase the cooldown
        _spawnCooldown += Time.deltaTime;
        // If it's still in cooldown, do nothing
        if (_spawnCooldown < _spawnInterval) return;
        // Spawn a car
        SpawnRandomCar();
        // Restart cooldown
        _spawnCooldown = 0f;
    }

    private void SpawnRandomCar()
    {
        // Pick a random spawner and ask it to spawn a car
        _carSpawnPoints[Random.Range(0, _carSpawnPoints.Length)].SpawnCar(transform, _carPrefab);
    }
}
