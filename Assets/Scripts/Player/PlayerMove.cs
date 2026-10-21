using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Управление игроком от первого лица: ходьба, прыжок, присед, поворот камеры
public class PlayerMove : MonoBehaviour
{
    [Header("Настройки движения")]
    public float speed = 5f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    [Header("Настройки приседа")]
    public float crouchSpeed = 2f;   // Скорость в приседе
    public float crouchHeight = 1f;  // Высота камеры в приседе
    public float normalHeight = 0.8f; // Обычная высота камеры

    [Header("Настройки камеры")]
    public Transform cameraTransform;
    public float mouseSensitivity = 100f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float xRotation = 0f;
    private bool isCrouching = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // --- ПОВОРОТ КАМЕРЫ ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // --- ПРИСЕД ---
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Если ещё не приседаем — включаем
            if (!isCrouching)
            {
                isCrouching = true;
                // Смещаем камеру вниз
                cameraTransform.localPosition = new Vector3(0, crouchHeight, 0);
            }
        }
        else
        {
            // Если отпустили Shift — встаём
            if (isCrouching)
            {
                isCrouching = false;
                // Возвращаем камеру на место
                cameraTransform.localPosition = new Vector3(0, normalHeight, 0);
            }
        }

        // --- ДВИЖЕНИЕ ---
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Выбираем скорость: присед или обычная
        float currentSpeed = isCrouching ? crouchSpeed : speed;

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // --- ПРЫЖОК ---
        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // --- ГРАВИТАЦИЯ ---
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}