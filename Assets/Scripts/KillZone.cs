using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Зона смерти: если игрок сюда падает — он умирает
public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Если в зону попал игрок
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                // Наносим смертельный урон
                health.TakeDamage(9999);
            }
        }
    }
}