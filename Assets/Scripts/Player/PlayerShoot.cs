using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Стрельба игрока: ЛКМ стреляет пулей
public class PlayerShoot : MonoBehaviour
{
    [Header("Настройки")]
    public GameObject bulletPrefab;   // Префаб пули (можно взять от босса)
    public Transform cameraTransform; // Камера (чтобы стрелять туда, куда смотрим)
    public float bulletSpeed = 25f;
    public float fireRate = 0.2f;

    private float nextFireTime = 0f;

    private void Update()
    {
        // Если нажали ЛКМ и время пришло
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        if (bulletPrefab != null && cameraTransform != null)
        {
            Vector3 spawnPosition = cameraTransform.position + cameraTransform.forward * 1.5f;
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, cameraTransform.rotation);

            // ГОВОРИМ ПУЛЕ, ЧТО ОНА ИГРОКА (чтобы она не ранила самого игрока)
            BossBullet bulletScript = bullet.GetComponent<BossBullet>();
            if (bulletScript != null)
            {
                bulletScript.SetAsPlayerBullet();
            }

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = cameraTransform.forward * bulletSpeed;
            }
        }
    }
}