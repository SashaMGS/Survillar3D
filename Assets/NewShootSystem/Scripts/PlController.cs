using System.Collections;
using UnityEngine;

namespace NewShootingSystem
{
    public class PlController : MonoBehaviour
    {
        public bool isMobile = false;
        [Header("����������� ���������")]
        [Tooltip("������������ ����������� ������ ���������")]
        public bool canWalkPlayer = true;
        [Tooltip("������������ ����������� ���� ���������")]
        public bool canRunPlayer = true;
        [Tooltip("������������ ����������� ���������� ���������")]
        public bool canCrouchPlayer = true;
        [Tooltip("������������ ����������� ������ ���������")]
        public bool canLiePlayer = true;
        [Tooltip("����� �� ������������ ���� ������?")]
        public bool canDoubleJump = false;
        [Tooltip("����� �� ������?")]
        public bool canFly = false;
        [Tooltip("���������� ������")]
        public float gravityFly = -15f;

        [Header("���������� �������� ���������")]
        [Tooltip("�������� ������")]
        public float walkSpeed = 5f; // �������� ������
        [Tooltip("�������� ����")]
        public float runSpeed = 10f; // �������� ����
        [Tooltip(" �������� � �������")]
        public float crouchSpeed = 2.5f;
        [Tooltip(" �������� � �������")]
        public float lieSpeed = 1f;

        [Header("���������� ���������� ���������")]
        [Tooltip("���� ������.")]
        [SerializeField] private float jumpForce = 6f;
        [Tooltip("�����������. ���� ���������� ������ � �����")]
        [SerializeField] public float gravity = -12f; // �������� ������� ������ (����������)

        [Header("���������� ������� ���������")]
        [Tooltip("����������������� ����")]
        public float staminaCurrent = 1f; // ����������������� ����
        [Tooltip("����������� ��������, ��� ������� �������� ������ ���")]
        public float speedCanRunStamina = 0.15f; // ����������� ��������, ��� ������� �������� ������ ���
        [Tooltip("������������ ����������������� ����")]
        public float staminaFull = 1f; // ������������ ����������������� ����
        [Tooltip("�������� ��������� ��� ����")]
        public float speedStaminaMinus = 0.1f; // �������� ��������� ��� ����
        [Tooltip("�������� ����������� ������������")]
        public float speedStaminaPlus = 0.1f; // �������� ����������� ������������

        [Header("������ ������������ ���������")]
        [Tooltip("������ ������")]
        public KeyCode jumpKeyCode = KeyCode.Space;
        [Tooltip("������ ����������")]
        public KeyCode crouchKeyCode = KeyCode.C;
        [Tooltip("������ ����")]
        public KeyCode lieKeyCode = KeyCode.X;

        [HideInInspector]
        public bool canRun = true; // ����� ����� ������?
        [HideInInspector]
        public bool isRun = false; // ����� �����?
        private bool isWalk = false;
        private bool isCrouching = false;
        private bool isUpBlock = false;
        private bool isLie = false;
        public bool doubleJumpCur = false;

        private AudioSource audioSource;

        public float distanceRayDown = 1.5f;
        private float distanceRayUp = 1.5f;

        private CharacterController controller; // ��������� Unity, ����� ������� �������������� �������� ������
        private HeadBob headBobScript; // ������ ����������� ������� �������� ������ ������
        [HideInInspector]
        public bool isOnGround = false;
        private float playerSpeed = 5f; // ������� �������� ������
        private Vector3 velocity; // ������ �������� ������ �� ��� Y
        private float moveHorizontal; // x
        private float moveVertical; // z

        //public VariableJoystick variableJoystick; // ������ ��������� ��� ���������� � �������� (��������� ��� ���������� � ��� ��) (New)

        void Start()
        {
            if (canFly)
                gravity = gravityFly;
            //
            if (GetComponent<AudioSource>() != null)
            {
                audioSource = GetComponent<AudioSource>();
                StartCoroutine(SoundWalk());
            }
            controller = GetComponent<CharacterController>();
            headBobScript = GetComponentInChildren<HeadBob>();
        }

        void Update()
        {
            if (Input.GetKey(jumpKeyCode) && isOnGround && !isCrouching && !isLie)
            {
                doubleJumpCur = true;
                velocity.y = jumpForce;
            }
            if (Input.GetKeyDown(jumpKeyCode) && !isOnGround && !isCrouching && !isLie && doubleJumpCur && canDoubleJump)
            {
                doubleJumpCur = false;
                velocity.y = jumpForce;
            }
            if (Input.GetKey(crouchKeyCode) && isOnGround)
                Crouch();
            if (Input.GetKey(lieKeyCode) && isOnGround)
                Lie();

            Walk();
            Run();
            StaminaVoid();
            Events();
        }
        private void FixedUpdate()
        {
            GravityVoid();
        }

        void Events()
        {
            Ray ray = new Ray(transform.position, transform.up);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, distanceRayUp))
            {
                isUpBlock = true;
                Debug.DrawRay(transform.position, transform.up, Color.red);
            }
            else
            {
                isUpBlock = false;
                Debug.DrawRay(transform.position, transform.up, Color.green);
            }

            Ray ray1 = new Ray(transform.position, -transform.up);

            if (Physics.SphereCast(ray1, 0.5f, out hit, distanceRayDown))
                isOnGround = true;
            else
                isOnGround = false;
        }

        void Lie()
        {
            if (isRun && canLiePlayer)
            {
                isRun = false;
                headBobScript.isRun = false;
            }

            if (isLie && !isUpBlock)
            {
                playerSpeed = walkSpeed;
                isLie = false;
                controller.height = 2;
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (!isLie && canLiePlayer)
            {
                playerSpeed = lieSpeed;
                isLie = true;
                controller.height = 0.5f;
                transform.localScale = new Vector3(1f, 0.25f, 1f);
            }
        }

        void Crouch()
        {
            if (isRun && canCrouchPlayer)
            {
                isRun = false;
                headBobScript.isRun = false;
            }

            if (isCrouching && !isUpBlock)
            {
                playerSpeed = walkSpeed;
                isCrouching = false;
                controller.height = 2;
            }
            else if (!isCrouching && canCrouchPlayer)
            {
                playerSpeed = crouchSpeed;
                isCrouching = true;
                controller.height = 1;
            }
        }

        void Walk()
        {
            if (canWalkPlayer)
            {
                if (!isMobile)
                {
                    moveHorizontal = Input.GetAxis("Horizontal"); // x
                    moveVertical = Input.GetAxis("Vertical"); // z
                }
                else
                {

                }
            }
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && canWalkPlayer)
                isWalk = true;
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
                isWalk = false;

            Vector3 move = transform.right * moveHorizontal + transform.forward * moveVertical;

            controller.Move(move * playerSpeed * Time.deltaTime); // ������� ������ ������������ ��������� �������
        }

        void GravityVoid()
        {
            if (!isOnGround)
                velocity.y += gravity * Time.deltaTime; // ��������� ���������� � ������

            controller.Move(velocity * Time.deltaTime); // ������� ������ ������������ ��� Y
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
            if (Input.GetKeyDown(KeyCode.LeftShift) && canRun && staminaCurrent > speedCanRunStamina
                && isCrouching && !isUpBlock && !isLie && canRunPlayer)
            {
                isCrouching = false;
                controller.height = 2;
                playerSpeed = runSpeed;
                isRun = true;
                headBobScript.isRun = true;
            }
            if (canRun && Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching && staminaCurrent > speedCanRunStamina && canRunPlayer && !isLie)
            {
                playerSpeed = runSpeed;
                isRun = true;
                headBobScript.isRun = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching && !isLie)
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
}