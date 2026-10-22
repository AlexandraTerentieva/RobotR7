using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Шипы: наносят урон при касании
public class Spikes : MonoBehaviour
{
    public int damage = 2; // Сколько урона отнимать

    // Срабатывает, когда кто-то заходит в триггер шипов
    private void OnTriggerEnter(Collider other)
    {
        // Если это игрок
        if (other.CompareTag("Player"))
        {
            // Находим его здоровье и отнимаем
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
}