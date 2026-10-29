using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Гигантский робот: стреляет в игрока, пока тот близко
public class BossRobot : MonoBehaviour
{
    [Header("Настройки")]
    public GameObject bulletPrefab;
    public Transform firePoint;       // Точка вылета пули
    public float fireRate = 1.5f;
    public float attackRange = 15f;
    public float bulletSpeed = 15f;

    [Header("Статус")]
    public static bool isBossAlive = true;

    private Transform player;
    private float nextFireTime = 0f;
    private EnemyHealth bossHealth;

    private void Start()
    {
        isBossAlive = true;
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
        bossHealth = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        if (!isBossAlive || player == null) return;
        if (bossHealth != null && bossHealth.currentHealth <= 0)
        {
            isBossAlive = false;
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > attackRange) return;

        // Босс поворачивается только по горизонтали (чтобы не наклонялся)
        Vector3 lookDir = player.position - transform.position;
        lookDir.y = 0; // Фиксируем высоту для поворота тела
        if (lookDir != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), Time.deltaTime * 3f);
        }

        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // Создаем пулю в точке выстрела
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Принудительно направляем пулю прямо в игрока
            bullet.transform.LookAt(player.position + Vector3.up * 1f); // +1f, чтобы целиться чуть выше ног

            // Даем скорость
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = bullet.transform.forward * bulletSpeed;
            }
        }
    }
}