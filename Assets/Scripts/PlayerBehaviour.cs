using System.Collections;
using UnityEngine;
using Cinemachine;

public class PlayerBehaviour : MonoBehaviour
{
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

    public float gainMultiplier = 1.1f;
    public float loseMultiplier = 1.1f;

    public float jumpStacks = 1f;

    public bool loseMomentumIsPlaying;
    public bool loseAirMomentumIsPlaying;
    public bool gainMomentumIsPlaying;
    public bool usedJumpBoost = true;

    public float maxHeight;
    public float maxSpeed;
    public float currentSpeed;

    public bool playingCamera = false;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        leftTheRamp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrinding && jumpStacks > 0 && Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(new Vector2(2, 10   ), ForceMode2D.Impulse);
            jumpStacks--;
        }
        if (!usedJumpBoost && Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(new Vector2(5, 10), ForceMode2D.Impulse);
            usedJumpBoost = true;
        }

        if (player.transform.localPosition.x > 1 && rb.velocity.x <= 0 && !gameManager.isRoundOver)
        {
            gameManager.EndRound();

        }
        if (rb.velocity.x < 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
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
        isOnRamp = Physics2D.CircleCast(transform.position, 0.5f, Vector2.down, 0.05f, rampLayer);
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
        rb.velocity += new Vector2(.1f, -.1f) * gainMultiplier;
        yield return new WaitForSeconds(0.5f);
        //Debug.Log("gained momentum");
        gainMomentumIsPlaying = true;
    }
    private IEnumerator LoseMomentum()
    {
        while (rb.velocity.x > 0 && isGrinding)
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
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            gameManager.coins++;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("JumpZone"))
        {
            usedJumpBoost = true;
        }
    }

    private void CountHeight()
    {
        countedHeight = true;
        maxHeight = player.transform.position.y;
    }

}
