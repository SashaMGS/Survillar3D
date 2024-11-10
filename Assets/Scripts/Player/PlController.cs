using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlController : MonoBehaviour
{
    public float walkSpeed = 5f; // Скорость ходьбы
    public float runSpeed = 10f; // Скорость бега
    public float staminaCurrent = 1f; // Продолжительность бега
    public float staminaFull = 1f; // Максимальная продолжительность бега
    public float speedStaminaMinus = 0.1f; // Скорость усталости при беге
    public float speedStaminaPlus = 0.1f; // Скорость восполнения выносливости
    public float speedCanRunStamina = 0.15f; // Минимальное значение, при котором возможно начать бег
    public bool canWalk = true;
    public bool canRun = true; // Игрок может бежать?
    public bool isRun = false; // Игрок бежит?
    public bool isWalk = false;

    private AudioSource audioSource;

    public MobileControler mobileControler; // Скрипт джойстика для управления с телефона (Необходим для управления и для пк)
    public VariableJoystick variableJoystick; // Скрипт джойстика для управления с телефона (Необходим для управления и для пк) (New)

    private CharacterController controller; // Компонент Unity, через которые осуществляется движение игрока
    private HeadBob headBobScript; // Скрипт позволяющий оживить движение головы игрока
    private float playerSpeed = 5f; // Текущая скорость игрока
    private float gravity = -9.81f; // Скорость падения игрока (Гравитация)
    private Vector3 velocity; // Вектор движения игрока по оси Y
    private float moveHorizontal; // x
    private float moveVertical; // z

    void Start()
    {
        //
        if (GetComponent<AudioSource>() != null)
            audioSource = GetComponent<AudioSource>();
        StartCoroutine(SoundWalk());
        controller = GetComponent<CharacterController>();
        headBobScript = GetComponentInChildren<HeadBob>();

        SaveAll saveAll = GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<SaveAll>();
        staminaFull += staminaFull * saveAll.saveItemsClass.skillMaxStamina * 0.2f;
        speedStaminaPlus += speedStaminaPlus * saveAll.saveItemsClass.skillSpeedUpStamina * 0.2f;
        walkSpeed += walkSpeed * saveAll.saveItemsClass.skillSpeedWalk * 0.025f;
        runSpeed += runSpeed * saveAll.saveItemsClass.skillSpeedRun * 0.025f;

        speedCanRunStamina += speedCanRunStamina * saveAll.saveItemsClass.skillMaxStamina * 0.2f;
    }

    void Update()
    {
        if (canWalk)
        {
            moveHorizontal = Input.GetAxis("Horizontal"); // x
            moveVertical = Input.GetAxis("Vertical"); // z
        }
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && canWalk)
            isWalk = true;
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            isWalk = false;
        Vector3 move = transform.right * moveHorizontal + transform.forward * moveVertical;

        controller.Move(move * playerSpeed * Time.deltaTime); // Двигаем игрока относительно заданного вектора

        velocity.y += gravity * Time.deltaTime; // Применяем гравитацию к игроку
        controller.Move(velocity * Time.deltaTime); // Двигаем игрока относительно оси Y

        Run();
        StaminaVoid();
    }

    void StaminaVoid()
    {
        if (staminaCurrent < 0)
        {
            staminaCurrent = 0;
            canRun = false;
            isRun = false;
            headBobScript.isRun = false;
            playerSpeed = walkSpeed;
        }

        if (isRun && staminaCurrent > 0)
            staminaCurrent -= speedStaminaMinus * Time.deltaTime;

        if (!isRun && staminaCurrent < staminaFull)
            staminaCurrent += speedStaminaPlus * Time.deltaTime;

        if (staminaCurrent > staminaFull)
            staminaCurrent = staminaFull;

        if (staminaCurrent >= speedCanRunStamina)
            canRun = true;
    }
    void Run()
    {
        if (canRun && Input.GetKeyDown(KeyCode.LeftShift) && isWalk && staminaCurrent > speedCanRunStamina)
        {

            playerSpeed = runSpeed;
            isRun = true;
            headBobScript.isRun = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerSpeed = walkSpeed;
            isRun = false;
            headBobScript.isRun = false;
        }
    }

    IEnumerator SoundWalk()
    {
        while (true)
        {
            if (isWalk && !isRun)
            {
                audioSource.Play();
                yield return new WaitForSeconds(0.5f);
            }
            if (isRun)
            {
                audioSource.Play();
                yield return new WaitForSeconds(0.3f);
            }
            yield return new WaitForFixedUpdate();
        }
    }
}