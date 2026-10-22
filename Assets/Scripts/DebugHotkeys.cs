using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Дебаг-хоткеи для тестирования
public class DebugHotkeys : MonoBehaviour
{
    private float holdTimer = 0f; // Таймер удержания тильды

    private void Update()
    {
        // --- РЕСТАРТ УРОВНЯ (УДЕРЖАНИЕ ТИЛЬДЫ) ---
        // Если зажата клавиша ~ (тильда или ё)
        if (Input.GetKey(KeyCode.BackQuote))
        {
            holdTimer += Time.unscaledDeltaTime; // Считаем время (unscaled — чтобы работало даже при timeScale = 0)

            // Если держим больше 1.5 секунд — перезапускаем
            if (holdTimer >= 1.5f)
            {
                Time.timeScale = 1f; // Возвращаем время на место
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        else
        {
            // Если отпустили — сбрасываем таймер
            holdTimer = 0f;
        }

        // --- ТАЙМСКЕЙЛ (TAB) ---
        // Если нажали TAB
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Если время идет (1) — останавливаем (0). Если стоит (0) — запускаем (1).
            if (Time.timeScale > 0)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
}