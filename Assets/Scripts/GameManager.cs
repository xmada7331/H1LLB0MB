using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TMP_Text coinCounter;
    public TMP_Text heightValue;
    public TMP_Text distanceValue;
    public TMP_Text timerText;
    public TMP_Text speedText;
    public TMP_Text scoreText;
    public TMP_Text rankText;
    public TMP_Text pressSpace;
    private float elapsedTime;
    private float score;
    public float coins = 0;
    public Slider timerSlider;
    public Slider heightSlider;
    public GameObject player;
    public GameObject startingPoint;
    public bool timerPhase = false;
    public bool pressLeft = false;
    public bool pressRight = false;

    public GameObject endRoundCanvas;
    public Animator firstScreenAnimator;
    public Animator secondScreenAnimator;

    public float timerMultiplier;

    public bool isRoundOver = false;
    public bool isRoundStarted = false;
    public bool firstStart = true;

    PlayerBehaviour playerBehaviour;

    private void Awake()
    {
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();

    }
    void Start()
    {
        firstStart = true;
        pressSpace.enabled = false;
        playerBehaviour.cinemachineCamera.Follow = null;
        playerBehaviour.rb.velocity = Vector2.zero;
        playerBehaviour.rb.isKinematic = true;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !firstStart)
        {
            StartRound();
        }
        if (Input.GetKeyDown(KeyCode.Space) && firstStart)
        {
            StartCoroutine(CameraIntro());
        }
        if (!playerBehaviour.leftTheRamp)
        {
            speedText.enabled = false;
        }
        else
        {
            speedText.enabled = true;
        }


        if (playerBehaviour.isGrinding && !isRoundOver)
        {
            elapsedTime += Time.deltaTime;
            timerText.text = "Grind time: " + elapsedTime.ToString("F1") + "s"; 
        }
        score = elapsedTime * (playerBehaviour.player.transform.position.x + coins + playerBehaviour.maxSpeed + playerBehaviour.maxHeight);
        coinCounter.text = "Coins: " + coins;
        heightValue.text = "" + player.transform.position.y.ToString("F1") + "m";
        speedText.text = "" + playerBehaviour.currentSpeed.ToString("F1") + "m/s";
        scoreText.text = "Score: " + score.ToString("F0");
        heightSlider.value = player.transform.position.y;
        if (player.transform.position.x > 0)
        {
            distanceValue.text = "Distance: " + player.transform.position.x.ToString("F1") + "m"; 
        }
        if (timerPhase)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && !pressLeft || Input.GetKeyDown(KeyCode.A) && !pressLeft)
            {
                LeftPress();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && !pressRight || Input.GetKeyDown(KeyCode.D) && !pressRight)
            {
                RightPress();
            }
        }
        if (score > 0 && score < 100)
        {
            rankText.text = "Rank: F";
        }
        if (score > 101 && score < 500)
        {
            rankText.text = "Rank: E";
        }
        if (score > 501 && score < 1500)
        {
            rankText.text = "Rank: D";
        }
        if (score > 1501 && score < 3000)
        {
            rankText.text = "Rank: C";
        }
        if (score > 3001 && score < 7500)
        {
            rankText.text = "Rank: B";
        }
        if (score > 7501 && score < 15000)
        {
            rankText.text = "Rank: A";
        }
        if (score > 15001 && score < 30000)
        {
            rankText.text = "Rank: S";
        }
        if (score > 30001 && score < 50000)
        {
            rankText.text = "Rank: SS";
        }
        if (score > 50001)
        {
            rankText.text = "Rank: SSS!";
        }

    }

    private IEnumerator TimerCountdown()
    {
        while (timerSlider.value > 0)
        {
            timerPhase = true;
            playerBehaviour.rb.isKinematic = true;
            timerSlider.value -= .02f * timerMultiplier;
            yield return new WaitForSeconds(.15f);
        }
        //Debug.Log("Time's up!");
        playerBehaviour.rb.isKinematic = false;
        timerPhase = false;
        //playerBehaviour.player.transform.SetParent(null);
    }

    private void LeftPress()
    {
        pressLeft = true;
        pressRight = false;
        player.transform.position += new Vector3(-.1f, .1f);
    }
    private void RightPress()
    {
        pressRight = true;
        pressLeft = false;
        player.transform.position += new Vector3(-.1f, .1f);
    }

    public void EndRound()
    {
        isRoundOver = true;
        playerBehaviour.leftTheRamp = false;
        //StartCoroutine(playerBehaviour.CameraChangeBack());
        firstScreenAnimator.SetTrigger("FirstScreenTr1");
        Debug.Log("Round Over");
    }

    public void ResetRound()
    {
        playerBehaviour.cinemachineCamera.Follow = playerBehaviour.player.transform;
        pressSpace.enabled = true;
        firstScreenAnimator.Update(0);
        speedText.enabled = false;
        isRoundOver = false;
        isRoundStarted = true;
        playerBehaviour.player.transform.SetParent(startingPoint.transform);
        playerBehaviour.player.transform.localPosition = Vector3.zero;
        playerBehaviour.rb.velocity = Vector2.zero;
        playerBehaviour.rb.isKinematic = true;
        timerSlider.value = 1;
        playerBehaviour.jumpStacks = 1f;
    }
    public void StartRound()
    {
        playerBehaviour.cinemachineCamera.Follow = playerBehaviour.player.transform;
        pressSpace.enabled = false;
        elapsedTime = 0f;
        playerBehaviour.loseMomentumIsPlaying = false;
        playerBehaviour.gainMomentumIsPlaying = false;
        playerBehaviour.loseAirMomentumIsPlaying = false;
        StartCoroutine(TimerCountdown());
        isRoundStarted = false;
    }

    private IEnumerator CameraIntro()
    {
        firstStart = false;
        Animator cameraAnimator = playerBehaviour.cinemachineCamera.GetComponent<Animator>();
        cameraAnimator.Play("CameraIntro");
        yield return null;

    }
}
