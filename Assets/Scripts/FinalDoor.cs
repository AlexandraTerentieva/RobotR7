using System.Collections.Generic;
using System.Collections;
using UnityEngine;

// Дверь: зажигает слоты по расстоянию (коллайдер не нужен)
public class FinalDoor : MonoBehaviour
{
    public KeySlot[] slots;             // Массив из 10 слотов (перетаскиваются в инспекторе)
    public float insertInterval = 0.5f; // Пауза между зажиганием слотов (в секундах)
    public float openDistance = 3f;     // На каком расстоянии дверь реагирует на игрока

    private bool isActivated = false;   // Флаг: запущена ли анимация двери
    private Transform player;           // Ссылка на трансформ игрока

    private void Start()
    {
        // При старте игры ищем игрока по тегу
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    private void Update()
    {
        // Если игрок не найден или дверь уже активирована — выходим
        if (player == null || isActivated) return;

        // Считаем расстояние по горизонтали (без высоты Y)
        Vector3 doorPos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 playerPos = new Vector3(player.position.x, 0, player.position.z);
        float distance = Vector3.Distance(doorPos, playerPos);

        // Если игрок подошел близко — активируем дверь
        if (distance <= openDistance)
        {
            // ПРОВЕРКА: Если босс еще жив — не открываем
            if (BossRobot.isBossAlive)
            {
                Debug.Log("Дверь заблокирована! Сначала победите босса.");
                return; // Выходим, слоты не зажигаются
            }

            // Если босс мертв — запускаем зажигание
            Debug.Log("Игрок подошел к двери. Босс мертв, зажигаем слоты!");
            isActivated = true;
            StartCoroutine(ActivateSlots());
        }
    }

    // Корутина: зажигает слоты по очереди
    private IEnumerator ActivateSlots()
    {
        // Считаем, сколько слотов нужно зажечь (минимум из собранных ключей и слотов)
        int slotsToActivate = Mathf.Min(GameManager.keysCollected, slots.Length);

        // Проходим по каждому слоту
        for (int i = 0; i < slotsToActivate; i++)
        {
            slots[i].TurnOn(); // Зажигаем текущий слот
            yield return new WaitForSeconds(insertInterval); // Ждем полсекунды
        }

        // Забираем ключи у игрока (отнимаем то, что вставили)
        GameManager.keysCollected -= slotsToActivate;

        // Защита от отрицательных чисел
        if (GameManager.keysCollected < 0) GameManager.keysCollected = 0;

        // Если все ключи вставлены — показываем экран победы
        if (GameManager.keysCollected <= 0 && slotsToActivate >= GameManager.keysNeeded)
        {
            WinScreen winScreen = FindObjectOfType<WinScreen>();
            if (winScreen != null) winScreen.ShowWinScreen();
        }
    }
}