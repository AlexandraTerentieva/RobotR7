using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Этот скрипт висит на пустом объекте в начале каждого уровня
public class LevelStart : MonoBehaviour
{
    private void Start()
    {
        // Говорим GameManager'у запомнить текущее количество ключей и секретов
        GameManager.SaveLevelStart();
        Debug.Log("LevelStart сработал!");
    }
}