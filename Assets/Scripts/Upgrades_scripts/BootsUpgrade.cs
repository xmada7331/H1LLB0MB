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
            gameManager.climbDistance1 = -0.2f;
            gameManager.climbDistance2 = 0.2f;

        }
        if (upgrade.upgradeLevel == 2)
        {
            gameManager.climbDistance1 = -.4f;
            gameManager.climbDistance2 = .4f;
        }
        if (upgrade.upgradeLevel == 3)
        {
            gameManager.climbDistance1 = -.7f;
            gameManager.climbDistance2 = .7f;

        }

    }
}
