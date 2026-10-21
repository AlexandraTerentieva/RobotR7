using System.Collections.Generic;
using UnityEngine;

// Хранит общий прогресс игры и список собранных ключей
public class GameManager : MonoBehaviour
{
    public static int keysCollected = 0;   // Сколько ключей собрано
    public static int keysNeeded = 10;     // Сколько нужно всего

    public static int keysAtLevelStart = 0; // Сколько было в начале уровня

    // Список ID всех собранных ключей (чтобы они не появлялись снова)
    public static List<string> collectedKeyIDs = new List<string>();

    private void Awake()
    {
        // Этот объект не удалится при переходе на другую сцену
        DontDestroyOnLoad(gameObject);
    }

    // Запоминает, сколько ключей было в начале уровня
    public static void SaveLevelStart()
    {
        keysAtLevelStart = keysCollected;
    }

    // Откатывает ключи при смерти игрока
    public static void RollbackKeys()
    {
        keysCollected = keysAtLevelStart; // Откатываем счетчик
        collectedKeyIDs.Clear();          // Очищаем список, чтобы ключи вернулись
    }

    // Проверяет, был ли уже собран ключ с таким ID
    public static bool IsKeyCollected(string id)
    {
        return collectedKeyIDs.Contains(id);
    }

    // Добавляет ключ в список собранных
    public static void AddKeyID(string id)
    {
        if (!collectedKeyIDs.Contains(id))
        {
            collectedKeyIDs.Add(id);
        }
    }
}