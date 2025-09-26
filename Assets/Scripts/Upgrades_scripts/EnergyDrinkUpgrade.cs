using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrinkUpgrade : MonoBehaviour
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

    void Update()
    {

        if (upgrade.upgradeLevel == 1)
        {
            playerBehaviour.slowAmount = 0.45f;
            playerBehaviour.slowDuration = 0.5f;

        }
        if (upgrade.upgradeLevel == 2)
        {
            playerBehaviour.slowAmount = 0.65f;
            playerBehaviour.slowDuration = 0.5f;

        }
        if (upgrade.upgradeLevel == 3)
        {
            playerBehaviour.slowAmount = 0.9f;
            playerBehaviour.slowDuration = 0.35f;

        }

    }
}
