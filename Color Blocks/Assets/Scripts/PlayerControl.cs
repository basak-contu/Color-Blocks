using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 direction;
    public float forwardSpeed;

    public float jumpForce;
    public float gravity = -10;

    private bool isGrounded;
    private float jumpHeight = 10.0f;
    private Vector3 playerVelocity;

    public BlockSpawner blockSpawner;
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
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.Move(move * Time.deltaTime * forwardSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }

        playerVelocity.y += gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }



    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        GameObject currentBlock = hit.gameObject;
        int index = blockSpawner.ActiveBlocks.FindIndex(a => a.Equals(hit.gameObject));
        GameObject nextBlock = blockSpawner.ActiveBlocks[index + 1];
        float currentBlockHeight = currentBlock.transform.localScale.y;
        float nextBlockHeight = nextBlock.transform.localScale.y;
        if (nextBlockHeight > currentBlockHeight)
        {
            jumpHeight = nextBlockHeight - currentBlockHeight;
        }
        Debug.Log(index);
    }



}
