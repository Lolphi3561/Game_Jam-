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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive == false)
        {
            transform.position = new Vector3(0,10,1);
            isAlive = true;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame && jumpCount >= 1)
        {
            myRigidBody.linearVelocity = Vector2.up * jumpPower;
            jumpCount -= 1;
        }

        if (Keyboard.current.aKey.isPressed)
        {
            myRigidBody.linearVelocityX = -5;
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            myRigidBody.linearVelocityX = 5;
        }
        else
        {
            myRigidBody.linearVelocityX = 0;
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
}
