using System.Collections.Generic;
using UnityEngine;

// Хранит прогресс игры (ключи, секреты и т.д.)
public class GameManager : MonoBehaviour
{
    // --- Ключи ---
    public static int keysCollected = 0;   // Сколько ключей собрано
    public static int keysNeeded = 10;     // Сколько нужно всего
    public static int keysAtLevelStart = 0; // Сколько было в начале уровня
    public static List<string> collectedKeyIDs = new List<string>(); // Список собранных ключей

    // --- Секреты (Мета-механика) ---
    public static int secretsCollected = 0; // Сколько секретов собрано
    public static int secretsNeeded = 4;    // Сколько нужно для портала
    public static int secretsAtLevelStart = 0; // Сколько было в начале уровня

    private static GameManager instance; // Ссылка на единственный экземпляр

    private void Awake()
    {
        // Защита от дубликатов: если GameManager уже существует, удаляем этот
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Иначе делаем этот объект главным
        instance = this;
        DontDestroyOnLoad(gameObject); // Не удаляем при переходе между сценами
    }

    // Вызывается при старте каждого уровня (скриптом LevelStart)
    public static void SaveLevelStart()
    {
        // ЗАЩИТА: Если мы перешли на новый уровень, и у нас уже есть собранные секреты,
        // мы запоминаем их. Это не дает сбросить прогресс в ноль.
        if (secretsCollected > 0)
        {
            secretsAtLevelStart = secretsCollected;
        }

        keysAtLevelStart = keysCollected; // Запоминаем ключи

        Debug.Log($"[GameManager] Сохранение старта: Ключей = {keysCollected}, Секретов = {secretsCollected}");
    }

    // Вызывается при смерти игрока (откат прогресса)
    public static void RollbackKeys()
    {
        keysCollected = keysAtLevelStart; // Возвращаем ключи
        collectedKeyIDs.Clear();          // Очищаем список, чтобы ключи появились на уровне заново

        secretsCollected = secretsAtLevelStart; // Возвращаем секреты

        Debug.Log($"[GameManager] Откат: Ключей = {keysCollected}, Секретов = {secretsCollected}");
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