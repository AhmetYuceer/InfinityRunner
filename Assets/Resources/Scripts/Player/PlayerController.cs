using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private const float leftPositionValue = 1.3f;
    private const float middlePositionValue = 3.8f;
    private const float rightPositionValue = 6.3f;

    private Animator animator;
    private Rigidbody rb;

    private CharacterEnums.CharacterPosition playerPosition;
    private CharacterEnums.CharacterState playerState;

    [Header("Move")]
    private bool isMoving;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float speedIncreaseAmount;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private bool isGrounded;
    [SerializeField] private Transform checkSpherePos;
    [SerializeField] private float checkSphereRadius;
    [SerializeField] private LayerMask groundMask;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        playerState = CharacterEnums.CharacterState.run;
        playerPosition = CharacterEnums.CharacterPosition.middle;
        ChangePosition(playerPosition);
        ChangeAnimationState(playerState);
    }

    void Update()
    {
        if (isMoving)
        {
            Moveing();
        }
        PlayerInputs();
    }

    private void FixedUpdate()
    {
        Vector3 movement = transform.forward * movementSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);

        isGrounded = Physics.CheckSphere(checkSpherePos.position, checkSphereRadius, groundMask);
        animator.SetBool("isGrounded", isGrounded);
    }

    public void SetRigidbodyIsKinematic(bool isKinematic)
    {
        rb.isKinematic = isKinematic;
    }

    public float GetSpeed()
    {
        return movementSpeed;
    }

    public void SetSpeed(float speed)
    {
        movementSpeed = speed;
    }

    private void PlayerInputs()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            switch (playerPosition)
            {
                case CharacterEnums.CharacterPosition.middle:
                    ChangePosition(CharacterEnums.CharacterPosition.left);
                    break;
                case CharacterEnums.CharacterPosition.right:
                    ChangePosition(CharacterEnums.CharacterPosition.middle);
                    break;
                default:
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            switch (playerPosition)
            {
                case CharacterEnums.CharacterPosition.left:
                    ChangePosition(CharacterEnums.CharacterPosition.middle);
                    break;
                case CharacterEnums.CharacterPosition.middle:
                    ChangePosition(CharacterEnums.CharacterPosition.right);
                    break;  
                default:
                    break;
            }
        }
        if ( isGrounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)))
        {
            ChangeAnimationState(CharacterEnums.CharacterState.jump);

        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeAnimationState(CharacterEnums.CharacterState.slide);
        }    
    }


    Vector3 playerPos;
    private void ChangePosition(CharacterEnums.CharacterPosition characterPosition)
    {
        playerPos = transform.position;
        playerPosition = characterPosition;

        switch (playerPosition)
        {
            case CharacterEnums.CharacterPosition.left:
                playerPos.x = leftPositionValue;
                break;
            case CharacterEnums.CharacterPosition.middle:
                playerPos.x = middlePositionValue;
                break;
            case CharacterEnums.CharacterPosition.right:
                playerPos.x = rightPositionValue;
                break;
            default:
                break;
        }
        isMoving = true;
    }

    private void Moveing()
    {
        float newXPosition = Mathf.Lerp(transform.position.x, playerPos.x, 10 * Time.deltaTime);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

        if (Mathf.Abs(transform.position.x - playerPos.x) <= 0.01f)
        {
            isMoving = false;
        }
    }

    private IEnumerator Jump()
    {
        animator.SetBool("Jump", true);
        animator.SetBool("Run", false);
        rb.AddForce(Vector3.up * jumpForce);
        yield return new WaitForSeconds(0.2f);
        ChangeAnimationState(CharacterEnums.CharacterState.run);
    }
    private IEnumerator Slide()
    {
        animator.SetBool("Run", false);
        animator.SetBool("Slide", true);
        var velocity = rb.velocity;
        velocity.y = -15;
        rb.velocity= velocity;
        yield return new WaitForSeconds(0.2f);
        velocity.y = 0;
        rb.velocity = velocity;
        animator.SetBool("Slide", false);
        animator.SetBool("Run", true);
    }

    private void ChangeAnimationState(CharacterEnums.CharacterState characterState)
    {
        playerState = characterState;
        switch (playerState)
        {
            case CharacterEnums.CharacterState.idle:
                //isplay kapat
                break;
            case CharacterEnums.CharacterState.run:
                animator.SetBool("Run", true);
                animator.SetBool("Jump", false);
                break;
            case CharacterEnums.CharacterState.jump:
                if (isGrounded)
                {
                    StartCoroutine(Jump());
                }
                break;
            case CharacterEnums.CharacterState.slide:
                StartCoroutine(Slide());
                break;
            default:
                break;
        } 
    }

    //skor 5 in katlarýna uþaltýðý zaman hýz, "speedIncreaseAmount" deðeri kadar artar
    public void IncreaseSpeed()
    {
        movementSpeed += speedIncreaseAmount;
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
    public enum CharacterState
    {
        idle,
        run,
        jump,
        slide
    }
}