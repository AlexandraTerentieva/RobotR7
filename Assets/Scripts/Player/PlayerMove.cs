using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Управление от третьего лица: камера сзади, робот ходит по WASD
public class PlayerMove : MonoBehaviour
{
    [Header("Движение")]
    public float speed = 5f;
    public float runSpeed = 8f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    public float turnSpeed = 100f; // Скорость поворота робота

    [Header("Присед")]
    public float crouchSpeed = 2f;

    [Header("Камера")]
    public Transform cameraTransform;
    public float mouseSensitivity = 100f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float xRotation = 0f;
    private bool isCrouching = false;
    private bool canRun = false;
    private Animator anim;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;

        // Проверяем, на каком мы уровне (для бега)
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Level_4" || currentScene == "Level_5") canRun = true;
    }

    private void Update()
    {
        // --- 1. КАМЕРА (только наклон вверх/вниз) ---
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -10f, 50f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // --- 2. ПОВОРОТ РОБОТА (мышкой влево/вправо) ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);

        // --- 3. ДВИЖЕНИЕ (WASD) ---
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Присед на Ctrl
        isCrouching = Input.GetKey(KeyCode.LeftControl);
        float currentSpeed = isCrouching ? crouchSpeed : (canRun && Input.GetKey(KeyCode.LeftShift) ? runSpeed : speed);

        // Двигаем робота вперед/назад (z) и вбок (x)
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // --- 4. АНИМАЦИИ ---
        if (anim != null)
        {
            // Передаем в аниматор, движется ли робот (0 - стоит, 1 - бежит)
            anim.SetFloat("Speed", move.magnitude);
            anim.SetBool("Grounded", isGrounded);
        }

        // --- 5. ПРЫЖОК ---
        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}