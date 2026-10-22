using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ключ-ловушка: при подборе игрок мгновенно умирает
public class KeyTrap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Если ключ подобрал игрок
        if (other.CompareTag("Player"))
        {
            // Находим скрипт здоровья на игроке
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                // Отнимаем больше здоровья, чем есть, чтобы сработала смерть
                health.TakeDamage(9999);
            }

            // Удаляем ключ со сцены
            Destroy(gameObject);
        }
    }
}