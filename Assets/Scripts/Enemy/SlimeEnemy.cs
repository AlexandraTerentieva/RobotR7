using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Враг-слизень: прыгает на игрока и сжимается
public class SlimeEnemy : MonoBehaviour
{
    [Header("Настройки движения")]
    public float jumpForce = 5f;      // Сила прыжка
    public float detectionRange = 8f; // Радиус обнаружения игрока
    public float jumpInterval = 1.5f; // Пауза между прыжками
    public int damage = 2;            // Сколько урона наносит

    private Transform player;         // Ссылка на игрока
    private float jumpTimer = 0f;     // Таймер до следующего прыжка
    private Vector3 startScale;       // Обычный размер слизня
    private Rigidbody rb;             // Физика слизня

    private void Start()
    {
        // Запоминаем обычный размер для анимации сжатия
        startScale = transform.localScale;

        // Находим игрока по тегу
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;

        // Берем компонент физики
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (player == null) return;

        // Считаем расстояние до игрока
        float distance = Vector3.Distance(transform.position, player.position);

        // Если игрок близко — прыгаем
        if (distance <= detectionRange)
        {
            jumpTimer -= Time.deltaTime; // Уменьшаем таймер

            // Если таймер прошел — прыгаем
            if (jumpTimer <= 0f)
            {
                JumpTowardsPlayer();
                jumpTimer = jumpInterval; // Сбрасываем таймер
            }
        }
    }

    private void JumpTowardsPlayer()
    {
        // Направление на игрока (только по горизонтали, чтобы не улететь в небо)
        Vector3 direction = (player.position - transform.position);
        direction.y = 0f;
        direction = direction.normalized;

        // Прыжок: добавляем силу вверх и вперед
        rb.velocity = new Vector3(direction.x * jumpForce, jumpForce, direction.z * jumpForce);

        // Запускаем анимацию сжатия (через корутину)
        StartCoroutine(SquishAnimation());
    }

    // Анимация сжатия при прыжке (сжимаем и разжимаем)
    private System.Collections.IEnumerator SquishAnimation()
    {
        // Сжимаемся перед прыжком
        transform.localScale = new Vector3(startScale.x * 1.2f, startScale.y * 0.8f, startScale.z * 1.2f);
        yield return new WaitForSeconds(0.1f);

        // Вытягиваемся в прыжке
        transform.localScale = new Vector3(startScale.x * 0.8f, startScale.y * 1.2f, startScale.z * 0.8f);
        yield return new WaitForSeconds(0.2f);

        // Возвращаем обычный размер
        transform.localScale = startScale;
    }

    // Если слизень коснулся игрока
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null) health.TakeDamage(damage);
        }
    }
}