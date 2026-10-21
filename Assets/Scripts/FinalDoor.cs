using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

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

        Debug.Log($"Дверь забрала {slotsToActivate} ключей. Осталось у игрока: {GameManager.keysCollected}");

        // Тут можно добавить открытие двери
        if (GameManager.keysCollected <= 0 && slotsToActivate >= GameManager.keysNeeded)
        {
            Debug.Log("Все ключи отданы, дверь открывается!");
        }
    }
}