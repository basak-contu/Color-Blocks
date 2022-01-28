using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 direction;
    private float forwardSpeed = 10f;

    public float gravity = -10;

    private bool isGrounded;
    float jumpHeight = 10.0f;
    Vector3 playerVelocity;

    BlockSpawner blockSpawner;

    GameObject currentBlock;
    GameObject nextBlock;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        blockSpawner = GameObject.FindObjectOfType<BlockSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.z = forwardSpeed;
        playerVelocity.z = forwardSpeed;
       
        isGrounded = characterController.isGrounded;
        if(isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical") * forwardSpeed);
        characterController.Move(move * Time.deltaTime);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravity);
            float distance = nextBlock.transform.position.z - gameObject.transform.position.z;
            ///playerVelocity.z = (playerVelocity.y * distance) / jumpHeight;

        }

        playerVelocity.y += gravity * Time.deltaTime;
        //playerVelocity.y += gravity ;
        characterController.Move(playerVelocity * Time.deltaTime);
        Debug.Log("player velocity "+playerVelocity);
    }



    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        currentBlock = hit.gameObject;
        int index = blockSpawner.ActiveBlocks.FindIndex(a => a.Equals(hit.gameObject));
        nextBlock = blockSpawner.ActiveBlocks[index + 1];
        float currentBlockHeight = currentBlock.transform.localScale.y;
        float nextBlockHeight = nextBlock.transform.localScale.y;
        
        if (nextBlockHeight > currentBlockHeight)
        {
            jumpHeight = nextBlockHeight - currentBlockHeight + 1.0f;

        }
        
        else
        {
            jumpHeight = 1.0f;
        }
       
    }



}
