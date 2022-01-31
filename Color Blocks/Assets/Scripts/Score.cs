using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public GameObject player;
    public Text scoreText;

    PlayerControl playerControl;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = player.GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = playerControl.score.ToString();
    }
}
