using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject blockPrefab;
    public Transform playerTransform;
    public float distanceBetweenBlocks = 0.0f;

    private float zSpawn = 0;
    private int numberOfBlocks = 10;
    private List<GameObject> activeBlocks = new List<GameObject>();

    Color[] colors;

    void Awake()
    {
        colors = new Color[3];
        colors[0] = new Color(1, 0.01999587f, 0, 1); //Red
        colors[1] = new Color(0.1253912f, 0.9245283f, 0, 1); //Green
        colors[2] = new Color(0, 0.1690532f, 0.9254902f, 1); //Blue
    }


    // Start is called before the first frame update
    void Start()
    {
        StartingBlocks(numberOfBlocks);
    }

    private void StartingBlocks(int numberOfBlocks)
    {
        SpawnBlock(25, 20,colors[0]);
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
        if(zSpawn - playerTransform.position.z < 50)
        {
            int height = Random.Range(25, 35);
            Color color = GenerateRandomColor();
            SpawnBlock(height, 9, color);
        }
        if (playerTransform.position.z - 50 > activeBlocks[0].transform.position.z)
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
        GameObject block = Instantiate(blockPrefab, transform.forward * zSpawn, transform.rotation);
        block.GetComponent<Renderer>().material.color = color;
        block.transform.localScale = new Vector3(9, height, width);
        block.transform.position += new Vector3(0, block.transform.localScale.y / 2, 0);
        activeBlocks.Add(block);
        zSpawn = zSpawn + block.transform.localScale.z + distanceBetweenBlocks;
       
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
}
