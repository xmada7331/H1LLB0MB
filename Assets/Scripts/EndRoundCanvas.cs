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
        distance.text = "Distance: " + gameManager.player.transform.position.x.ToString("F1") + "m";
        coins.text = "Bonus Coins: " + gameManager.bonusCoins;
        maxSpeed.text = "Max Speed: " + playerBehaviour.maxSpeed.ToString("F1") + "m/s";
        maxHeight.text = "Max Height: " + playerBehaviour.maxHeight.ToString("F1") + "m";
        grindTime.text = "Grind time: " + gameManager.elapsedTime.ToString("F1") + "s";

    }
}
