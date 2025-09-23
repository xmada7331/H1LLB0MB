using UnityEngine;

public class RampChange : MonoBehaviour
{
    Upgrade upgrade;
    GameManager gameManager;
    PlayerBehaviour playerBehaviour;

    public GameObject ramp0;
    public GameObject ramp1;
    public GameObject ramp2;
    public GameObject ramp3;
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
            ramp0.SetActive(true);
            ramp1.SetActive(false);
            ramp2.SetActive(false);
            ramp3.SetActive(false);
        }
        if (upgrade.upgradeLevel == 1)
        {
            ramp0.SetActive(false);
            ramp1.SetActive(true);
            ramp2.SetActive(false);
            ramp3.SetActive(false);
        }
        if (upgrade.upgradeLevel == 2)
        {
            ramp1.SetActive(false);
            ramp2.SetActive(true);
            ramp3.SetActive(false);
        }
        if (upgrade.upgradeLevel == 3)
        {
            ramp1.SetActive(false);
            ramp2.SetActive(false);
            ramp3.SetActive(true);
        }
    }
}
