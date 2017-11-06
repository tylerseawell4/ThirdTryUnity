using UnityEngine;

public class OvershieldPickup : MonoBehaviour
{
    private PlayerHealth _health;
    private void Awake()
    {
        _health = FindObjectOfType<PlayerHealth>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _health.IncreasePlayerHealth();
            Destroy(gameObject);
        }
    }
}
