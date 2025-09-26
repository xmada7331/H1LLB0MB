using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootsUpgrade : MonoBehaviour
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
            gameManager.climbDistance1 = -.3f;
            gameManager.climbDistance2 = .3f;

        }
        if (upgrade.upgradeLevel == 2)
        {
            gameManager.climbDistance1 = -.5f;
            gameManager.climbDistance2 = .5f;
        }
        if (upgrade.upgradeLevel == 3)
        {
            gameManager.climbDistance1 = -1f;
            gameManager.climbDistance2 = 1f;

        }

    }
}
