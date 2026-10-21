using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Запоминает количество ключей при старте уровня
public class LevelStart : MonoBehaviour
{
    private void Start()
    {
        GameManager.SaveLevelStart();
    }
}