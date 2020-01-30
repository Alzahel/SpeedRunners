using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Start_Match : MonoBehaviour
{
    public Sprite trois;
    public Sprite deux;
    public Sprite un;
    public Sprite zero;
    private SpriteRenderer sr;
    private Image img;
    public GameObject player1;
    public GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        player1.GetComponent<Mvt>().enabled = false;
        player1.GetComponent<Animator>().enabled = false;
        player2.GetComponent<Mvt>().enabled = false;
        player2.GetComponent<Animator>().enabled = false;
        img = GetComponent<Image>();
        StartCoroutine(StartMatch());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartMatch()
    {
        yield return new WaitForSeconds(1f);
        img.sprite = deux;
        yield return new WaitForSeconds(1f);
        img.sprite = un;
        yield return new WaitForSeconds(1f);
        img.sprite = zero;
        player1.GetComponent<Mvt>().enabled = true;
        player1.GetComponent<Animator>().enabled = true;
        player2.GetComponent<Mvt>().enabled = true;
        player2.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(0.8f);
        img.enabled = false;
    }
}
