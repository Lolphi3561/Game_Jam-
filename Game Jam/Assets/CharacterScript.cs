using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

public class CharacterScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float jumpPower = 5;
    public int jumpCount = 2;
    public bool isAlive = true;
    public GameObject floorDeathBox;

    private bool lastDirectionLeft = true;
    public bool hasDash = true;
    public float dashCooldown = (float)1.25;
    public float dashTimer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive == false)
        {
            transform.position = new Vector3(0,8,1);
            isAlive = true;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame && jumpCount >= 1)
        {
            myRigidBody.linearVelocity = Vector2.up * jumpPower;
            jumpCount -= 1;
        }

        if (Keyboard.current.aKey.isPressed)
        {
            if (myRigidBody.linearVelocityX > -5)
            {
                myRigidBody.linearVelocityX = -5;
            }
            lastDirectionLeft = true;

        }
        else if (Keyboard.current.dKey.isPressed)
        {
            if(myRigidBody.linearVelocityX < 5)
            {
                myRigidBody.linearVelocityX = 5;
            }
            lastDirectionLeft = false;
        }
        if (lastDirectionLeft && !Keyboard.current.aKey.isPressed || myRigidBody.linearVelocityX < -5)
        {
            myRigidBody.linearVelocityX += (float)0.5;
            if(myRigidBody.linearVelocityX > 0)
            {
                myRigidBody.linearVelocityX = 0;
            }
        }
        else if (!lastDirectionLeft && !Keyboard.current.dKey.isPressed || myRigidBody.linearVelocityX > 5)
        {
            myRigidBody.linearVelocityX -= (float)0.5;
            if (myRigidBody.linearVelocityX < 0)
            {
                myRigidBody.linearVelocityX = 0;
            }
        }
        if (Keyboard.current.shiftKey.wasPressedThisFrame)
        {
            Dash();
        }
        if(dashTimer < dashCooldown)
        {
            dashTimer += Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                jumpCount = 2;
                break;
            }
        }

        if (collision.gameObject.CompareTag("DeathArea"))
        {
            isAlive = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(Keyboard.current.spaceKey.isPressed == false)
        {
            jumpCount -= 1;
        }
    }

    private void Dash()
    {
        if(dashTimer >= dashCooldown)
        {
            if (lastDirectionLeft)
            {
                myRigidBody.linearVelocityX -= 20;
            }
            else
            {
                myRigidBody.linearVelocityX += 20;
            }
            dashTimer = 0;
        }
    }
}
