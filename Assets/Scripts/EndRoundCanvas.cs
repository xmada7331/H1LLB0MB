using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndRoundCanvas : MonoBehaviour
{
    GameManager gameManager;
    PlayerBehaviour playerBehaviour;

    public TMP_Text distance;
    public TMP_Text coins;
    public TMP_Text maxSpeed;
    public TMP_Text maxHeight;
    public TMP_Text grindTime;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        distance.text = gameManager.distanceValue.text;
        coins.text = "Coins: " + gameManager.coins;
        maxSpeed.text = "Max Speed: " + playerBehaviour.maxSpeed.ToString("F1") + "m/s";
        maxHeight.text = "Max Height: " + playerBehaviour.maxHeight.ToString("F1") + "m";
        grindTime.text =gameManager.timerText.text;

    }
}
