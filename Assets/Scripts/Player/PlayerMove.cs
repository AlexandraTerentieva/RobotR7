using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Управление игроком: ходьба, прыжок, присед (Ctrl), бег (Shift)
public class PlayerMove : MonoBehaviour
{
    [Header("Движение")]
    public float speed = 5f;
    public float runSpeed = 8f;      // Скорость бега (с 4 уровня)
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    [Header("Присед")]
    public float crouchSpeed = 2f;
    public float crouchHeight = 1f;
    public float normalHeight = 0.8f;

    [Header("Камера")]
    public Transform cameraTransform;
    public float mouseSensitivity = 100f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float xRotation = 0f;
    private bool isCrouching = false;
    private bool canRun = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        // Проверяем уровень: бег только на 4 и 5
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Level_4" || currentScene == "Level_5")
        {
            canRun = true;
        }
    }

    private void Update()
    {
        // Поворот камеры
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // ПРИСЕД (на Left Ctrl)
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (!isCrouching)
            {
                isCrouching = true;
                cameraTransform.localPosition = new Vector3(0, crouchHeight, 0);
            }
        }
        else
        {
            if (isCrouching)
            {
                isCrouching = false;
                cameraTransform.localPosition = new Vector3(0, normalHeight, 0);
            }
        }

        // Проверка земли
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        // Движение
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Выбор скорости
        float currentSpeed = speed;
        if (isCrouching)
        {
            currentSpeed = crouchSpeed;
        }
        else if (canRun && Input.GetKey(KeyCode.LeftShift)) // БЕГ (на Left Shift)
        {
            currentSpeed = runSpeed;
        }

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Прыжок
        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Гравитация
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}