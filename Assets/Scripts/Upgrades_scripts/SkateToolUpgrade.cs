using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkateToolUpgrade : MonoBehaviour
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
            playerBehaviour.purchasedTool = true;
            playerBehaviour.reviveLaunchForceX = 13f;
            playerBehaviour.reviveLaunchForceX = 15f;

        }
        if (upgrade.upgradeLevel == 2)
        {
            playerBehaviour.reviveLaunchForceX = 26f;
            playerBehaviour.reviveLaunchForceX = 20f;

        }
        if (upgrade.upgradeLevel == 3)
        {
            playerBehaviour.reviveLaunchForceX = 42f;
            playerBehaviour.reviveLaunchForceX = 45f;

        }

    }
}
