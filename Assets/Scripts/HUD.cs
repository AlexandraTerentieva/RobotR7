using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Отображает HUD: ключи, HP, управление
public class HUD : MonoBehaviour
{
    public TextMeshProUGUI keyCounterText; // Ссылка на текст ключей
    public TextMeshProUGUI hpText;         // Ссылка на текст HP

    private PlayerHealth playerHealth;     // Ссылка на здоровье игрока

    private void Start()
    {
        // Находим игрока и его здоровье
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    private void Update()
    {
        // Обновляем счётчик ключей
        if (keyCounterText != null)
        {
            keyCounterText.text = $"Ключи: {GameManager.keysCollected} / {GameManager.keysNeeded}";
        }

        // Обновляем HP
        if (hpText != null && playerHealth != null)
        {
            hpText.text = $"HP: {playerHealth.currentHealth} / {playerHealth.maxHealth}";
        }
    }
}