using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketUpgrade : MonoBehaviour
{
    Upgrade upgrade;
    GameManager gameManager;
    PlayerBehaviour playerBehaviour;

    public GameObject street0;
    public GameObject street1;
    public GameObject street2;
    public GameObject street3;
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
        if (upgrade.upgradeLevel == 0)
        {
            gameManager.streetNumber = 0;
            street0.SetActive(true);
            street1.SetActive(false);
            street2.SetActive(false);
            street3.SetActive(false);

        }
        if (upgrade.upgradeLevel == 1)
        {
            gameManager.streetNumber = 1;
            street0.SetActive(false);
            street1.SetActive(true);


        }
        if (upgrade.upgradeLevel == 2)
        {
            gameManager.streetNumber = 2;
            street1.SetActive(false);
            street2.SetActive(true);

        }
        if (upgrade.upgradeLevel == 3)
        {
            gameManager.streetNumber = 3;
            street2.SetActive(false);
            street3.SetActive(true);

        }

    }
}
