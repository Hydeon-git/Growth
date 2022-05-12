using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerController : MonoBehaviour
{
    //-----------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------
    // GENERAL
    public GameManager GameManager;

    public bool characterCanMove = true;
    private float timeToRestartScene = 3.5f;

    public Image heart1;
    public Image heart2;
    public Image heart3;

    private Animator GaveAnim;
    private bool canAnimJump = true;

    public GameObject Camera;
    private bool isGroundedDetectedByTransform;

    // MOVEMENT
    private Rigidbody2D playerRb;

    public float speed;
    public float jumpForce;
    private float moveInput;

    public bool isGrounded
    {
        get
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, 0.55f, GroundLayerMask);
            return ray.collider != null;
        }
    }

    public Transform feetPos;
    public float checkRadius;
    public LayerMask GroundLayerMask;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    public bool facingRight;
    public bool facingLeft;

    // DOUBLE JUMP
    public bool canDoubleJump;

    // DASH
    public bool canDash;

    public float dashCooldown = 3f;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;
    private float currentDashSpeed;

    // JUMP BETWEEN WALLS
    public Transform wallCheckPointR;

    public bool isWall;
    public LayerMask wallLayerMask;
    public float wallJumpForce = 4f;

    // KEYS
    public int currentKeys;

    // TOXIC GAS
    private float toxicGasCounter = 0;

    public bool insideToxicGas;

    // SPIKES
    public bool iTouchSpikes;

    public bool iTouchLetalSpikes;

    //-----------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------
    private void Start()
    {
        Application.targetFrameRate = 60;
        // Getting Rigidbody from my gameObject
        playerRb = GetComponent<Rigidbody2D>();
        GaveAnim = GetComponent<Animator>();
        canDash = false;
        dashTime = startDashTime;
        //GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
    }

    private void FixedUpdate()
    {
        if (iTouchSpikes && facingLeft)
        {
            GameManager.SendMessage("DownGaveLives");
            playerRb.AddForce(transform.up * 200);
            playerRb.AddForce(-transform.right * 200);
            iTouchSpikes = false;
        }
        if (iTouchSpikes && facingRight)
        {
            GameManager.SendMessage("DownGaveLives");
            playerRb.AddForce(transform.up * 200);
            playerRb.AddForce(transform.right * 200);
            iTouchSpikes = false;
        }
        if (iTouchLetalSpikes)
        {
            StartCoroutine(RestartScene(timeToRestartScene));
            playerRb.AddForce(transform.up * 200);
            iTouchLetalSpikes = false;
        }
    }

    private void Update()
    {
        // Getting the Horizontal Axis and saving it to a variable
        moveInput = Input.GetAxisRaw("Horizontal");
        // PlayerMovement        
        if (Input.GetKey(KeyCode.A) && characterCanMove)
        {
            GaveAnim.SetBool("isRunningL", true);
            if (isWall)
            {
                HandleWallSliding();
            }
            else
            {
                transform.position -= Vector3.right * speed * Time.deltaTime;
            }
        }
        else
        {
            GaveAnim.SetBool("isRunningL", false);
        }
        if (Input.GetKey(KeyCode.D) && characterCanMove)
        {
            GaveAnim.SetBool("isRunningR", true);
            if (isWall)
            {
                HandleWallSliding();
            }
            else
            {
                transform.position -= Vector3.left * speed * Time.deltaTime;
            }
        }
        else
        {
            GaveAnim.SetBool("isRunningR", false);
        }
        if (GameManager.GaveLives < 1)
        {
            StartCoroutine(RestartScene(timeToRestartScene));
        }
        // MOVEMENT (Just the facing direction of my character)
        // isGroundedDetectedByTransform = Physics2D.OverlapCircle(feetPos.position, checkRadius, GroundLayerMask);        
        if (moveInput > 0 && characterCanMove)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            facingRight = true;
            facingLeft = false;
        }
        else if (moveInput < 0 && characterCanMove)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            facingRight = false;
            facingLeft = true;
        }
        // If you are on the ground and you clicked Space you Jump
        if (Input.GetKeyDown(KeyCode.Space) && characterCanMove)
        {
            if (isGrounded)
            {
                PlayerJump();
                canDoubleJump = false; //Posarho en true per fer doble salt
            }
            else
            {
                if (canDoubleJump)
                {
                    PlayerJump();                    
                    canDoubleJump = false;
                    playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
                }
            }
        }
        // If you clicked Space and is Jumping is True,
        // and if zero is less than my jumpTimeCounter,
        // then the character jumps while the jumpTimeCounter is not exceeding zero
        // Else isJumping equal to false;
        if (Input.GetKey(KeyCode.Space) && isJumping == true && characterCanMove)
        {
            if (jumpTimeCounter > 0)
            {
                GaveAnim.SetBool("Idle", false);
                playerRb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
                if (canAnimJump)
                {
                    GaveAnim.SetTrigger("Jump");
                    canAnimJump = false;
                }
            }
            else
            {
                isJumping = false;
            }
        }
        else
        {
            canAnimJump = true;
            
        }
        if (isGrounded)
        {
            GaveAnim.SetBool("Idle", true);
        }
        // When you click Space isJumping is equal to false
        if (Input.GetKeyUp(KeyCode.Space) && characterCanMove)
        {
            isJumping = false;
        }
        //-----------------------------------------------------------------------------------
        // DASH
        dashCooldown += Time.deltaTime;
        if (dashCooldown >= 2f)
        {
            dashCooldown = 2f;
        }
        if (direction == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && dashCooldown >= 2 && characterCanMove && canDash)
            {
                direction = 1;
                currentDashSpeed = dashSpeed;
                dashCooldown = 0f;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && dashCooldown >= 2 && characterCanMove && canDash)
            {
                direction = 2;
                currentDashSpeed = dashSpeed;
                dashCooldown = 0f;
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;

                StartCoroutine(StopDash());
            }
            else
            {
                dashTime -= Time.deltaTime;

                currentDashSpeed += 20f * Time.deltaTime;
                currentDashSpeed = Mathf.Min(currentDashSpeed, dashSpeed);

                if (direction == 1)
                {
                    playerRb.AddForce(Vector2.left * currentDashSpeed);
                }
                if (direction == 2)
                {
                    playerRb.AddForce(Vector2.right * currentDashSpeed);
                }
            }
        }
        //-----------------------------------------------------------------------------------
        // JUMP BETWEEN WALLS
        //Detecting the wall using a transform from a child empty
        isWall = Physics2D.OverlapCircle(wallCheckPointR.position, 0.1f, wallLayerMask);
        if (isWall && !isGrounded && characterCanMove)
        {
            // Starting the wallSliding Coroutine
            HandleWallSliding();
        }
        else
        {
            // I'm not sliding in the wall here
            isWall = false;
        }
        //-----------------------------------------------------------------------------------
        // TOXIC GAS
        if (insideToxicGas)
        {
            toxicGasCounter = 0;
            toxicGasCounter += Time.deltaTime;
            Debug.Log(toxicGasCounter);
            if (toxicGasCounter > 2)
            {
                StartCoroutine(ToxicDamage());
            }
            if (GameManager.GaveLives < 1)
            {
                StartCoroutine(RestartScene(timeToRestartScene));
            }
        }
    }

    // StopDashCoroutine
    private IEnumerator StopDash()
    {
        float stopTime = 0;

        while (stopTime <= 0.1f)
        {
            currentDashSpeed = Mathf.Lerp(currentDashSpeed, 0f, stopTime / 0.1f);
            stopTime += Time.deltaTime;
            yield return null;
        }
    }

    // Makes Player Jumps
    private void PlayerJump()
    {
        isJumping = true;
        jumpTimeCounter = jumpTime;
        playerRb.velocity = Vector2.up * jumpForce;
    }

    // Makes Player Slide on a wall;
    private void HandleWallSliding()
    {
        playerRb.velocity = new Vector2(0, -0.2f);
        GaveAnim.SetTrigger("Slide");
        canDoubleJump = true;
    }

    // On CollisionEnter
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("MobilePlatform"))
        {
            this.transform.parent = collision.transform;
        }
        if (collision.gameObject.tag.Equals("GeyserPlatform"))
        {
            this.transform.parent = collision.transform;
        }
    }

    // On CollisionExit
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("MobilePlatform"))
        {
            this.transform.parent = null;
        }
        if (collision.gameObject.tag.Equals("GeyserPlatform"))
        {
            this.transform.parent = null;
        }
        if (collision.gameObject.name.Equals("ToxicGas"))
        {
            insideToxicGas = false;
            toxicGasCounter = 0;
        }
    }

    // On TriggerEnter
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Geyser"))
        {
            if (GameManager.GaveLives < 1)
            {
                StartCoroutine(RestartScene(timeToRestartScene));
            }
            else
            {
                GameManager.SendMessage("DownGaveLives");
            }
        }
        if (collision.gameObject.tag.Equals("Magma"))
        {
            StartCoroutine(RestartScene(timeToRestartScene));
            heart3.gameObject.SetActive(false);
            heart2.gameObject.SetActive(false);
            heart1.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag.Equals("KillerCollider"))
        {
            StartCoroutine(RestartScene(timeToRestartScene));
        }
        if (collision.gameObject.tag.Equals("ToxicGas"))
        {
            Debug.Log("Inside Toxic Gas");
            insideToxicGas = true;
        }
        if (collision.gameObject.tag.Equals("Spikes"))
        {
            iTouchSpikes = true;
        }
        if (collision.gameObject.tag.Equals("LetalSpikes"))
        {
            StartCoroutine(RestartScene(timeToRestartScene));
            iTouchLetalSpikes = true;
        }
        if (collision.gameObject.tag.Equals("Life"))
        {
            if (GameManager.GetComponent<GameManager>().GaveLives == 2)
            {
                heart3.gameObject.SetActive(true);
            }
            if (GameManager.GetComponent<GameManager>().GaveLives == 1)
            {
                heart2.gameObject.SetActive(true);
            }
            GameManager.SendMessage("UpGaveLives");
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag.Equals("NormalMusic"))
        {
            GameManager.diabloIsOn = false;
            GameManager.StopDiabloMusic();
        }
        if (collision.gameObject.tag.Equals("DiabloMusic"))
        {
            GameManager.diabloIsOn = true;
            GameManager.StartDiabloMusic();
        }
    }

    private bool isGameOver = true;

    private IEnumerator RestartScene(float time)
    {
        if (isGameOver)
        {
            isGameOver = false;
        }
        else
        {
            yield break;
        }
        characterCanMove = false;
        GaveAnim.SetTrigger("Dead");
        heart1.gameObject.SetActive(false);
        heart2.gameObject.SetActive(false);
        heart3.gameObject.SetActive(false);
        yield return new WaitForSeconds(time);
        GameManager.SendMessage("RestartScene");
    }

    private IEnumerator ToxicDamage()
    {
        GameManager.SendMessage("DownGaveLives");
        toxicGasCounter = 0f;
        yield return null;
    }

    public void KilledByDrop()
    {
        StartCoroutine(RestartScene(timeToRestartScene));
    }

    // DRAW RAYCAST TO CHECK GROUND
    /*
    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - 0.556f), Color.green, 1000000000f);
    }
    */
    // LOAD PLAYER STATS FROM SaveManager
    /*
    public void SaveStats()
    {
        SaveManager.SavePlayerStats(this);
    }
    public void LoadStats()
    {
        SaveData saveData = SaveManager.LoadPlayerStats();

        currentKeys = saveData.current
    }
    */
}