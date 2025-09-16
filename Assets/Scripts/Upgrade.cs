using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    private Image thisImage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HoverUpgrade()
    {
        thisImage = GetComponent<Image>();
        thisImage.color = new Color32(255, 255, 255, 100); 
        //Debug.Log("hovered on upgrade");
    }

    public void PurchaseUpgrade()
    {
        //Debug.Log("purchased upgrade");
    }

    public void ExitHover()
    {
        thisImage = GetComponent<Image>();
        thisImage.color = new Color32(255, 255, 255, 255);
        //Debug.Log("exited hover on upgrade");
    }
}
