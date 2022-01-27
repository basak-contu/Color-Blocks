using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeColorToRed()
    {
        player.gameObject.GetComponent<Renderer>().material.color = new Color(1, 0.01999587f, 0, 1); //Red
    }
    public void ChangeColorToBlue()
    {
        player.gameObject.GetComponent<Renderer>().material.color = new Color(0, 0.1690532f, 0.9254902f, 1); //Blue
    }
    public void ChangeColorToGreen()
    {
        player.gameObject.GetComponent<Renderer>().material.color = new Color(0.1253912f, 0.9245283f, 0, 1); //Green
    }
}
