using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Экран победы: показывается, когда игрок прошёл игру
public class WinScreen : MonoBehaviour
{
    public GameObject winPanel; // Панель с поздравлением (назначим в инспекторе)

    // Этот метод вызовет финальная дверь
    public void ShowWinScreen()
    {
        winPanel.SetActive(true);           // Показываем панель
        Time.timeScale = 0f;                // Останавливаем игру
        Cursor.lockState = CursorLockMode.None; // Показываем курсор
        Cursor.visible = true;
    }

    // Кнопка "В главное меню"
    public void GoToMenu()
    {
        Time.timeScale = 1f; // Возвращаем время
        SceneManager.LoadScene("MainMenu");
    }
}