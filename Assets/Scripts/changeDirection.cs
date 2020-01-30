using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeDirection : MonoBehaviour
{
    public string direction;
    public event Action onDirectionChange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            foreach (GameObject cam in GameObject.FindGameObjectsWithTag("vCam"))
            {
                cam.GetComponent<Place_player>().direction = direction;
            }
            //GameObject.FindGameObjectWithTag("vCam").GetComponent<Place_player>().direction = direction;
        }
    }


}
