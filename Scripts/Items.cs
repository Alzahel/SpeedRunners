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

    void Start()
    {
        
    }

    void Update()
    {
        if (Player == true){
            Destroy(gameObject);
            //Debug.Log ("detection");
        }
    }

    void FixedUpdate(){
        Player = Physics2D.OverlapArea(playerCheck_1.position, playerCheck_2.position, whatIsPlayer); //collision 
    }
}
