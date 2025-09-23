using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class How2JumpUpgrade : MonoBehaviour
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
            playerBehaviour.maxJumpUsed = 2f;

        }
        if (upgrade.upgradeLevel == 2)
        {
            playerBehaviour.maxJumpUsed = 3f;

        }
        if (upgrade.upgradeLevel == 3)
        {
            playerBehaviour.maxJumpUsed = 4f;

        }

    }
}
