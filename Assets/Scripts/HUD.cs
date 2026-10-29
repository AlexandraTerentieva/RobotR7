using UnityEngine;
using TMPro;

// Отображает HP, ключи и секреты на экране
public class HUD : MonoBehaviour
{
    [Header("Тексты UI")]
    public TextMeshProUGUI keyCounterText; // Текст для ключей
    public TextMeshProUGUI hpText;         // Текст для HP
    public TextMeshProUGUI secretsText;    // Текст для секретов (НОВОЕ)

    private PlayerHealth playerHealth;     // Ссылка на здоровье игрока

    private void Start()
    {
        FindPlayer();
    }

    private void Update()
    {
        // Если игрок не найден (например, только загрузились) — ищем его
        if (playerHealth == null)
        {
            FindPlayer();
        }

        // 1. Обновляем счётчик ключей
        if (keyCounterText != null)
        {
            keyCounterText.text = $"Ключи: {GameManager.keysCollected} / {GameManager.keysNeeded}";
        }

        // 2. Обновляем HP
        if (hpText != null && playerHealth != null)
        {
            hpText.text = $"HP: {playerHealth.currentHealth} / {playerHealth.maxHealth}";
        }

        // 3. Обновляем счётчик секретов (НОВОЕ)
        if (secretsText != null)
        {
            secretsText.text = $"Секреты: {GameManager.secretsCollected} / {GameManager.secretsNeeded}";
        }
    }

    // Метод для поиска игрока на сцене
    private void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }
}