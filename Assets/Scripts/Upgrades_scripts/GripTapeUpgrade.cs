using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripTapeUpgrade : MonoBehaviour
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
        //level 0/default is 3/6
        if (upgrade.upgradeLevel == 1)
        {
            playerBehaviour.launchForceX = 4f;
            playerBehaviour.launchForceY = 7f;

        }
        if (upgrade.upgradeLevel == 2)
        {
            playerBehaviour.launchForceX = 6f;
            playerBehaviour.launchForceY = 9f;

        }
        if (upgrade.upgradeLevel == 3)
        {
            playerBehaviour.launchForceX = 9f;
            playerBehaviour.launchForceY = 12f;

        }

    }
}
