using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantsUpgrade : MonoBehaviour
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
            playerBehaviour.coinValue = 2f;

        }
        if (upgrade.upgradeLevel == 2)
        {
            playerBehaviour.coinValue = 3f;

        }
        if (upgrade.upgradeLevel == 3)
        {
            playerBehaviour.coinValue = 5f;

        }

    }
}
