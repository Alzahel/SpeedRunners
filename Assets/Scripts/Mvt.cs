using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    A faire :

    - changer les detection en tag pour eviter le jump infinis sur le bors des murs.
     
*/



public class Mvt : MonoBehaviour
{
    
    [Header("Forces values")]
    
    public float speed;
    public float jumpForce;
    public float regularGravity;
    public float slideGravity;
    private float moveInput;
    private float memoryJumpForce;

    
    [Header("Binding controls")]
    // config touches

    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode slide;

    //gravity

    
    [Header("Jump")]
    //Jump
    
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    private int extraJumps;
    public int extraJumpsValue;

    [Header("Animation")]
    //Animation
    
	public Animator anim;

    // Variable Slide

    [Header("RigidBody")]
    //RB

    public Rigidbody2D rb;
    
    [Header("Slide")]
    //Slide
    
    public BoxCollider2D regularColl_1;
    public CircleCollider2D regularColl_2;
    public CircleCollider2D slideColl;

    [Header("WallClimb")]

    public bool isTouchingWall;
    public Transform wallCheck;
    public float wallCheckDistance;
    public bool isWallSliding;
    public float wallSlideSpeed;
    public float wallSlideFastSpeed;		
	public float wallSlideNormalSpeed;
    public float wallClimbSpeed;

    [Header("WallJump")]

    public Vector2 wallJumpDirection;
    public float wallJumpForce;
    public bool canTouchWall = true;
    public bool jumpPushBefore = false;

    [Header("Ceilling")]		
			
	public bool haveCelling;		
	public Transform ceillingCheck;

    [Header("Debug")]
    //Debug

    public float facingDirection = 1;
    public bool facingRight = true;
    public bool isSliding = false;
    public bool isGrounded;
    public bool isMoving;
    public bool canMove = true;
    public bool canChangeMoveInput = true;

    void Start()
    {
        extraJumps = extraJumpsValue;
        memoryJumpForce = jumpForce;
        rb = GetComponent<Rigidbody2D>();
        wallJumpDirection.Normalize();
        wallSlideSpeed = wallSlideNormalSpeed;
    }

    void Update()
    {
        //Jump
        if (canMove)
        {
            if (isGrounded == true)
            {
                extraJumps = 1;
            }

            if (Input.GetKeyDown(jump) && extraJumps > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                extraJumps--;
            }
            else if (Input.GetKeyDown(jump) && extraJumps == 0 && isGrounded == true)
            {
                rb.velocity = Vector2.up * jumpForce;
            }
            if (((isWallSliding || isTouchingWall) && (Input.GetKeyDown(jump) || jumpPushBefore)) || (isWallSliding || isTouchingWall) && Input.GetKey(jump))
            {
                StartCoroutine(jumpPushBeforeCoroutine());
                if (((facingRight == true && Input.GetKey(left)) || (facingRight == false && Input.GetKey(right))))
                {
                    isWallSliding = false;
                    StartCoroutine(WaitJumpWall());
                    rb.velocity = new Vector2(wallJumpForce * wallJumpDirection.x * -facingDirection, wallJumpForce * wallJumpDirection.y);
                }
            }
        }
        
        // Slide update

        if (Input.GetKey(slide)){
            preformSlide();
        }

        if (!Input.GetKey(slide)){
            stopSlide ();
        }


        if (isSliding == true && isGrounded == true){
            GetComponent<Rigidbody2D>().gravityScale = slideGravity;
            jumpForce = 0f;
        }else if (isSliding == false){
            GetComponent<Rigidbody2D>().gravityScale = regularGravity;
            jumpForce = memoryJumpForce;
        }

        
        if (canChangeMoveInput)
        {
            if (Input.GetKey(left))
            {
                moveInput = -1;
            }
            if (Input.GetKey(right))
            {
                moveInput = 1;
            }
            if (Input.GetKey(left) && Input.GetKey(right))
            {
                moveInput = 0;
            }
            if (!Input.GetKey(left) && !Input.GetKey(right))
            {
                moveInput = 0;
            }
        }
        if (isTouchingWall && isWallSliding)
        {
            if (((facingRight == true && Input.GetKey(left)) || (facingRight == false && Input.GetKey(right))))
            {
                StartCoroutine(canChangeMoveInputCoroutine());
            }
        }   

        /*
        Animation :
        */

        //Animation idle

        if (!Input.anyKey) {
			isMoving = false;
			anim.SetBool ("IsMove", false);
		}

        // Animation run
        
        if (Input.GetKey (right)) {
			isMoving = true;
			anim.SetBool ("IsMove", true);
		} else if (Input.GetKey (left)) {
			isMoving = true;
			anim.SetBool ("IsMove", true);
		}

        CheckIfWallSliding();
    }


    void FixedUpdate()
    {
        //Check pour le sol
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        //check pour les murs
        haveCelling = Physics2D.OverlapCircle(ceillingCheck.position, checkRadius, whatIsGround);

        if (canTouchWall)
        {
            isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        }
        else
        {
            isTouchingWall = false;
        }
        

        mouvements();

        // Appel de la fonction flip pour le flip du personnage

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        } else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }


    private void mouvements()
    {
        if (canMove)
        {
            //Move Speed
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

            if (isWallSliding)
            {
                if (rb.velocity.y < wallClimbSpeed && Input.GetKey(jump))
                {
                    extraJumps = 0;
                    rb.velocity = new Vector2(rb.velocity.x, wallClimbSpeed);
                }
                else if (rb.velocity.y < -wallSlideSpeed)
                {
                    extraJumps = 0;
                    rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
                }
            }
        }
    }



    // Retourne le personnage
    void Flip()
    {
        if (!isWallSliding)
        {
            facingDirection *= -1;
            facingRight = !facingRight;
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }
    }

    // Slide Function

    private void preformSlide()
    {

        if (isWallSliding && isTouchingWall)
        {
            wallSlideSpeed = wallSlideFastSpeed;
        }
        else if (!isTouchingWall)
        {
            isSliding = true;
            anim.SetBool("IsSlide", true);
            regularColl_1.enabled = false;
            regularColl_2.enabled = false;
            slideColl.enabled = true;
        }
    }

    private void stopSlide(){
        if (isWallSliding && isTouchingWall)
        {
            wallSlideSpeed = wallSlideNormalSpeed;
        }
        else if (!haveCelling && isSliding)
        {
            anim.Play("Idle");
            anim.SetBool("IsSlide", false);
            regularColl_1.enabled = true;
            regularColl_2.enabled = true;
            slideColl.enabled = false;
            isSliding = false;
        }
    }

    private void CheckIfWallSliding() 
    {
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    IEnumerator WaitJumpWall()
    {
        canMove = false;
        yield return new WaitForSeconds(0.2f);
        canMove = true;
    }

    IEnumerator canChangeMoveInputCoroutine()
    {
        canChangeMoveInput = false;
        moveInput = 0;
        yield return new WaitForSeconds(0.18f);
        canChangeMoveInput = true;
        canTouchWall = false;
        yield return new WaitForSeconds(0.1f);
        canTouchWall = true;
    }

    IEnumerator jumpPushBeforeCoroutine()
    {
        jumpPushBefore = true;
        yield return new WaitForSeconds(0.18f);
        jumpPushBefore = false;
    }
}