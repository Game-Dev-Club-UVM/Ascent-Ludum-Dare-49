using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundObjects;
    [SerializeField] private float checkRadius;

    private Rigidbody2D rb;
    private bool facingRight = true;
    private float moveDirection;
    private bool isJumping = false;
    private bool isGrounded = false;

    // Awake is called before the Start, in the step phase in untiy
    private void Awake()
	{
        rb = GetComponent<Rigidbody2D>();
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();

        Animations();
    }

	//Physics and movement Update
    void FixedUpdate()
	{
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);

        Move();
    }


	//user Inputs
	private void Inputs()
	{
        moveDirection = Input.GetAxis("Horizontal");
		if (Input.GetButtonDown("Jump"))
		{
            isJumping = true;
		}
    }

    //animation
    private void Animations()
	{
        //character sprite face the direction they are moving
        if ((moveDirection > 0 && !facingRight) || (moveDirection < 0 && facingRight))
        {
            FlipCharacter();
        }
    }

    //movement
    private void Move()
	{
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
		if (isJumping && isGrounded)
		{
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
		}
    }

    private void FlipCharacter()
	{
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

	}
}
