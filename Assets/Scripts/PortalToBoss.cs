using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Портал к боссу: пускает только если собраны все секреты
public class PortalToBoss : MonoBehaviour
{
    public float openDistance = 3f; // На каком расстоянии срабатывает
    private Transform player;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    private void Update()
    {
        if (player == null) return;

        // Считаем расстояние по горизонтали
        Vector3 portalPos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 playerPos = new Vector3(player.position.x, 0, player.position.z);
        float distance = Vector3.Distance(portalPos, playerPos);

        // Если подошел
        if (distance <= openDistance)
        {
            if (GameManager.secretsCollected >= GameManager.secretsNeeded)
            {
                Debug.Log("Все секреты собраны! Переход к боссу...");
                SceneManager.LoadScene("Level_5");
            }
            else
            {
                Debug.Log($"Портал закрыт! Нужно секретов: {GameManager.secretsNeeded - GameManager.secretsCollected}");
            }
        }
    }
}