using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Здоровье игрока
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;

    [Header("UI Смерти")]
    public GameObject deathScreen;
    public TextMeshProUGUI deathText;

    private bool isDead = false;
    private bool isInvulnerable = false; // Неуязвимость

    private void Start()
    {
        currentHealth = maxHealth;
        if (deathScreen != null) deathScreen.SetActive(false);

        // Включаем неуязвимость на 1 секунду при старте
        StartCoroutine(StartInvulnerability());
    }

    private IEnumerator StartInvulnerability()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(1f); // 1 секунда
        isInvulnerable = false;
    }

    public void TakeDamage(int damage)
    {
        if (isDead || isInvulnerable) return; // Если мертв или неуязвим — игнорируем

        currentHealth -= damage;
        Debug.Log($"Игрок получил {damage} урона. Осталось: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Игрок умер!");

        if (deathScreen != null) deathScreen.SetActive(true);
        if (deathText != null) deathText.text = "Вы умерли!\nПопробуйте еще раз...";

        GetComponent<CharacterController>().enabled = false;

        StartCoroutine(RestartLevel());
    }

    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(2f);

        GameManager.RollbackKeys();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}