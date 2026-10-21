using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Портал: переводит игрока на следующий уровень по индексу
public class Portal : MonoBehaviour
{
    public int nextSceneIndex; // Индекс сцены (в Build Settings)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextSceneIndex);
            Debug.Log($"Переход на сцену с индексом: {nextSceneIndex}");
        }
    }
}