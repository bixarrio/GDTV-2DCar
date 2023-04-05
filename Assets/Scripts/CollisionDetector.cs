using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private RoadSpawner _roadSpawner;
    private PlayerController _playerController;

    private void Awake()
    {
        // Find the road spawner. There should only be one
        _roadSpawner = FindObjectOfType<RoadSpawner>();
        // Get the player controller
        _playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If we didn't collide with a car do nothing
        if (!collision.gameObject.CompareTag("Car")) return;

        // Stop the road from moving
        _roadSpawner.Stop();

        // Disable player control and do the spin
        _playerController.Crash();

        // Destroy the other car
        Destroy(collision.gameObject);
    }
}
