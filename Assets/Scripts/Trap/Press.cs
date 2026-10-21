using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ловушка-пресс: двигается вверх-вниз и наносит урон игроку
public class Press : MonoBehaviour
{
    [Header("Настройки движения")]
    public float speed = 2f;       // Скорость движения пресса
    public float distance = 3f;    // На сколько метров опускается вниз

    [Header("Настройки урона")]
    public int damage = 5;         // Сколько урона наносит

    private Vector3 startPosition; // Начальная позиция пресса
    private bool goingDown = true; // Двигается вниз?

    private void Start()
    {
        // Запоминаем начальную позицию
        startPosition = transform.position;
    }

    private void Update()
    {
        // Если двигаемся вниз — опускаемся
        if (goingDown)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;

            // Если опустились ниже, чем нужно — начинаем подниматься
            if (transform.position.y <= startPosition.y - distance)
            {
                goingDown = false;
            }
        }
        else
        {
            // Поднимаемся вверх
            transform.position += Vector3.up * speed * Time.deltaTime;

            // Если вернулись наверх — начинаем опускаться
            if (transform.position.y >= startPosition.y)
            {
                goingDown = true;
            }
        }
    }

    // Срабатывает, когда игрок заходит в коллайдер пресса
    private void OnTriggerEnter(Collider other)
    {
        // Если это игрок — наносим урон
        if (other.CompareTag("Player"))
        {
            // Находим скрипт PlayerHealth у игрока
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
}
