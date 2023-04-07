using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private PlayerController _playerController;

    private void Awake()
    {
        // Get the player controller
        _playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If we didn't collide with a car do nothing
        if (!collision.gameObject.CompareTag("Car")) return;

        // Disable player control and do the spin
        _playerController.Crash();

        // Destroy the other car
        Destroy(collision.gameObject);
    }
}
