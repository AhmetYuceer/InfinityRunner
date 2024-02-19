using DG.Tweening;
using System.Collections;
using UnityEngine;

public enum CharacterPosition
{
    left,
    middle,
    right
}

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private CharacterPosition currentCharacterPosition;
    private const float leftPositionValue = 1.3f;
    private const float middlePositionValue = 3.8f;
    private const float rightPositionValue = 6.3f;
    private const float gravity = -9.81f;

    private Animator animator;
    private CharacterController characterController;

    public bool isMove;
    private float targetX;
    [Header("MOVEÝNG")]
    [SerializeField] private bool isHorizontalMove;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float upSpeedAmount;
    [SerializeField] private float horizontalSpeed;

    [Header("Gravity")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    private Vector3 velocity;

    [Header("Jump")]
    public float jumpHeight = 3f;  
    [SerializeField] public bool isJump;
    [SerializeField] private float jumpDelay;

    [Header("Slide")]
    [SerializeField] private bool isSlide;
    [SerializeField] private float slideDelay;

    public bool isCanPressKey { get; set;}

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        DOTween.Clear(true);
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        currentCharacterPosition = CharacterPosition.middle;
        targetX = middlePositionValue;

        isMove = false;
        isCanPressKey = true;
    }

    private void Update()
    {
        if (Input.anyKeyDown && !GameManager.Instance.isPlay && isCanPressKey)
        {
            isMove = true;
            animator.SetBool("Run", true);
            GameManager.Instance.StartGame(); 
            return;
        }

        if (!isCanPressKey)
        {
            return;
        }
        
        Gravity();
        if (isMove)
        {
            PlayerInputs();

            if (isHorizontalMove)
            {
                float distanceToTarget = targetX - transform.position.x;
                Vector3 movement = new Vector3(Mathf.Sign(distanceToTarget) * horizontalSpeed * Time.deltaTime, 0, 0);
               
                characterController.Move(movement);
                if (Mathf.Abs(distanceToTarget) < 0.1f)
                {
                    var pos = transform.position;
                    pos.x = targetX;
                    transform.position = pos;
                    isHorizontalMove = false;
                }
            }
        }
    }

    private void PlayerInputs()
    {
        bool leftInput = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        bool rightInput = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
        bool jumpInput = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space);
        bool slideInput = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

        characterController.Move(new Vector3(0, 0, forwardSpeed) * Time.deltaTime);

        if (leftInput)
        {
            ChangeCharacterPositiom(CharacterPosition.left);
        }
        if (rightInput)
        {
            ChangeCharacterPositiom(CharacterPosition.right);
        }
        if (jumpInput && !isJump)
        {
            StartCoroutine(Jump());
        }
        if (slideInput && !isSlide)
        {
            StartCoroutine(Slide());
        }
    }

    private IEnumerator Jump()
    {
        if (isGrounded)
        {
            isJump = true;
            animator.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            yield return new WaitForSeconds(jumpDelay);
            isJump = false;
        }
    }

    private IEnumerator Slide()
    {
        isSlide = true;
        if (isGrounded)
        {
            animator.SetTrigger("Slide");
        }
        else
        {
            animator.SetTrigger("Slide");
            velocity.y = -20f;
        }
        velocity.y += gravity * Time.deltaTime;

        yield return new WaitForSeconds(slideDelay);

        isSlide = false;
    }

    public void UpSpeed()
    {
        forwardSpeed += upSpeedAmount;
    }

    private void Gravity()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);
        animator.SetBool("Falling", !isGrounded);

        if (!isGrounded)
        {
            velocity.y += gravity * 2 * Time.deltaTime;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
            if (velocity.y < 0)
            {
                velocity.y = -2f;
            }
        }
        characterController.Move(velocity * Time.deltaTime);
    }

    private void ChangeCharacterPositiom(CharacterPosition _characterPosition)
    {
        switch (_characterPosition)
        {
            case CharacterPosition.left:

                if (CheckCharacterPosition(_characterPosition))
                {
                    if (currentCharacterPosition == CharacterPosition.middle)
                    {
                        currentCharacterPosition = CharacterPosition.left;
                        targetX = leftPositionValue;
                    }
                    else
                    {
                        currentCharacterPosition = CharacterPosition.middle;
                        targetX = middlePositionValue;
                    }
                }
                break;
            case CharacterPosition.right:
                if (CheckCharacterPosition(_characterPosition))
                {
                    if (currentCharacterPosition == CharacterPosition.left)
                    {
                        currentCharacterPosition = CharacterPosition.middle;
                        targetX = middlePositionValue;
                    }
                    else
                    {
                        currentCharacterPosition = CharacterPosition.right;
                        targetX = rightPositionValue;
                    }
                }
                break;
            default:
                break;
        }
        isHorizontalMove = true;
    }

    public void ReturnToTakeDamagePosition()
    {
        float targetX = 0;
        switch (currentCharacterPosition)
        {
            case CharacterPosition.left:
                targetX = leftPositionValue;
                break;
            case CharacterPosition.middle:
                targetX = middlePositionValue;
                break;
            case CharacterPosition.right:
                targetX = rightPositionValue;
                break;
        }
        var pos = transform.position;
        pos.x = targetX;
        pos.y = 1f;
        transform.position = pos;
    }

    private bool CheckCharacterPosition(CharacterPosition _characterPosition)
    {
        if (currentCharacterPosition != _characterPosition)
        {
            return true;
        }
        return false;
    }
}