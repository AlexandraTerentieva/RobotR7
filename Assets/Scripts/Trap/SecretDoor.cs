using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Потайная дверь: открывается на E, если игрок вплотную
public class SecretDoor : MonoBehaviour
{
    public float openDistance = 2f;    // Расстояние, чтобы открыть (в метрах)
    public float openSpeed = 10f;      // Скорость ухода вниз
    public float downDistance = 30f;   // На сколько метров уезжает вниз

    private Transform player;          // Ссылка на игрока
    private Vector3 closedPosition;    // Закрытая позиция двери
    private Vector3 openPosition;      // Открытая позиция (внизу)
    private bool isOpen = false;       // Открыта ли дверь
    private Collider doorCollider;     // Коллайдер двери

    private void Start()
    {
        // Запоминаем закрытую позицию
        closedPosition = transform.position;

        // Открытая позиция — вниз на downDistance метров
        openPosition = closedPosition + Vector3.down * downDistance;

        // Находим коллайдер двери
        doorCollider = GetComponent<Collider>();

        // Ищем игрока по тегу
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    private void Update()
    {
        if (player == null) return;

        // Считаем расстояние по горизонтали (без высоты Y)
        Vector3 doorPos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 playerPos = new Vector3(player.position.x, 0, player.position.z);
        float distance = Vector3.Distance(doorPos, playerPos);

        // Если игрок вплотную и нажал E — открываем
        if (distance <= openDistance && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = true;

            // Отключаем коллайдер, чтобы дверь не застревала
            if (doorCollider != null)
            {
                doorCollider.enabled = false;
            }

            Debug.Log("Дверь открыта.");
        }

        // Двигаем дверь вниз
        if (isOpen)
        {
            transform.position = Vector3.MoveTowards(transform.position, openPosition, openSpeed * Time.deltaTime);
        }
    }
}