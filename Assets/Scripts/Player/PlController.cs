using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlController : MonoBehaviour
{
    public float walkSpeed = 5f; // �������� ������
    public float runSpeed = 10f; // �������� ����
    public float staminaCurrent = 1f; // ����������������� ����
    public float staminaFull = 1f; // ������������ ����������������� ����
    public float speedStaminaMinus = 0.1f; // �������� ��������� ��� ����
    public float speedStaminaPlus = 0.1f; // �������� ����������� ������������
    public float speedCanRunStamina = 0.15f; // ����������� ��������, ��� ������� �������� ������ ���
    public bool canWalk = true;
    public bool canRun = true; // ����� ����� ������?
    public bool isRun = false; // ����� �����?
    public bool isWalk = false;

    private AudioSource audioSource;

    public MobileControler mobileControler; // ������ ��������� ��� ���������� � �������� (��������� ��� ���������� � ��� ��)
    public VariableJoystick variableJoystick; // ������ ��������� ��� ���������� � �������� (��������� ��� ���������� � ��� ��) (New)

    private CharacterController controller; // ��������� Unity, ����� ������� �������������� �������� ������
    private HeadBob headBobScript; // ������ ����������� ������� �������� ������ ������
    private float playerSpeed = 5f; // ������� �������� ������
    private float gravity = -9.81f; // �������� ������� ������ (����������)
    private Vector3 velocity; // ������ �������� ������ �� ��� Y
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

        controller.Move(move * playerSpeed * Time.deltaTime); // ������� ������ ������������ ��������� �������

        velocity.y += gravity * Time.deltaTime; // ��������� ���������� � ������
        controller.Move(velocity * Time.deltaTime); // ������� ������ ������������ ��� Y

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