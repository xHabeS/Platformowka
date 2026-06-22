using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public float groundSpeed;
    public float jumpSpeed;
    public float acceleration;

    [Range(0f, 1f)]
    public float groundDecay;
    public Rigidbody2D body;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    public bool grounded;

    public float knockbackForce;
    public float knockbackDuration;
    public float knockbackTime;
    public bool knockRight;

    float xInput;
    float yInput;

    void Update()
    {
        GetInput();
        HandleJump();
        animator.SetFloat("AirSpeedY", body.linearVelocity.y);
    }
        void FixedUpdate()
    {
        if (knockbackDuration <= 0)
        {
            MoveWithInput();
        }
        else
        {
            if(knockRight == true)
            {
                body.linearVelocity = new Vector2(-knockbackForce, knockbackForce);
                animator.SetTrigger("Hurt");
            }
            if(knockRight == false)
            {
                body.linearVelocity = new Vector2(knockbackForce, knockbackForce);
                animator.SetTrigger("Hurt");
            }
            knockbackDuration -= Time.deltaTime;
        }
        CheckGround();
        ApplyFriction();
    }

    void MoveWithInput()
    {
        if (Mathf.Abs(xInput) > 0)
        {
           animator.SetInteger("AnimState", 1);
           float increment = acceleration * xInput;
           float newSpeed = Mathf.Clamp(body.linearVelocity.x + increment, -groundSpeed, groundSpeed);
           body.linearVelocity = new Vector2(newSpeed, body.linearVelocity.y);
           FaceInput();
        }
        else
        {
            animator.SetInteger("AnimState", 0);
        }
    }

    void FaceInput()
    {
        float direction = Mathf.Sign(xInput);
        transform.localScale = new Vector3(direction, 1, 1);
    }
    void HandleJump()
    {
        if (yInput > 0 && grounded)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed);
            animator.SetTrigger("Jump");
        }
    }
    void GetInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
    }

    void CheckGround()
    {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
        if (grounded)
        {
            animator.SetBool("Grounded", true);
        }
        else
        {
            animator.SetBool("Grounded", false);
        }
    }
    void ApplyFriction()
    {
        if (grounded && xInput == 0 && body.linearVelocity.y <= 0)
        {
            body.linearVelocity *= groundDecay;
        }
    }
}
