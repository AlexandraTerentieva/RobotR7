using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Шипы: урон мгновенно при наступании + каждую секунду, пока стоишь
public class Spikes : MonoBehaviour
{
    public int damage = 2;            // Сколько урона
    public float triggerRadius = 1f;  // Радиус срабатывания
    public float damageInterval = 1f; // Как часто наносить урон, пока стоишь

    private Transform player;
    private bool wasOnSpikes = false; // Был ли игрок на шипах
    private float timer = 0f;         // Таймер периодического урона

    private void Start()
    {
        // Находим игрока
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    private void Update()
    {
        if (player == null) return;

        // Считаем расстояние по горизонтали
        Vector3 spikePos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 playerPos = new Vector3(player.position.x, 0, player.position.z);
        float distance = Vector3.Distance(spikePos, playerPos);

        // Проверяем, стоит ли игрок на шипах
        bool playerOnSpikes = distance <= triggerRadius
                              && player.position.y <= transform.position.y + 1f
                              && player.position.y >= transform.position.y - 1f;

        if (playerOnSpikes)
        {
            // Мгновенный урон при наступании
            if (!wasOnSpikes)
            {
                PlayerHealth health = player.GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                    Debug.Log($"Шипы нанесли {damage} урона (мгновенно)!");
                }
                timer = 0f;
            }

            // Периодический урон, пока стоишь
            timer += Time.deltaTime;
            if (timer >= damageInterval)
            {
                timer = 0f;
                PlayerHealth health = player.GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                    Debug.Log($"Шипы нанесли {damage} урона (периодически)!");
                }
            }
        }
        else
        {
            timer = 0f;
        }

        // Запоминаем состояние
        wasOnSpikes = playerOnSpikes;
    }
}