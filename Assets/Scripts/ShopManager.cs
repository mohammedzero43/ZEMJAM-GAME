using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    public static ShopManager instance {get; private set; }

    void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public GameObject enterShopButton;
    public GameObject shopUI;
    public bool isEntered = false;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowShop()
    {
        shopUI.SetActive(true);
        enterShopButton.SetActive(false);
    }

    void HideShop()
    {
        shopUI.SetActive(false);
        enterShopButton.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isEntered = true;
            enterShopButton.SetActive(true);
        }
    } 

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isEntered = false;
            enterShopButton.SetActive(false);
            HideShop();
        }
    } 
}
