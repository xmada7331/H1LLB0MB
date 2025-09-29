using UnityEngine;

public class AutographUpgrade : MonoBehaviour
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
            gameManager.autographValue = 2f;

        }
        if (upgrade.upgradeLevel == 2)
        {
            gameManager.autographValue = 4f;

        }
        if (upgrade.upgradeLevel == 3)
        {
            gameManager.autographValue = 7f;


        }

    }
}
