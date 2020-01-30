using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
A faire :

Changer les items checker lors du mvt slide!!!

*/

public class UseItems : MonoBehaviour
{
    [Header("BoxCollider")]
    //Pour changer les items check en fontion de la situation du personnage (slide ou mvt)

    public BoxCollider2D regularColl;
    public BoxCollider2D slideColl;
    

    [Header("Check Interaction")]
    //Take items

    public Transform ItemsCheck1;
    public Transform ItemsCheck2;
    public float checkPlace;

    [Header("Layer Interaction")]

    public LayerMask whatIsItems;
    private bool Items = false;
    public LayerMask whatIsFireBall;
    public bool fireBall = false;
    


    [Header("Fire Ball")]
    //FireBall

    public GameObject FireBall;//prefab
    public Transform TrowPoint;
    public KeyCode throwBall;
    public bool haveBall;
    public float TimeStun;
    private float memorySclale;
    private bool stunt = false;

    [Header("Boost Speed")]
    //boost speed

    public GameObject Player;
    public float boostSpeed;
    private float SaveSpeed;

    private Rigidbody2D rb;

    void Start()
    {
        memorySclale = FireBall.transform.localScale.x;
        haveBall = false;
        rb = GetComponent<Rigidbody2D>();
        SaveSpeed = Player.GetComponent<Mvt>().speed ;
    }

    void Update()
    {
        
        if (Input.GetKeyDown(throwBall) && haveBall == true && stunt == false){
            GameObject fireballClone = (GameObject)Instantiate (FireBall , TrowPoint.position, TrowPoint.rotation);//Creer un clone de la prefab FireBall
            haveBall = false;
            //Gestion du sens de la fireBall
            if (transform.localScale.x < 0){
                fireballClone.transform.localScale = new Vector3(fireballClone.transform.localScale.x * -1,fireballClone.transform.localScale.y ,fireballClone.transform.localScale.z );
            } else {
                fireballClone.transform.localScale = new Vector3(memorySclale, fireballClone.transform.localScale.y, fireballClone.transform.localScale.z);
            }
            
        }

        if (Items == true){//take ball
            haveBall = true;
        }

        if (fireBall == true){//receive a fireball
            StartCoroutine(Stunt());
        }
    }

    void FixedUpdate(){
        Items = Physics2D.OverlapArea(ItemsCheck1.position, ItemsCheck2.position, whatIsItems);//take Items
        fireBall = Physics2D.OverlapArea(ItemsCheck1.position, ItemsCheck2.position, whatIsFireBall);//received a ball
    }

    
    IEnumerator Stunt(){
        rb.velocity = new Vector3(0,0,0);
        GetComponent<Mvt> ().enabled = false;
        yield return new WaitForSeconds (TimeStun);
        GetComponent<Mvt> ().enabled = true;
    }

}
