using System.Collections;
using UnityEngine;

// Ключ: при подборе исчезает и больше не появляется
public class Key : MonoBehaviour
{
    private string keyID; // Уникальный ID ключа (берется из имени объекта)

    private void Start()
    {
        // Запоминаем имя объекта как ID
        keyID = gameObject.name;

        // Если этот ключ уже был собран ранее — удаляем его
        if (GameManager.IsKeyCollected(keyID))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Если ключ подобрал игрок
        if (other.CompareTag("Player"))
        {
            // Увеличиваем счетчик и запоминаем ID
            GameManager.keysCollected++;
            GameManager.AddKeyID(keyID);

            // Удаляем ключ со сцены
            Destroy(gameObject);
        }
    }
}