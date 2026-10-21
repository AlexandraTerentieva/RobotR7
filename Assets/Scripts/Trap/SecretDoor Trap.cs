using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Дверь-ловушка: открывается при входе игрока, закрывается — когда он внутри
public class TrapDoor : MonoBehaviour
{
    public float openDistance = 2f;   // На каком расстоянии открыть (по E)
    public float openSpeed = 2f;      // Скорость открытия
    public Transform player;          // Ссылка на игрока (назначить в инспекторе)

    private Vector3 closedPosition;   // Закрытая позиция
    private Vector3 openPosition;     // Открытая позиция (вниз)
    private bool isOpen = false;      // Открыта ли дверь
    private bool isLocked = false;    // Ловушка сработала

    private void Start()
    {
        // Запоминаем позиции
        closedPosition = transform.position;
        openPosition = closedPosition + Vector3.down * 10f; // Дверь уезжает на 4 метра вниз

        // Если игрок не назначен — ищем по тегу
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    private void Update()
    {
        if (player == null) return;

        // Если ловушка сработала — дверь закрывается (уезжает наверх)
        if (isLocked)
        {
            transform.position = Vector3.MoveTowards(transform.position, closedPosition, openSpeed * Time.deltaTime);
            return;
        }

        // Считаем расстояние по горизонтали
        Vector3 doorPos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 playerPos = new Vector3(player.position.x, 0, player.position.z);
        float distance = Vector3.Distance(doorPos, playerPos);

        // Если игрок рядом и нажал E — открываем дверь
        if (!isOpen && distance <= openDistance && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = true;
            Debug.Log("Дверь-ловушка открыта.");
        }

        // Двигаем дверь
        if (isOpen)
        {
            transform.position = Vector3.MoveTowards(transform.position, openPosition, openSpeed * Time.deltaTime);
        }
    }

    // Ловушка: срабатывает, когда игрок ЗАХОДИТ внутрь (за дверь)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isOpen)
        {
            // Игрок прошёл дверь — закрываем её
            isLocked = true;
            isOpen = false;
            Debug.Log("Дверь-ловушка закрылась! Игрок заперт.");
        }
    }
}