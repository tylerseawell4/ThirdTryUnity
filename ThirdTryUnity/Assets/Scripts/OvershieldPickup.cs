using UnityEngine;

public class OvershieldPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<PlayerHealth>().IncreasePlayerHealth();
            Destroy(gameObject);
        }
    }
}
