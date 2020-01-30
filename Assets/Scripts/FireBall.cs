using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    [Header("Speed")]
    
    public float FireBallSpeed;
    private Rigidbody2D rigidBody;

    [Header("Gestion de la collision")]
    //Verifie deux layer différent (player and ground)
    public LayerMask whatIsPlayer;
    public LayerMask whatIsGround;
    public Transform collisionCheck;
    public float checkRadius;
    private bool collision_Player = false;
    private bool collision_Plateform = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigidBody.velocity = new Vector2(FireBallSpeed * transform.localScale.x, 0); //Speed

        if ((collision_Player == true) || (collision_Plateform == true)){ // Verif collision
            Destroy(gameObject);
        }
    }

    void FixedUpdate(){
        collision_Plateform = Physics2D.OverlapCircle(collisionCheck.position, checkRadius, whatIsGround);
        collision_Player = Physics2D.OverlapCircle(collisionCheck.position, checkRadius, whatIsPlayer);
    }

}

