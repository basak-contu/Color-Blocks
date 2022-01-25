using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject blockPrefab;
    public Transform playerTransform;
    private float zSpawn = 0;
    public float distanceBetweenBlocks = 10f;

    Color[] colors;

    void Awake()
    {
        colors = new Color[3];
        colors[0] = new Color(1, 0.01999587f, 0, 1);
        colors[1] = new Color(0.1253912f, 0.9245283f, 0, 1);
        colors[2] = new Color(0, 0.1690532f, 0.9254902f, 1);
    }


    // Start is called before the first frame update
    void Start()
    {
        StartingBlocks(10);
    }

    private void StartingBlocks(int numberOfBlocks)
    {
        SpawnBlock(25);
        for (int i = 1; i < numberOfBlocks; i++)
        {
            int height = Random.Range(25, 40);
            SpawnBlock(height);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Color GenerateRandomColor()
    {
        var randomIndex = Random.Range(0, colors.Length);
        Color randomColor = colors[randomIndex];
        return randomColor;
    }

    void SpawnBlock(int height)
    {
        GameObject block = Instantiate(blockPrefab, transform.forward * zSpawn, transform.rotation);
        block.GetComponent<Renderer>().material.color = GenerateRandomColor();
        block.transform.localScale = new Vector3(9, height, 9);
        block.transform.localPosition += new Vector3(0, block.transform.localScale.y / 2, 0);
        zSpawn += block.transform.localScale.z + distanceBetweenBlocks;
    }

}
