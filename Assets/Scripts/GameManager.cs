using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject mainSprite;
    public Sprite spriteStand;
    public Sprite spriteLeft;
    public Sprite spriteRight;
    public Sprite spriteRamp;
    public Sprite spriteStraight;
    public Sprite spriteGrind;
    public float coinsOnLevel = 250f;
    public TMP_Text coinCounter;
    public TMP_Text heightValue;
    //public TMP_Text distanceValue;
    //public TMP_Text timerText;
    public TMP_Text speedText;
    public TMP_Text scoreText;
    public TMP_Text rankText;
    public TMP_Text pressSpace;
    public float elapsedTime;
    private float score;
    public float coins = 0;
    public float bonusCoins = 0;
    public float autographValue = 1f;
    public Slider timerSlider;
    public Slider heightSlider;
    public GameObject coinsGO;
    public GameObject heightGO;
    public GameObject timerSliderObject;
    public GameObject heightSliderObject;
    public GameObject player;
    public GameObject startingPoint;
    public bool timerPhase = false;
    public bool pressLeft = false;
    public bool pressRight = false;

    public GameObject endRoundCanvas;
    public Animator firstScreenAnimator;
    public Animator secondScreenAnimator;
    public Animator timerAnimator;

    public float timerMultiplier;

    public bool isRoundOver = false;
    public bool isRoundStarted = false;
    public bool firstStart = true;

    public float climbDistance1 = 1f;  //default -.1f
    public float climbDistance2 = 1f;   //defauilt .1f;
    public int streetNumber = 0;

    public GameObject[] coinPrefab;
    public GameObject coinPrefab2;

    PlayerBehaviour playerBehaviour;

    private void Awake()
    {
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();

    }
    void Start()
    {
        mainSprite.GetComponent<SpriteRenderer>().sprite = spriteStand;
        RespawnCoins();
        firstStart = true;
        pressSpace.enabled = true;
        playerBehaviour.cinemachineCamera.Follow = null;
        playerBehaviour.rb.velocity = Vector2.zero;
        playerBehaviour.rb.isKinematic = true;
        coinsGO.SetActive(false);
        heightGO.SetActive(false);
    }


    void Update()
    {
        if (streetNumber == 0)
        {
            startingPoint.transform.position = new Vector3(0.37f, 1.04f, 0);
        }
        else if (streetNumber == 1 || streetNumber == 2)
        {
            startingPoint.transform.position = new Vector3(0.89f, 1.42f, 0);
        }
        else
        {
            startingPoint.transform.position = new Vector3(0.8f, 1.56f, 0);
        }
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
        if (firstStart)
        {
            coinsGO.SetActive(false);
            heightGO.SetActive(false);
            //coinCounter.enabled = false;
            heightValue.enabled = false;
            //heightSlider.enabled = false;
            //distanceValue.enabled = false;
            //timerText.enabled = false;
            speedText.enabled = false;
            scoreText.enabled = false;
            rankText.enabled = false;
            timerSliderObject.SetActive(false);
            //heightSliderObject.SetActive(false);
        }
        else
        {
            coinsGO.SetActive(true);
            heightGO.SetActive(true);
            //coinCounter.enabled = true;
            heightValue.enabled = true;
            //distanceValue.enabled = true;
            //timerText.enabled = true;
            speedText.enabled = true;
            scoreText.enabled = true;
            rankText.enabled = true;
            timerSliderObject.SetActive(true);
            //heightSliderObject.SetActive(true);
        }

        if (playerBehaviour.isGrinding && !isRoundOver)
        {
            elapsedTime += Time.deltaTime;
            //timerText.text = "Grind time: " + elapsedTime.ToString("F1") + "s";
        }
        score = autographValue * (playerBehaviour.player.transform.position.x + coins + playerBehaviour.maxSpeed + playerBehaviour.maxHeight + elapsedTime);
        coinCounter.text = "Coins: " + coins;
        heightValue.text = "" + player.transform.position.y.ToString("F1") + "m";
        speedText.text = "" + playerBehaviour.currentSpeed.ToString("F1") + "m/s";
        scoreText.text = "Score: " + score.ToString("F0");
        heightSlider.value = player.transform.position.y;
        if (player.transform.position.x > 0)
        {
            //distanceValue.text = "Distance: " + player.transform.position.x.ToString("F1") + "m";
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
        if (!isRoundStarted && !timerPhase)
        {
            if (pressLeft && !isRoundStarted || pressRight && !isRoundStarted)
            {
                pressSpace.text = "press W to jump off the ramp!";
            }
            else
            {
                pressSpace.text = "press SPACE to start!"; 
            }
        }
        else if (timerPhase)
        {
                pressSpace.text = "press A/D or arrows to climb!"; 
        }
        if (timerSlider.value <= 0.1f || timerSlider.value >= 0.9f)
        {
            timerAnimator.enabled = false;
        }
        else
        {
            timerAnimator.enabled = true;
        }





        if (score > 0 && score < 100)
        {
            rankText.color = Color.grey;
            rankText.fontSize = 24;
            rankText.text = "F";
        }
        if (score > 101 && score < 500)
        {
            rankText.color = Color.yellow;
            rankText.fontSize = 26;
            rankText.text = "E";
        }
        if (score > 501 && score < 1500)
        {
            rankText.color = Color.blue;
            rankText.fontSize = 28;
            rankText.text = "D";
        }
        if (score > 1501 && score < 3000)
        {
            rankText.color = new Color32(55,105,125, 255);
            rankText.fontSize = 30;
            rankText.text = "C";
        }
        if (score > 3001 && score < 7500)
        {
            rankText.color = new Color32(255, 145, 255, 255);
            rankText.fontSize = 48;
            rankText.text = "B";
        }
        if (score > 7501 && score < 10000)
        {
            rankText.color = new Color32(255, 5, 55, 255);
            rankText.fontSize = 58;
            rankText.text = "A";
        }
        if (score > 10001 && score < 15000)
        {
            rankText.color = new Color32(250, 247, 92, 255);
            rankText.fontSize = 78;
            rankText.text = "S";
        }
        if (score > 15001 && score < 20000)
        {
            rankText.color = new Color32(93, 255, 189, 255);
            rankText.fontSize = 50;
            rankText.text = "SS";
        }
        if (score > 20001)
        {
            rankText.color = new Color32(255, 0, 249, 255);
            rankText.fontSize = 52;
            rankText.text = "SSS!";
        }

    }

    private IEnumerator TimerCountdown()
    {
        while (timerSlider.value > 0)
        {
            timerPhase = true;
            playerBehaviour.rb.isKinematic = true;
            timerSlider.value -= .05f * timerMultiplier;
            yield return new WaitForSeconds(.15f);
        }
        //Debug.Log("Time's up!");
        playerBehaviour.rb.isKinematic = false;
        timerPhase = false;
        //playerBehaviour.player.transform.SetParent(null);
    }

    private void LeftPress()
    {
        if (streetNumber == 0)
        {
            mainSprite.GetComponent<SpriteRenderer>().sprite = spriteLeft;
            pressLeft = true;
            pressRight = false;
            player.transform.position += new Vector3(-(climbDistance1 * (-0.565f)), climbDistance2 * (0.562f));
        }
        else if(streetNumber == 1)
        {
            mainSprite.GetComponent<SpriteRenderer>().sprite = spriteLeft;
            pressLeft = true;
            pressRight = false;
            player.transform.position += new Vector3(-(climbDistance1 * (-0.6f)), climbDistance2 * (0.75f));

        }
        else if (streetNumber == 2)
        {
            mainSprite.GetComponent<SpriteRenderer>().sprite = spriteLeft;
            pressLeft = true;
            pressRight = false;
            player.transform.position += new Vector3(-(climbDistance1 * (-0.49f)), climbDistance2 * (0.75f));

        }
        else if (streetNumber == 3)
        {
            mainSprite.GetComponent<SpriteRenderer>().sprite = spriteLeft;
            pressLeft = true;
            pressRight = false;
            player.transform.position += new Vector3(-(climbDistance1 * (-0.42f)), climbDistance2 * (0.95f));

        }
    }
    private void RightPress()
    {
        if (streetNumber == 0)
        {
            mainSprite.GetComponent<SpriteRenderer>().sprite = spriteRight;
            pressLeft = false;
            pressRight = true;
            player.transform.position += new Vector3(-(climbDistance1 * (-0.565f)), climbDistance2 * (0.562f));
        }
        else if (streetNumber == 1)
        {
            mainSprite.GetComponent<SpriteRenderer>().sprite = spriteRight;
            pressLeft = false;
            pressRight = true;
            player.transform.position += new Vector3(-(climbDistance1 * (-0.6f)), climbDistance2 * (0.75f));

        }
        else if (streetNumber == 2)
        {
            mainSprite.GetComponent<SpriteRenderer>().sprite = spriteRight;
            pressLeft = false;
            pressRight = true;
            player.transform.position += new Vector3(-(climbDistance1 * (-0.49f)), climbDistance2 * (0.75f));

        }
        else if (streetNumber == 3)
        {
            mainSprite.GetComponent<SpriteRenderer>().sprite = spriteRight;
            pressLeft = false;
            pressRight = true;
            player.transform.position += new Vector3(-(climbDistance1 * (-0.42f)), climbDistance2 * (0.95f));

        }
    }

    public void EndRound()
    {
        playerBehaviour.setStraight = false;
        bonusCoins = Mathf.Floor(score / 10);
        coins += bonusCoins;
        isRoundOver = true;
        playerBehaviour.leftTheRamp = false;
        //StartCoroutine(playerBehaviour.CameraChangeBack());
        firstScreenAnimator.SetTrigger("FirstScreenTr1");
        Debug.Log("Round Over");
        pressLeft = false;
        pressRight = false;
    }

    public void ResetRound()
    {
        mainSprite.GetComponent<SpriteRenderer>().sprite = spriteStand;
        RespawnCoins();
        playerBehaviour.isSlowRunning = false;
        //distanceValue.text = "Distance: 0m";
        elapsedTime = 0f;
        //timerText.text = "Grind time: " + elapsedTime.ToString("F1") + "s";
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
        if (playerBehaviour.purchasedTool)
        {
            playerBehaviour.jumpedToolUpgrade = true;
        }
        playerBehaviour.cinemachineCamera.Follow = playerBehaviour.player.transform;
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

    private void RespawnCoins()
    {
        coinPrefab = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject coin in coinPrefab)
        {
            Destroy(coin); 
        }
        for (int i = 0; i < coinsOnLevel; i++)
        {
            GameObject coinObject = Instantiate(coinPrefab2, new Vector3(Random.Range(10f, 1000f), Random.Range(0.5f, 25f), 0), Quaternion.identity); 
        }
    }
}
