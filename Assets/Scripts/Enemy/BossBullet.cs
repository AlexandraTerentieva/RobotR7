using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Пуля: летит, наносит урон и исчезает при столкновении
public class BossBullet : MonoBehaviour
{
    public int damage = 1;       // Урон пули
    public float lifeTime = 3f;  // Время жизни (чтобы не висела вечно)

    private bool isPlayerBullet = false; // Флаг: чья это пуля

    private void Start()
    {
        Destroy(gameObject, lifeTime); // Удаляем через 3 секунды
    }

    // Этот метод вызывает игрок, когда стреляет
    public void SetAsPlayerBullet()
    {
        isPlayerBullet = true;
    }

    // Срабатывает, когда пуля с чем-то сталкивается
    private void OnTriggerEnter(Collider other)
    {
        // 1. Если это ИГРОК
        if (other.CompareTag("Player"))
        {
            // Если пуля НЕ игрока (т.е. босса) — наносим урон
            if (!isPlayerBullet)
            {
                PlayerHealth player = other.GetComponent<PlayerHealth>();
                if (player != null) player.TakeDamage(damage);
                Destroy(gameObject);
            }
            // Если пуля ИГРОКА — ничего не делаем, пуля летит дальше
            return;
        }

        // 2. Если это ВРАГ (слизень или босс)
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null) enemy.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // 3. Если это стена/пол — разбиваем пулю
        Destroy(gameObject);
    }
}