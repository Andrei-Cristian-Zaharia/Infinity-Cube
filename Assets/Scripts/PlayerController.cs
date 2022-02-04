using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Ground, NotGround}

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed;
    public float movementSpeed;
    public float jumpPower;

    public bool doubleJump = false;
    public bool canJump = true;
    public bool jump = false;
    public bool isGrounded;
    public bool canMove = true;
    private new Rigidbody2D rigidbody2D;

    public bool isMobile; // enable this on phone

    SwipeData swipeData;
    public Joystick joystick;
    private bool rightInput;

    public float moveInput;

    public static PlayerController instance;
    private float lastInput;

    public PlayerEyes PE;

    private void Awake()
    {
        instance = this;
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
    }

    private void SwipeDetector_OnSwipe(SwipeData data)
    {
        swipeData = data;
        // use the swipe
    }

    private void Start()
    {
        if (isMobile && joystick == null) joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();

        rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (var touch in Input.touches)
                if (touch.position.x > Screen.width / 2)
                {
                    rightInput = true;
                    break;
                }
        }

        if (canJump && ((isGrounded || doubleJump) && (Input.GetKeyDown(KeyCode.Space) || (rightInput && (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(1).phase == TouchPhase.Began)))))
            jump = true;

        if (!isMobile)
            moveInput = Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0;
        else moveInput = joystick.Horizontal * 2.5f;
    }

    private void FixedUpdate()
    {
        if (PE.state == PlayerState.Ground && moveInput == 0)
            PE.direction = RollDirection.None; 

        if (PE.state == PlayerState.NotGround && moveInput != 0)
        {
            if (moveInput > 0) PE.direction = RollDirection.Right;
            if (moveInput < 0) PE.direction = RollDirection.Left;
        }

        if (isGrounded && jump)
        {
            //rigidbody2D.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            rigidbody2D.velocity = Vector2.up * jumpPower;
            doubleJump = true;
            jump = false;
            rightInput = false;
        }

        if (moveInput != 0) lastInput = moveInput;

        if (!isGrounded && jump && doubleJump)
        {
            //rigidbody2D.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            rigidbody2D.velocity = Vector2.up * jumpPower;
            doubleJump = false;
            jump = false;
            rightInput = false;
        }

        if (canMove)
        {
            rigidbody2D.velocity = new Vector2(moveInput * movementSpeed, rigidbody2D.velocity.y);

            if (!isGrounded && moveInput < 0)
            {
                transform.Rotate(0, 0, rotationSpeed);
            }

            if (!isGrounded && moveInput > 0)
            {
                transform.Rotate(0, 0, -rotationSpeed);
            }

            if (!isGrounded && moveInput == 0)
            {
                if (!isGrounded && lastInput < 0)
                {
                    transform.Rotate(0, 0, rotationSpeed/2);
                }

                if (!isGrounded && lastInput > 0)
                {
                    transform.Rotate(0, 0, -rotationSpeed/2);
                }
            }
        }

        rightInput = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Goal") || collision.CompareTag("MovingPlatform") || collision.CompareTag("JumpPlatform"))
        {
            isGrounded = true;
            PE.state = PlayerState.Ground;
            doubleJump = false;
            canMove = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    { 
        if (collision.CompareTag("JumpPlatform"))
        {
            isGrounded = false;
            PE.state = PlayerState.NotGround;
            doubleJump = true;
        }

        if (collision.CompareTag("Ground") || collision.CompareTag("Goal") || collision.CompareTag("MovingPlatform"))
        {
            isGrounded = false;
            PE.state = PlayerState.NotGround;
        }
    }

    public void Knockback(Vector2 power)
    {
         rigidbody2D.AddForce(power);
    }


}
