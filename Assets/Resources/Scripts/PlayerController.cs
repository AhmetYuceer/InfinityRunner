using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private float moveSpeed;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    
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
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        PlayerInputs();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
        if (isGrounded)
        {
            ChangeAnimationState(CharacterEnums.CharacterState.run);
        }
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
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
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
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            ChangeAnimationState(CharacterEnums.CharacterState.jump);
        }  
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeAnimationState(CharacterEnums.CharacterState.slide);
        }
    }

    private IEnumerator Slide()
    {
        animator.SetBool("Slide", true);    
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("Slide", false);
    }

    private void Jump()
    {
        if (isGrounded && !animator.GetBool("Jump"))
        {
            rb.AddForce(transform.up * jumpForce);
            animator.SetBool("Run", false);
            animator.SetBool("Jump", true);
        }
    }
    
    private void ChangePosition(CharacterEnums.CharacterPosition characterPosition)
    {
        var playerPos = this.transform.position;
        playerPosition = characterPosition;

        float targetXPosition = 0f;

        switch (playerPosition)
        {
            case CharacterEnums.CharacterPosition.left:
                targetXPosition = leftPositionValue;
                break;
            case CharacterEnums.CharacterPosition.middle:
                targetXPosition = middlePositionValue;
                break;
            case CharacterEnums.CharacterPosition.right:
                targetXPosition = rightPositionValue;
                break;
            default:
                break;
        }
        playerPos.x = targetXPosition;
        this.transform.position = playerPos;
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
                    Jump();
                break;
            case CharacterEnums.CharacterState.slide:
                    StartCoroutine(Slide());
                break;
            default:
                break;
        }
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