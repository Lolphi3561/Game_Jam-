using System.Threading;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

public class CharacterScript : MonoBehaviour
{
    // Allgemien
    public Rigidbody2D myRigidBody;
    public bool isAlive = true;
    public GameObject floorDeathBox;
    public GameObject respawnPoint;
    public Collision2D collision;
    public float speed = 10f;
    private int lastDirection = 0;

    // Springen
    private int jumpsLeft = 0;
    private bool isGrounded = false;
    public float jumpPower = 30;

    // WallCling
    private bool isWallSliding;
    private float wallSlidingSpeed = 1f;
    public Transform wallCheck;
    public LayerMask wallLayer;

    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private Vector2 wallJumpingPower = new Vector2(15f, 20f);


    // Dash
    public bool hasDash = true;
    public float dashCooldown = 0.75f;
    public float dashTimer = 0;
    public float dashBoost = 30;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        WallSlide();
        WallJump();

        Dash();
    }

    private void Movement()
    {
        // Totesfall
        if (isAlive == false)
        {
            transform.position = respawnPoint.transform.position;
            transform.rotation = respawnPoint.transform.rotation;
            isAlive = true;
        }


        if (Keyboard.current.spaceKey.wasPressedThisFrame && jumpsLeft > 0)
        {
            myRigidBody.linearVelocityY = jumpPower;
            jumpsLeft--;
        }

        if (Keyboard.current.aKey.isPressed)
        {
            if (myRigidBody.linearVelocityX > -speed)
            {
                myRigidBody.linearVelocityX = -speed;
            }
            lastDirection = -1;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            if (myRigidBody.linearVelocityX < speed)
            {
                myRigidBody.linearVelocityX = speed;
            }
            lastDirection = 1;
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (lastDirection == -1 && !Keyboard.current.aKey.isPressed || myRigidBody.linearVelocityX < -5)
        {
            myRigidBody.linearVelocityX += (float)75*Time.deltaTime;
            if (myRigidBody.linearVelocityX > 0)
            {
                myRigidBody.linearVelocityX = 0;
            }
        }
        else if (lastDirection == 1 && !Keyboard.current.dKey.isPressed || myRigidBody.linearVelocityX > 5)
        {
            myRigidBody.linearVelocityX -= (float)75*Time.deltaTime;
            if (myRigidBody.linearVelocityX < 0)
            {
                myRigidBody.linearVelocityX = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckGround(collision);

        if (collision.gameObject.CompareTag("DeathArea") || collision.gameObject.CompareTag("DeadlyProjectile"))
        {
            isAlive = false;
            transform.SetParent(null);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckGround(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isGrounded)
        {
            isGrounded = false;
            jumpsLeft = 1;
        }
    }

    private void CheckGround(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
                isWallSliding = false;
                jumpsLeft = 1;
                return;
            }
        }
    }

    private bool CheckWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if(CheckWall() && !isGrounded)
        {
            isWallSliding = true;
            myRigidBody.linearVelocity = new Vector2(myRigidBody.linearVelocity.x, Mathf.Clamp(myRigidBody.linearVelocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if(Keyboard.current.spaceKey.wasPressedThisFrame && wallJumpingCounter > 0f)
        {
            myRigidBody.linearVelocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if(transform.localScale.x != wallJumpingDirection)
            {
                lastDirection = -lastDirection;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }
    }

    private void Dash()
    {
        if (dashTimer < dashCooldown)
        {
            dashTimer += Time.deltaTime;
        }

        if (dashTimer >= dashCooldown && Keyboard.current.shiftKey.wasPressedThisFrame)
        {
            myRigidBody.linearVelocityX = dashBoost * lastDirection;
            dashTimer = 0;
        }
    }
}
