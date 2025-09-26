using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsUpgrade : MonoBehaviour
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
            playerBehaviour.gainMultiplier = 1.2f;


        }
        if (upgrade.upgradeLevel == 2)
        {
            playerBehaviour.gainMultiplier = 1.5f;

        }
        if (upgrade.upgradeLevel == 3)
        {
            playerBehaviour.gainMultiplier = 1.8f;

        }

    }
}
