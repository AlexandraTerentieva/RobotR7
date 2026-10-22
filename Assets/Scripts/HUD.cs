using UnityEngine;
using TMPro;

// Отображает HP и ключи на экране
public class HUD : MonoBehaviour
{
    public TextMeshProUGUI keyCounterText; // Текст для ключей
    public TextMeshProUGUI hpText;         // Текст для HP

    private PlayerHealth playerHealth;     // Ссылка на здоровье игрока

    private void Start()
    {
        // Находим игрока на сцене по тегу
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Берем с него компонент здоровья
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    private void Update()
    {
        // Если текст ключей назначен — обновляем его
        if (keyCounterText != null)
        {
            keyCounterText.text = $"Ключи: {GameManager.keysCollected} / {GameManager.keysNeeded}";
        }

        // Если текст HP назначен и игрок найден — обновляем HP
        if (hpText != null && playerHealth != null)
        {
            hpText.text = $"HP: {playerHealth.currentHealth} / {playerHealth.maxHealth}";
        }
    }
}