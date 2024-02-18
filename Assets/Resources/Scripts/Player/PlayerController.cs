using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private CharacterEnums.CharacterPosition currentPosition;
    private const float leftPositionValue = 1.3f;
    private const float middlePositionValue = 3.8f;
    private const float rightPositionValue = 6.3f;

    private Animator animator;
    private Rigidbody rb;

    [Header("MOVEÝNG")]
    private float targetX; 
    public bool isMove;
    [SerializeField] private bool isHorizontalMove;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float upSpeedAmount;
    [SerializeField] private float horizontalSpeed;

    [Header("Jump")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private float jumpForce;
    
    [SerializeField] private Transform groundCheckerTransform;
    [SerializeField] private float checkerRadius;
    [SerializeField] LayerMask groundLayer;

    [Header("Slide")]
    [SerializeField] private bool isSlide;
    [SerializeField] private float slideDelay;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        currentPosition = CharacterEnums.CharacterPosition.middle;

        isMove = false;
        animator.SetBool("Run", false);
    }

    private void Update()
    {
        if (Input.anyKey && !isMove && GameManager.Instance.isPlay)
        {
            isMove = true;
            UIManager.Instance.HideControls();
            animator.SetBool("Run", true);
        }
        if (!isMove)
        {
            return;
        }
        animator.SetBool("Falling", !isGrounded);

        Inputs();
        if (isHorizontalMove)
        {
            moveCharacterHorizontally();
        }
    }

    private void FixedUpdate()
    {
        if (isMove)
        {
            isGrounded = Physics.CheckSphere(groundCheckerTransform.position, checkerRadius, groundLayer);
            moveCharacterForward();
        }
    }

 
    private void moveCharacterForward()
    {
        Vector3 forwardMovement = transform.forward * forwardSpeed * Time.fixedDeltaTime;
        transform.position += forwardMovement;
        rb.MovePosition(transform.position);
    }

    private void moveCharacterHorizontally()
    {
        var pos = transform.position;
        pos.x = Mathf.Lerp(pos.x, targetX, horizontalSpeed * Time.fixedDeltaTime);
        transform.position = pos;
        if (Mathf.Abs(pos.x - targetX) < 0.1f)
        {
            isHorizontalMove = false;
            pos.x = targetX;
            transform.position = pos;
        }
    }

    private void Inputs()
    {
        bool leftInput = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        bool rightInput = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
        bool jumpInput = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space);
        bool slideInput = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

        if (leftInput)
        {
            ChangeCharacterPosition(CharacterEnums.CharacterPosition.left);
        }  
        if (rightInput)
        {
            ChangeCharacterPosition(CharacterEnums.CharacterPosition.right);
        }
        if (jumpInput && !isSlide)
        {
            if (isGrounded)
            {
                animator.SetTrigger("Jump");
                rb.AddForce(new Vector3(0.0f, 2.0f, 0.0f) * jumpForce , ForceMode.Impulse);
                isGrounded = false;
            }
        }
        if (slideInput && !isSlide)
        {
            StartCoroutine(SlideDelay());
        }
    }
 

    private IEnumerator SlideDelay()
    {
        rb.mass = 70;
        isSlide = true;

        var velocity = rb.velocity;
        velocity.y = -20;
        rb.velocity = velocity;

        animator.SetTrigger("Slide");
        yield return new WaitForSeconds(slideDelay);
        isSlide = false;
        rb.mass = 1;
    }


    private void ChangeCharacterPosition(CharacterEnums.CharacterPosition _characterPosition)
    {
        switch (_characterPosition)
        {
            case CharacterEnums.CharacterPosition.left:

                if (currentPosition != _characterPosition)
                {
                    if (currentPosition == CharacterEnums.CharacterPosition.middle)
                    {
                        currentPosition = CharacterEnums.CharacterPosition.left;
                        targetX = leftPositionValue;
                    }
                    else
                    {
                        currentPosition = CharacterEnums.CharacterPosition.middle;
                        targetX = middlePositionValue;
                    }
                    isHorizontalMove = true;
                }
                break;
            case CharacterEnums.CharacterPosition.right:

                if (currentPosition != _characterPosition)
                {
                    if (currentPosition == CharacterEnums.CharacterPosition.middle)
                    {
                        currentPosition = CharacterEnums.CharacterPosition.right;
                        targetX = rightPositionValue;
                    }
                    else
                    {
                        currentPosition = CharacterEnums.CharacterPosition.middle;
                        targetX = middlePositionValue;
                    }
                    isHorizontalMove = true;
                }
                break;
            default:
                break;
        }
    }

    public void SetIsKinematic(bool isKinematic)
    {
        rb.isKinematic = isKinematic;
    }

    public float GetSpeed()
    {
        return forwardSpeed;
    }    
    public void SetSpeed(float speed)
    {
        forwardSpeed = speed;
    }

    public void UpSpeed()
    {
        forwardSpeed += upSpeedAmount;
    }
}

public class CharacterEnums
{
    public enum CharacterPosition
    {
        left,
        middle,
        right
    }
}