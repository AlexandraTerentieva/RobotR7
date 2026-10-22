using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Меню паузы: останавливает игру и показывает кнопки
public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel; // Панель с кнопками (назначим в инспекторе)

    private bool isPaused = false; // На паузе ли игра

    private void Start()
    {
        // При старте игры панель скрыта, курсор спрятан
        pausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Если нажали Esc — включаем или выключаем паузу
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    // Поставить на паузу
    public void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);      // Показываем панель
        Time.timeScale = 0f;             // Останавливаем время в игре
        Cursor.lockState = CursorLockMode.None; // Показываем курсор
        Cursor.visible = true;
    }

    // Продолжить игру
    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);     // Скрываем панель
        Time.timeScale = 1f;             // Возвращаем время
        Cursor.lockState = CursorLockMode.Locked; // Прячем курсор
        Cursor.visible = false;
    }

    // Перезапустить уровень
    public void RestartLevel()
    {
        Time.timeScale = 1f; // Обязательно возвращаем время, иначе новый уровень зависнет
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Выйти в главное меню
    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}