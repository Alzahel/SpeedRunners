using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBoost : MonoBehaviour
{

    public bool affiche;
    public GameObject player;
    public SpriteRenderer Sprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        affiche = player.GetComponent<UseItems>().haveBoostSpeed;
        if (affiche)
        {
            Sprite.enabled = true;
        }
        else
        {
            Sprite.enabled = false;
        }
    }
}
