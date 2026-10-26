using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;

    bool isAlive = true;

    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    BoxCollider2D feetCollider;
    CapsuleCollider2D myCapsuleCollider;
    float gravityScaleAtStart;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        feetCollider = GetComponent<BoxCollider2D>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
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

        bool hasHorizontalSpeed = Mathf.Abs(myRigidBody.linearVelocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", hasHorizontalSpeed);
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

    void ClimbLadder()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("isClimbing", false);
            myRigidBody.gravityScale = gravityScaleAtStart;
            return;
        }

        myRigidBody.gravityScale = 0f;
        Vector2 climbVelocity = new Vector2(myRigidBody.linearVelocity.x, moveInput.y * climbSpeed);
        myRigidBody.linearVelocity = climbVelocity;

        bool hasVerticalSpeed = Mathf.Abs(myRigidBody.linearVelocity.y) > Mathf.Epsilon;

        myAnimator.SetBool("isClimbing", hasVerticalSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Hazard"))
        {
            isAlive = false;
            print("Player has taken damage!");
            // Handle player death logic here
        }

    }
}
