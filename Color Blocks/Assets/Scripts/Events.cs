using UnityEngine;

public class Events : MonoBehaviour
{
    public GameObject player;
    public Animator animator;


    public void ChangeColorToRed()
    {
        player.gameObject.GetComponent<Renderer>().material.color = new Color(1, 0.1537181f, 0.1367925f, 1); //Red
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
