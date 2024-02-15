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
    [SerializeField] private float horizontalSpeed;
    public float backwardMovementDistance;

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
    }

    private void Update()
    {
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheckerTransform.position, checkerRadius);
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
                rb.AddForce(Vector3.up * jumpForce);
            }
        }
        if (slideInput && !isSlide)
        {
            if (isGrounded)
            {
                var velocity = rb.velocity;
                velocity.y = 0;
                rb.velocity = velocity;
                animator.SetTrigger("Slide");
            }
            else
            {
                var velocity = rb.velocity;
                velocity.y = -20;
                rb.velocity = velocity;
                animator.SetTrigger("Slide");
            }
            StartCoroutine(SlideDelay());
        }
    }
 
    private IEnumerator SlideDelay()
    {
        isSlide = true;
        yield return new WaitForSeconds(slideDelay);
        isSlide = false;
    }

    #region
   
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

    public float GetSpeed()
    {
        return forwardSpeed;
    }    
    public void SetSpeed(float speed)
    {
        forwardSpeed = speed;
    }

    public void SetRigidbodyIsKinematic(bool isKinematic)
    {
        rb.isKinematic = isKinematic;
    }
    #endregion
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