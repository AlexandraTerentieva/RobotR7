using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Здоровье игрока: получение урона, смерть и рестарт
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;    // Максимальное здоровье игрока
    public int currentHealth;     // Текущее здоровье

    [Header("UI Смерти")]
    public GameObject deathScreen;    // Панель "Вы умерли" (назначается в инспекторе)
    public TextMeshProUGUI deathText; // Текст на панели

    private bool isDead = false;       // Умер ли игрок (чтобы не умирал дважды)
    private bool isInvulnerable = false; // Неуязвимость (чтобы не умереть при загрузке)

    private void Start()
    {
        currentHealth = maxHealth; // В начале здоровье полное
        if (deathScreen != null) deathScreen.SetActive(false); // Прячем экран смерти
        StartCoroutine(StartInvulnerability()); // Даем 1 секунду неуязвимости
    }

    private IEnumerator StartInvulnerability()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(1f); // Ждем 1 секунду
        isInvulnerable = false;
    }

    // Вызывается, когда игрок получает урон
    public void TakeDamage(int damage)
    {
        if (isDead || isInvulnerable) return; // Если уже мертв или неуязвим — игнорируем

        currentHealth -= damage; // Отнимаем здоровье
        Debug.Log($"Игрок получил {damage} урона. Осталось: {currentHealth}");

        if (currentHealth <= 0) Die(); // Если здоровье 0 — умираем
    }

    // Смерть игрока
    private void Die()
    {
        isDead = true;
        Debug.Log("Игрок умер!");

        // 1. Включаем анимацию смерти
        Animator anim = GetComponentInChildren<Animator>();
        if (anim != null)
        {
            anim.SetTrigger("Die"); // Если в аниматоре настроен триггер "Die"
            anim.SetBool("Dead", true); // Если настроен параметр Dead
        }

        // 2. Выключаем управление, чтобы игрок не мог ходить
        if (GetComponent<CharacterController>() != null)
        {
            GetComponent<CharacterController>().enabled = false;
        }

        // 3. Показываем экран смерти
        if (deathScreen != null) deathScreen.SetActive(true);
        if (deathText != null) deathText.text = "Вы умерли!\nПопробуйте еще раз...";

        // 4. Ждем 2 секунды и перезапускаем
        StartCoroutine(RestartLevel());
    }

    // Перезапуск уровня с откатом ключей
    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(2f); // Ждем 2 секунды
        GameManager.RollbackKeys(); // Откатываем ключи
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Перезагружаем сцену
    }
}