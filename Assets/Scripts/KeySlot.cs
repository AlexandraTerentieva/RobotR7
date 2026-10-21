using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Слот: ждет команды и загорается
public class KeySlot : MonoBehaviour
{
    public Material glowMaterial; // Желтый материал

    // Этот метод вызовет дверь, чтобы зажечь слот
    public void TurnOn()
    {
        Renderer r = GetComponent<Renderer>();
        if (r != null && glowMaterial != null)
        {
            r.material = glowMaterial;
            Debug.Log($"Слот {gameObject.name} загорелся!");
        }
    }
}