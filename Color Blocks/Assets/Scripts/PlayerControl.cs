using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 direction;
    public float forwardSpeed = 10f;
    public float maxSpeed = 60f;

    public float gravity = -10;

    private bool isGrounded;
    public float jumpHeight = 10.0f;
    Vector3 playerVelocity;

    BlockSpawner blockSpawner;

    GameObject currentBlock;
    GameObject nextBlock;

    public float rotationSpeed = 180;
    public int score = 0;

    public int diamond_count = 0;

    GameObject collidedBlock = null;

    private GameObject lastBlock = null;

    public GameObject LastBlock { get => lastBlock; set => lastBlock = value; }

    public int neededDiamondCount = 6;

    public int combo = 0;

    [SerializeField] GameObject perfectText;
    
    float previousDiamondPositionZ = 0;

    public AudioSource[] sounds;
    public AudioSource gemPickSound;
    public AudioSource jumpSound;
    // Start is called before the first frame update
    void Start()
    {
        TinySauce.OnGameStarted();

        sounds = GetComponents<AudioSource>();
        jumpSound = sounds[0];
        gemPickSound = sounds[1];
        

        characterController = GetComponent<CharacterController>();
        blockSpawner = GameObject.FindObjectOfType<BlockSpawner>();
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        SpeedUp();
        if(combo == 2)
        {
            //StartCoroutine(ShowPerfectText());
        }
    }

    private void MovePlayer()
    {
       
        if (characterController.enabled == false)
            return;
        direction.z = forwardSpeed;
        playerVelocity.z = forwardSpeed;

        isGrounded = characterController.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
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
            Jump();

        }
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            if (Input.touchCount > 0)
            {
                if (touch.phase == TouchPhase.Began && isGrounded)
                {
                    Jump();
                }
            }
        }

        playerVelocity.y += gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        // Rotare Cube if it is not on block
        if (!isGrounded)
        {
            GetComponent<Animator>().Play("Roll");
            //transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime, 0);
        }
        if (isGrounded)
        {
            GetComponent<Animator>().Play("Idle");
        }
    }

    private void Jump()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Prevent jump when button pressed
            if (shouldDiscardSwipe(touch.position))
                return;
        }
      
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        playerVelocity.z = forwardSpeed / 2;
        jumpSound.Play();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
        currentBlock = hit.gameObject;
        int index = blockSpawner.ActiveBlocks.FindIndex(a => a.Equals(hit.gameObject));
        nextBlock = blockSpawner.ActiveBlocks[index + 1];
        float currentBlockHeight = currentBlock.transform.localScale.y;
        float nextBlockHeight = nextBlock.transform.localScale.y;
        
        if(hit.gameObject.transform.localScale.z == 20)
        {
            GameObject wideBlock = hit.gameObject;
            if(wideBlock.transform.position.z >= gameObject.transform.position.z)
            {
                jumpHeight = 1.0f;
                float blockHeight = wideBlock.transform.localScale.y;

            }
            else if(nextBlockHeight > currentBlockHeight)
            {
                jumpHeight = nextBlockHeight - currentBlockHeight;
            }
        }

        else if (nextBlockHeight > currentBlockHeight)
        {
            jumpHeight = nextBlockHeight - currentBlockHeight;

        }
        
        else
        {
            jumpHeight = 1.0f;
        }

        // Get color of block
        Color currentBlockColor = currentBlock.GetComponent<Renderer>().material.color;
        Color playerColor = gameObject.GetComponent<Renderer>().material.color;

        if(playerColor != currentBlockColor || hit.gameObject.tag == "Plane")
        {
            characterController.enabled = false;
            gameObject.GetComponent<Animator>().enabled = false;
            //FindObjectOfType<GameManager>().EndGame();
            FindObjectOfType<GameManager>().Pause();
            GetComponent<Animator>().Play("Idle");
        }

        else if(currentBlock != collidedBlock && hit.gameObject.tag != "Plane")
        {
            score++;
            LastBlock = currentBlock;
        }
        collidedBlock = currentBlock;
    }

    void SpeedUp()
    {
        if(forwardSpeed < maxSpeed)
        {
            forwardSpeed += 0.1f * Time.deltaTime;
            gravity = -1.0f * 2 * forwardSpeed;
        }
    }

    private bool shouldDiscardSwipe(Vector2 touchPos)
    {
        PointerEventData touch = new PointerEventData(EventSystem.current);
        touch.position = touchPos;
        List<RaycastResult> hits = new List<RaycastResult>();
        EventSystem.current.RaycastAll(touch, hits);
        return (hits.Count > 0); // discard swipe if an UI element is beneath
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Diamond")
        {
            //GetComponent<AudioSource>().Play();
            gemPickSound.Play();

            diamond_count++;
            Destroy(other.gameObject);

            float currentDiamondPositionZ = other.gameObject.transform.position.z;
            if(currentDiamondPositionZ - previousDiamondPositionZ <= 5)
            {
                combo++;
                if(combo == 2)
                {
                    StartCoroutine(ShowPerfectText());
                    combo = 0;
                }

            }
            else
            {
                combo = 0;
            }
            previousDiamondPositionZ = currentDiamondPositionZ;
            //Debug.Log(combo);
        }
    }

    public void ResumePlayer()
    {
        gameObject.transform.position = lastBlock.transform.position + new Vector3(0, lastBlock.transform.localScale.y / 2 + gameObject.transform.localScale.y / 2, -gameObject.transform.localScale.z / 4 );
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        gameObject.GetComponent<Renderer>().material.color = lastBlock.GetComponent<Renderer>().material.color;
        characterController.enabled = true;
        gameObject.GetComponent<Animator>().enabled = true;
    }

    IEnumerator ShowPerfectText()
    {
        perfectText.SetActive(true);

        //Wait for 4 seconds
        yield return new WaitForSeconds(0.5f);

        perfectText.SetActive(false);
    }
}
