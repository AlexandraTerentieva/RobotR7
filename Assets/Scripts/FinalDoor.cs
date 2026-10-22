using System.Collections.Generic;
using System.Collections;
using UnityEngine;

// Дверь: зажигает столько слотов, сколько ключей собрано
public class FinalDoor : MonoBehaviour
{
    public KeySlot[] slots;
    public float insertInterval = 0.5f;
    private bool isActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            isActivated = true;
            StartCoroutine(ActivateSlots());
        }
    }

    private IEnumerator ActivateSlots()
    {
        // Сколько зажигать
        int slotsToActivate = Mathf.Min(GameManager.keysCollected, slots.Length);

        // Запускаем зажигание
        for (int i = 0; i < slotsToActivate; i++)
        {
            slots[i].TurnOn();
            yield return new WaitForSeconds(insertInterval);
        }

        // ЗАБИРАЕМ КЛЮЧИ У ИГРОКА
        GameManager.keysCollected -= slotsToActivate;

        // Защита от отрицательных чисел
        if (GameManager.keysCollected < 0)
        {
            GameManager.keysCollected = 0;
        }
        // Если все ключи вставлены — показываем экран победы
        if (GameManager.keysCollected <= 0 && slotsToActivate >= GameManager.keysNeeded)
        {
            // Находим экран победы на сцене и включаем его
            WinScreen winScreen = FindObjectOfType<WinScreen>();
            if (winScreen != null)
            {
                winScreen.ShowWinScreen();
            }
        }
    }
}