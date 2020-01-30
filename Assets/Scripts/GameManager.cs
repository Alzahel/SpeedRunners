using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Instantiation
    private string charToLoad;
    public int nbPlayers;
    public bool playingAlone = false;

    //Déroulement de la partie
    public int nbPlayersAlive;
    private GameObject lastPlayerAlive;

    //Fin de partie
    public GameObject winText;
    public GameObject restartText;
    private bool gameIsFinished = false;
    

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i == nbPlayers; i++)
        {
            charToLoad = "personnage_" + i;
         //   GameObject.Instantiate<charToLoad>
        }
    }

    // Update is called once per frame
    void Update()
    {
        DetermineNbPlayersAlive();
        if (nbPlayersAlive == 1)
        {
            EndTheGame();
            RestartTheGame();
        }
    }

    private void DetermineNbPlayersAlive()
    {
        nbPlayersAlive = 0;
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if(player.activeSelf == true)
            {
                nbPlayersAlive += 1;
                lastPlayerAlive = player;
            }
        }
    }

    private void EndTheGame()
    {
        if (nbPlayersAlive == 1 && !playingAlone && !gameIsFinished)
        {
            winText.SetActive(true);
            restartText.SetActive(true);
            SoundManager.PlaySound(SoundManager.Sound.Win);
            winText.GetComponent<Text>().text = lastPlayerAlive.name + " is the winner !";
            gameIsFinished = true;
        }
    }

    private void RestartTheGame()
    {
        if (gameIsFinished && Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);
        }
    }
}
