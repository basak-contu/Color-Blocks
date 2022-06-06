using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject blockPrefab;
    public GameObject diamondPrefab;
    public GameObject player;
    public float distanceBetweenBlocks = 0.0f;

    private float zSpawn = 25;
    private int numberOfBlocks = 10;
    private List<GameObject> activeBlocks = new List<GameObject>();

    float jumpHeight;

    Color[] colors;

    PlayerControl playerControl;

    void Awake()
    {
        colors = new Color[3];
        colors[0] = new Color(1, 0.1537181f, 0.1367925f, 1); //Red
        colors[1] = new Color(0.1253912f, 0.9245283f, 0, 1); //Green
        colors[2] = new Color(0, 0.1690532f, 0.9254902f, 1); //Blue
    }


    // Start is called before the first frame update
    void Start()
    {
        playerControl = player.GetComponent<PlayerControl>();
        jumpHeight = playerControl.jumpHeight;
        StartingBlocks(numberOfBlocks);
    }

    private void StartingBlocks(int numberOfBlocks)
    {
        SpawnBlock(25, 9, colors[0]);
        for (int i = 1; i < numberOfBlocks; i++)
        {
            int height = Random.Range(25, 35);
            Color color = GenerateRandomColor();
            SpawnBlock(height, 9, color);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(zSpawn - player.transform.position.z < 50)
        {
            int height = Random.Range(25, 35);
            Color color = GenerateRandomColor();
            SpawnBlock(height, 9, color);
        }
        if (player.transform.position.z - 50 > activeBlocks[0].transform.position.z)
            DeleteBlock();
        
    }

    Color GenerateRandomColor()
    {
        var randomIndex = Random.Range(0, colors.Length);
        Color randomColor = colors[randomIndex];
        return randomColor;
    }

    void SpawnBlock(int height, int width, Color color)
    {
        /*GameObject block = Instantiate(blockPrefab, transform.forward * zSpawn, transform.rotation);
        block.GetComponent<Renderer>().material.color = color;
        width = random(20, 9, 9, 20, 40, 40);
        block.transform.localScale = new Vector3(9, height, width);
        block.transform.position += new Vector3(0, block.transform.localScale.y / 2, 0);
        activeBlocks.Add(block);
        zSpawn = zSpawn + block.transform.localScale.z + distanceBetweenBlocks; */

        if (activeBlocks.Count == 0)
        {
            GameObject block = Instantiate(blockPrefab, transform.forward * zSpawn, transform.rotation);
            block.GetComponent<Renderer>().material.color = color;
            width = random(20, 9, 9, 20, 40, 40);
            block.transform.localScale = new Vector3(9, height, width);
            block.transform.position += new Vector3(0, block.transform.localScale.y / 2, 0);
            activeBlocks.Add(block);
        }
        else
        {
            int previousBlockWidth = (int)activeBlocks[activeBlocks.Count - 1].transform.localScale.z;
            int currentWidth = random(20, 9, 9, 20, 40, 40);

            
            zSpawn = zSpawn + currentWidth / 2 + previousBlockWidth / 2 + distanceBetweenBlocks;
            if(currentWidth == 20)
            {


                SpawnDiamond(new Vector3(0, height + 2, zSpawn - 5));
                SpawnDiamond(new Vector3(0, height + 3, zSpawn));
                SpawnDiamond(new Vector3(0, height + 2, zSpawn + 5)); 
            }
            GameObject block = Instantiate(blockPrefab, transform.forward * zSpawn, transform.rotation);
            block.GetComponent<Renderer>().material.color = color;
            block.transform.localScale = new Vector3(9, height, currentWidth);
            block.transform.position += new Vector3(0, block.transform.localScale.y / 2, 0);
            activeBlocks.Add(block);
        }
       
    }

    void SpawnDiamond(Vector3 position)
    {
        GameObject diamond = Instantiate(diamondPrefab, position, transform.rotation);
    }

    private void DeleteBlock()
    {
        Destroy(activeBlocks[0]);
        activeBlocks.RemoveAt(0);
    }

    public List<GameObject> ActiveBlocks   // property
    {
        get { return activeBlocks; }   // get method
       
    }

    int random(int x, int y, int z, int px, int py, int pz)
    {
        // Generate a number from 1 to 100
        int r = Random.Range(1, 100);

        // r is smaller than px with probability px/100
        if (r <= px)
            return x;

        // r is greater than px and smaller than or equal to px+py
        // with probability py/100
        if (r <= (px + py))
            return y;

        // r is greater than px+py and smaller than or equal to 100
        // with probability pz/100
        else
            return z;
    }

    
}
