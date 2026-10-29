using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Меню паузы: останавливает игру, показывает кнопки и прячет прицел
public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel; // Панель с кнопками паузы (назначается в инспекторе)
    public GameObject crosshair;  // Прицел на экране (точка в центре)

    private bool isPaused = false; // Флаг: на паузе ли игра

    private void Start()
    {
        // При старте игры принудительно прячем панель паузы
        if (pausePanel != null) pausePanel.SetActive(false);

        // Прячем курсор мыши в центр и блокируем его (чтобы не мешал играть)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Проверяем, нажал ли игрок клавишу Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Нажата кнопка паузы!");

            // Если игра на паузе — снимаем её. Если нет — ставим.
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Метод для постановки игры на паузу
    public void PauseGame()
    {
        isPaused = true;

        // Включаем панель паузы
        if (pausePanel != null) pausePanel.SetActive(true);

        // Прячем прицел, чтобы он не мешал смотреть на кнопки
        if (crosshair != null) crosshair.SetActive(false);

        // Останавливаем время в игре
        Time.timeScale = 0f;

        // Показываем курсор мыши
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Метод для снятия паузы
    public void ResumeGame()
    {
        isPaused = false;

        // Выключаем панель паузы
        if (pausePanel != null) pausePanel.SetActive(false);

        // Показываем прицел обратно
        if (crosshair != null) crosshair.SetActive(true);

        // Возвращаем время в игру
        Time.timeScale = 1f;

        // Прячем курсор
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Метод для перезапуска текущего уровня
    public void RestartLevel()
    {
        // Возвращаем время (иначе новый уровень зависнет)
        Time.timeScale = 1f;

        // Перезагружаем текущую сцену
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Метод для выхода в главное меню
    public void QuitToMenu()
    {
        // Возвращаем время
        Time.timeScale = 1f;

        // Загружаем сцену главного меню
        SceneManager.LoadScene("MainMenu");
    }
}