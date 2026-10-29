using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Секрет: собирается и сохраняется
public class SecretItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Отладка: проверяем, видит ли скрипт игрока
            Debug.Log("Секрет подобран!");

            GameManager.secretsCollected++;
            Debug.Log("Всего секретов: " + GameManager.secretsCollected);

            Destroy(gameObject);
        }
    }
}