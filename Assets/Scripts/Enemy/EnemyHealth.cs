using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Здоровье врагов: слизней и босса
public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3; // Максимальное здоровье (для слизня 3, для босса 100)
    public int currentHealth; // Текущее здоровье

    private void Start()
    {
        currentHealth = maxHealth; // В начале здоровье полное
    }

    // Вызывается, когда враг получает урон
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Отнимаем здоровье
        Debug.Log($"{gameObject.name} получил {damage} урона. Осталось: {currentHealth}");

        if (currentHealth <= 0) Die(); // Если здоровье 0 — умираем
    }

    // Смерть врага
    private void Die()
    {
        Debug.Log($"{gameObject.name} умер!");

        // Если это был БОСС — снимаем блокировку с двери
        if (GetComponent<BossRobot>() != null)
        {
            BossRobot.isBossAlive = false; // Сообщаем двери, что босс мертв
            Debug.Log("Босс побежден! Дверь разблокирована.");
        }

        Destroy(gameObject); // Удаляем врага
    }
}