using Cinemachine;
using System.Collections;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    AudioManager audioManager;
    GameManager gameManager;
    public CinemachineVirtualCamera cinemachineCamera;

    public GameObject player;
    public bool isOnRamp;
    public bool isGrinding;
    public bool leftTheRamp;
    public bool countedHeight;
    public Rigidbody2D rb;
    public LayerMask rampLayer;
    public LayerMask grindLayer;
    public PhysicsMaterial2D physicsMaterial;

    public float gainMultiplier = 3f;
    public float streetMult = .5f;
    public float loseMultiplier = 2f;

    public float maxJumpUsed = 1f;
    public float jumpStacks = 1f;
    public float launchForceX = 2f;
    public float launchForceY = 4f;
    public float jumpForceX = 2f;
    public float jumpForceY = 4f;

    public float reviveLaunchForceX = 3f;
    public float reviveLaunchForceY = 5f;

    public float coinValue = 1f;
    public float slowAmount = 0f;
    public float slowDuration = 0f;

    public bool loseMomentumIsPlaying;
    public bool loseAirMomentumIsPlaying;
    public bool gainMomentumIsPlaying;
    public bool usedJumpBoost = true;

    public float maxHeight;
    public float maxSpeed;
    public float currentSpeed;

    public bool playingCamera = false;
    public bool isSlowRunning = false;
    public bool purchasedTool = false;
    public bool jumpedToolUpgrade = false;

    public bool setStraight = false;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        setStraight = false;
        leftTheRamp = false;
        isSlowRunning = false;
        purchasedTool = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrinding && jumpStacks > 0 && Input.GetKeyDown(KeyCode.W) && jumpStacks <= maxJumpUsed)
        {
            rb.AddForce(new Vector2(jumpForceX, jumpForceY), ForceMode2D.Impulse);
            jumpStacks++;
            audioManager.PlayOllieJump();
        }
        if (!usedJumpBoost && Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(new Vector2(launchForceX, launchForceY), ForceMode2D.Impulse);
            usedJumpBoost = true;
            audioManager.PlayOllieJump();

        }

        if (player.transform.localPosition.x > 3 && rb.velocity.x <= 0 && !gameManager.isRoundOver)
        {
            if (purchasedTool && jumpedToolUpgrade)
            {
                ReviveLaunch();
            }
            else if (player.transform.localPosition.x > 1 && rb.velocity.x <= 0)
            {
                gameManager.EndRound(); 
            }

        }

        if (rb.velocity.x < 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            gameManager.mainSprite.GetComponent<SpriteRenderer>().sprite = gameManager.spriteStand;
        }
        else if (rb.velocity.x > 0.1)
        {
            if (player.transform.localPosition.y <= 0.2)
            {
                gameManager.mainSprite.GetComponent<SpriteRenderer>().sprite = gameManager.spriteStraight;
                setStraight = true;
            }
            else if (player.transform.localPosition.y >= 0.2 && !setStraight)
            {
                gameManager.mainSprite.GetComponent<SpriteRenderer>().sprite = gameManager.spriteRamp; 
            }
        }
        if (rb.velocity.y < 0 && !isOnRamp && leftTheRamp && !countedHeight)
        {
            CountHeight();
        }
        if (leftTheRamp && !playingCamera)
        {
            currentSpeed = rb.velocity.x;
            maxSpeed = Mathf.Max(maxSpeed, currentSpeed);
            //StartCoroutine(CameraChange());
        }
        if (!isGrinding)
        {
            loseMomentumIsPlaying = false;
        }

        //Debug.Log("max height:" + maxHeight);
        //Debug.Log("max speed:" + maxSpeed);
    }

    private void FixedUpdate()
    {
        //isOnRamp = Physics2D.CircleCast(transform.position, 0.5f, Vector2.down, 0.05f, rampLayer);
        isGrinding = Physics2D.CircleCast(transform.position, 0.5f, Vector2.down, 0.05f, grindLayer);

        if (isOnRamp && !gainMomentumIsPlaying)
        {
            StartCoroutine(GainMomentum());
        }
        else if (leftTheRamp && !loseAirMomentumIsPlaying)
        {
            StartCoroutine(LoseAirMomentum());
        }
        else if (isGrinding && !loseMomentumIsPlaying)
        {
            StartCoroutine(LoseMomentum());
            audioManager.PlayRailGrind();
            gameManager.mainSprite.GetComponent<SpriteRenderer>().sprite = gameManager.spriteGrind;
        }
        else if (!isGrinding || rb.velocity.x <= 0)
        {
            audioManager.StopRailGrindLoop();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag=="Ramp")
        {
            isOnRamp = true; 
        }
    }

    //private IEnumerator CameraChange()
    //{
    //    while (cinemachineCamera.m_Lens.OrthographicSize <= 10)
    //    {
    //        playingCamera = false;
    //        cinemachineCamera.m_Lens.OrthographicSize += .02f;
    //        yield return new WaitForSeconds(.3f);
    //    }
    //    playingCamera = true;
    //}
    //public IEnumerator CameraChangeBack()
    //{
    //    while (cinemachineCamera.m_Lens.OrthographicSize >=  7)
    //    {
    //        cinemachineCamera.m_Lens.OrthographicSize -= .02f;
    //        yield return new WaitForSeconds(.3f);
    //    }
    //}
    private IEnumerator GainMomentum()
    {
        Vector2 direction = rb.velocity.normalized;
        rb.velocity += direction * gainMultiplier * streetMult;
        yield return new WaitForSeconds(0.5f);
        gainMomentumIsPlaying = true;
    }
    private IEnumerator LoseMomentum()
    {
        while (rb.velocity.x > 0 && isGrinding && leftTheRamp)
        {
            rb.velocity += new Vector2(-.05f, 0f) * loseMultiplier;
            yield return new WaitForSeconds(.2f);
            //Debug.Log("losing momentun");
            loseMomentumIsPlaying = true;
        }
    }
    private IEnumerator LoseAirMomentum()
    {
        while (rb.velocity.x > 0)
        {
            rb.velocity += new Vector2(-.005f, 0f);
            yield return new WaitForSeconds(.2f);
            //Debug.Log("losing momentun");
            loseAirMomentumIsPlaying = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("JumpZone"))
        {
            usedJumpBoost = false;
            leftTheRamp = true;
        }
        if (collision.CompareTag("SlowZone") && !isSlowRunning)
        {
            StartCoroutine(SlowTime());
        }
        if (collision.CompareTag("Coin"))
        {
            audioManager.PlayCoinCollect();
            Destroy(collision.gameObject);
            gameManager.coins = gameManager.coins + coinValue;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("JumpZone"))
        {
            usedJumpBoost = true;
            isOnRamp = false;
        }
    }
    private IEnumerator SlowTime()
    {
        isSlowRunning = true;
        Time.timeScale = 1 - slowAmount;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        yield return new WaitForSeconds(slowDuration);
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;

    }

    private void CountHeight()
    {
        countedHeight = true;
        maxHeight = player.transform.position.y;
    }

    private void ReviveLaunch()
    {
        rb.AddForce(new Vector2(reviveLaunchForceX, reviveLaunchForceY), ForceMode2D.Impulse);
        jumpedToolUpgrade = false;
    }

}
