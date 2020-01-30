using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{

    [Header("Gestion de la collision")]
    
    public LayerMask whatIsPlayer;
    public Transform playerCheck_1;
    public Transform playerCheck_2;
    public float checkRadius;
    private bool Player = false;
    public BoxCollider2D Coll;
    public SpriteRenderer Sprite;
    private bool pop = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Player == true)
        { 
            Coll.enabled = false;
            Sprite.enabled = false;
            StartCoroutine(TimeDestroy());
        }
    }

    void FixedUpdate(){
        Player = Physics2D.OverlapArea(playerCheck_1.position, playerCheck_2.position, whatIsPlayer); //collision
    }

    private IEnumerator TimeDestroy()
    {
        yield return new WaitForSeconds(1);
        Sprite.enabled = true;
        Coll.enabled = true;
    }
}
