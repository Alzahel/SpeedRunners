
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
    public float ItemsRandom;

    [Header("Layer Interaction")]

    public LayerMask whatIsItems;
    private bool Items = false;// deux bool pour eviter de prendre plusieurs items.
    public LayerMask whatIsFireBall;
    public bool fireBall = false;
    public LayerMask whatIsSlow;
    public bool Slow = false;
    


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
    public float timeBoost;
    public bool haveBoostSpeed;
    private float SaveSpeed;

    [Header("SlowSpeed")]

    public float slowSpeed;
    public bool haveSlowSeed;
    public float timeSlow;


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
            FireBallFunction();
        }

        if (Input.GetKeyDown(throwBall) && haveBoostSpeed == true && stunt == false){
            StartCoroutine(boostSpeedFunction());
            
        }

        if (Items == true && haveBall == false && haveBoostSpeed == false){//take ball
            ItemsRandom = Random.Range(1, 3);
            if (ItemsRandom == 1){
                haveBall = true;
            }
            if (ItemsRandom == 2){
                haveBoostSpeed = true;
            }
        }

        if (fireBall == true){//receive a fireball
            StartCoroutine(Stunt());
        }
    }

    void FixedUpdate(){
        Items = Physics2D.OverlapArea(ItemsCheck1.position, ItemsCheck2.position, whatIsItems);//take Items
        fireBall = Physics2D.OverlapArea(ItemsCheck1.position, ItemsCheck2.position, whatIsFireBall);//received a ball
        Slow = Physics2D.OverlapArea(ItemsCheck1.position, ItemsCheck2.position, whatIsSlow);//your are in smoke
    }

    private void FireBallFunction(){
        haveBall = false;
        Items = false;
        GameObject fireballClone = (GameObject)Instantiate (FireBall , TrowPoint.position, TrowPoint.rotation);//Creer un clone de la prefab FireBall
        SoundManager.PlaySound(SoundManager.Sound.LaunchFireBall, transform.transform.position);
        //Gestion du sens de la fireBall
        if (transform.localScale.x < 0){
            fireballClone.transform.localScale = new Vector3(fireballClone.transform.localScale.x * -1,fireballClone.transform.localScale.y ,fireballClone.transform.localScale.z );
        } else {
            fireballClone.transform.localScale = new Vector3(memorySclale, fireballClone.transform.localScale.y, fireballClone.transform.localScale.z);
        }
    }

    IEnumerator boostSpeedFunction(){
        Items = false;
        haveBoostSpeed = false;
        Player.GetComponent<Mvt>().speed += boostSpeed;
        SoundManager.PlaySound(SoundManager.Sound.LoadSprint, transform.transform.position);
        yield return new WaitForSeconds (timeBoost);
        SoundManager.PlaySound(SoundManager.Sound.UnloadSprint, transform.transform.position);
        Player.GetComponent<Mvt>().speed = SaveSpeed;
    }
    
    IEnumerator Stunt(){
        rb.velocity = new Vector3(0,0,0);
        GetComponent<Mvt> ().enabled = false;
        yield return new WaitForSeconds (TimeStun);
        GetComponent<Mvt> ().enabled = true;
    }

}
