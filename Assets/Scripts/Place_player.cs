using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Place_player : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    private List<GameObject> players;
    GameObject firstPlayer;
    float firstPlayerX;
    float firstPlayerY;
    private GameObject playerToFollow;
    public CinemachineVirtualCamera cam;
    public CinemachineVirtualCamera cam2;
    public string direction;
    // Start is called before the first frame update
    void Start()
    {
        players = new List<GameObject>();
        //GameObject.FindGameObjectsWithTag("Player");
        players.Add(player1);
        players.Add(player2);
    }

    // Update is called once per frame
    void Update()
    {
        firstPlayerX = 0;
        for (int i = 0;  i < players.Count; i++ )
        {
            if (players[i].activeSelf == true)
            {
                if ((firstPlayerX == 0 || players[i].transform.position.x > firstPlayerX) && direction == "right")
                {
                    firstPlayerX = players[i].transform.position.x;
                    firstPlayer = players[i];
                }
                if ((firstPlayerX == 0 || players[i].transform.position.x < firstPlayerX) && direction == "left")
                {
                    firstPlayerX = players[i].transform.position.x;
                    firstPlayer = players[i];
                }
                if ((firstPlayerY == 0 || players[i].transform.position.y > firstPlayerY) && direction == "up")
                {
                    firstPlayerY = players[i].transform.position.y;
                    firstPlayer = players[i];
                }
                if ((firstPlayerY == 0 || players[i].transform.position.y < firstPlayerY) && direction == "down")
                {
                    firstPlayerY = players[i].transform.position.y;
                    firstPlayer = players[i];
                }
            }

        }
        playerToFollow = firstPlayer;
        setCam();
    }

    void setCam()
    {
        //// cam.Follow = playerToFollow.transform   ;
        // cam.LookAt = playerToFollow.transform;
        //cam2.Priority = 11 ;
        if(firstPlayer != null && firstPlayer.name == "personnage_1") 
        {
            cam.enabled = true;
            cam2.enabled = false;
        }
        if (firstPlayer != null && firstPlayer.name == "personnage_2")
        {
            cam.enabled = false;
            cam2.enabled = true;
        }
    }
}
