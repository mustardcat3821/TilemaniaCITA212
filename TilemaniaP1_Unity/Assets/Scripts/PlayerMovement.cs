using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    bool isAlive = true;

    Vector2 moveInput;

    Rigidbody2D myRigidBody;
    BoxCollider2D feetCollider;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        feetCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Run();
        FlipSprite();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if(!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if(!isAlive)
        {
            return;
        }

        if (value.isPressed)
        {
            Jump();
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, myRigidBody.linearVelocity.y);
        myRigidBody.linearVelocity = playerVelocity;
    }

    void Jump()
    {
        Vector2 jumpVelocity = new Vector2(myRigidBody.linearVelocity.x, jumpSpeed);
        myRigidBody.linearVelocity = jumpVelocity;
    }

    void FlipSprite()
    {
        bool hasHorizontalSpeed = Mathf.Abs(myRigidBody.linearVelocity.x) > Mathf.Epsilon;

        if (hasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidBody.linearVelocity.x), 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            isAlive = false;
            print("Player has taken damage!");
            // Handle player death logic here
        }

    }
}
