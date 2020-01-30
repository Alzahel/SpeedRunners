using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Rigidbody2D rb;

    
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

    public Rigidbody2D rigidBody;
    
    [Header("Slide")]
    //Slide
    
    public BoxCollider2D regularColl;
    public BoxCollider2D slideColl;
    
    [Header("Debug")]
    //Debug

    public bool facingRight = true;
    public bool IsSliding = false;
    public bool isGrounded;
    public bool isMoving;


    void Start()
    {
        extraJumps = extraJumpsValue;
        memoryJumpForce = jumpForce;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        //Jump

        if (isGrounded == true){
            extraJumps = 1;
        }

        if (Input.GetKeyDown(jump) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        } else if (Input.GetKeyDown(jump) && extraJumps == 0 && isGrounded == true){
            rb.velocity = Vector2.up * jumpForce;
        }

        // Slide update

        if (Input.GetKeyDown(slide)){
            preformSlide ();
        }

        if (IsSliding == true){
            GetComponent<Rigidbody2D>().gravityScale = slideGravity;
            jumpForce = 0f;
        }else if (IsSliding == false){
            GetComponent<Rigidbody2D>().gravityScale = regularGravity;
            jumpForce = memoryJumpForce;
        }

        //Mvt left and right

        if (Input.GetKey (right)) {
            moveInput = 1;
		} else if (Input.GetKey (left)) {
            moveInput = -1;
		} else {
            moveInput = 0;
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

    }

    void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Appel de la fonction flip pour le flip du personnage

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        } else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }


    // Retourne le personnage
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    
    // Slide Function

    private void preformSlide(){

        IsSliding = true;
        anim.SetBool("IsSlide",true);
        regularColl.enabled = false;
        slideColl.enabled = true;
        StartCoroutine ("stopSlide");
    }

    IEnumerator stopSlide(){
        yield return new WaitForSeconds (0.8f);
        anim.Play ("Idle");
        anim.SetBool ("IsSlide",false);
        regularColl.enabled = true;
        slideColl.enabled = false;
        IsSliding = false;
    }

}