using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowerTimer : MonoBehaviour
{
    Upgrade upgrade;
    GameManager gameManager;
    PlayerBehaviour playerBehaviour;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
    }
    void Start()
    {
        upgrade = GetComponent<Upgrade>();
    }

    // Update is called once per frame
    void Update()
    {
        if (upgrade.upgradeLevel == 1)
        {
            gameManager.timerMultiplier = 0.8f;

        }
        if (upgrade.upgradeLevel == 2)
        {
            gameManager.timerMultiplier = 0.6f;

        }
        if (upgrade.upgradeLevel == 3)
        {
            gameManager.timerMultiplier = 0.4f;


        }

    }
}
