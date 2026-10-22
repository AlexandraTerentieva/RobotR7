using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Главное меню: кнопки Новая игра, Продолжить, Выход
public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        // Возвращаем курсор мыши (чтобы можно было кликать по кнопкам)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Кнопка "Новая игра" (запускает Level_1 с нуля)
    public void StartNewGame()
    {
        // Сбрасываем прогресс (ключи и список)
        GameManager.keysCollected = 0;
        GameManager.collectedKeyIDs.Clear();

        // Загружаем первый уровень (по индексу 1, так как 0 — это меню)
        SceneManager.LoadScene(1);
    }

    // Кнопка "Продолжить" (запускает последний уровень, где был игрок)
    public void ContinueGame()
    {
        // Пока просто загружаем Level_1 (позже можно добавить сохранение сцены)
        SceneManager.LoadScene(1);
    }

    // Кнопка "Выход"
    public void QuitGame()
    {
        Application.Quit(); // Закрывает игру (в редакторе не работает, только в билде)
    }
}