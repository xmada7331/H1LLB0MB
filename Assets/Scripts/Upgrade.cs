using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    GameManager gameManager;


    private Image thisImage;
    public TMP_Text upgradeName;
    public TMP_Text upgradeDescription;
    public TMP_Text upgradeCost;
    public Sprite upgradeIcon1;
    public Sprite upgradeIcon2;
    public Sprite upgradeIcon3;

    public string upgradeNameText1;
    public string upgradeNameText2;
    public string upgradeNameText3;
    public string upgradeDescriptionText1;
    public string upgradeDescriptionText2;
    public string upgradeDescriptionText3;


    public float upgradeLevel1Cost = 1f;
    public float upgradeLevel2Cost = 2f;
    public float upgradeLevel3Cost = 4f;

    public int upgradeLevel = 0;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        thisImage = GetComponent<Image>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HoverUpgrade()
    {
        float currentUpgradeCost = 0;
        switch (upgradeLevel)
        {
            case 0:
                currentUpgradeCost = upgradeLevel1Cost;
                break;
            case 1:
                currentUpgradeCost = upgradeLevel2Cost;
                break;
            case 2:
                currentUpgradeCost = upgradeLevel3Cost;
                break;
            default:
                break;
        }
        if (upgradeLevel == 0)
        {
            upgradeName.text = upgradeNameText1;
            thisImage.sprite = upgradeIcon1;
            upgradeDescription.text = upgradeDescriptionText1;
            upgradeCost.text = upgradeLevel1Cost.ToString();
        }
        else if (upgradeLevel == 1)
        {
            upgradeName.text = upgradeNameText2;
            thisImage.sprite = upgradeIcon2;
            upgradeDescription.text = upgradeDescriptionText2;
            upgradeCost.text = upgradeLevel2Cost.ToString();
        }
        else if (upgradeLevel == 2)
        {
            upgradeName.text = upgradeNameText3;
            thisImage.sprite = upgradeIcon3;
            upgradeDescription.text = upgradeDescriptionText3;
            upgradeCost.text = upgradeLevel3Cost.ToString();
        }
        else if (upgradeLevel == 3)
        {
            thisImage.enabled = false;
            upgradeName.text = "MAX";
            upgradeDescription.text = "MAX";
            upgradeCost.text = "---";
        }



        thisImage = GetComponent<Image>();
        thisImage.color = new Color32(255, 255, 255, 100); 
        //Debug.Log("hovered on upgrade");
    }

    public void PurchaseUpgrade()
    {
        float upgradeCost = 0;

        switch (upgradeLevel)
        {
            case 0:
                upgradeCost = upgradeLevel1Cost;
                break;
            case 1:
                upgradeCost = upgradeLevel2Cost;
                break;
            case 2:
                upgradeCost = upgradeLevel3Cost;
                break;
            default:
                Debug.Log("Invalid upgrade level");
                return;
        }

        if (gameManager.coins >= upgradeCost)
        {
            if (upgradeLevel < 3)
            {
                gameManager.coins -= (int)upgradeCost;
                upgradeLevel++;
                Debug.Log("Purchased upgrade level " + upgradeLevel);
                thisImage.sprite = upgradeIcon2;
            }
        }
        else
        {
            Debug.Log("Not enough coins to purchase upgrade");
        }
    }

    public void ExitHover()
    {
        thisImage = GetComponent<Image>();
        thisImage.color = new Color32(255, 255, 255, 255);
        //Debug.Log("exited hover on upgrade");
    }
}
