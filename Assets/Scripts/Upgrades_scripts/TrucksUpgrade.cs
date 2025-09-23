using UnityEngine;

public class TrucksUpgrade : MonoBehaviour
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
            playerBehaviour.loseMultiplier = 1.6f;

        }
        if (upgrade.upgradeLevel == 2)
        {
            playerBehaviour.loseMultiplier = 1f;

        }
        if (upgrade.upgradeLevel == 3)
        {
            playerBehaviour.loseMultiplier = .7f;

        }

    }
}
