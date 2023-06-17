using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    public float extraSpeed, extraJump;
    public int speedPrice, jumpPrice, rampagePrice;

    public Button buttonOne, buttonTwo, buttonThree;
    public TMP_Text coinsText,textOne, textTwo, textThree;

    public int coins = 5;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = coins.ToString();
    }

    public void ShowShop()
    {
        shopUI.SetActive(true);
        enterShopButton.SetActive(false);
        coinsText.text = coins.ToString();
        textOne.text = speedPrice.ToString();
        textTwo.text = jumpPrice.ToString();
        textThree.text = rampagePrice.ToString();
        UpdateButtons();
    }

    void HideShop()
    {
        shopUI.SetActive(false);
        enterShopButton.SetActive(false);
        FindObjectOfType<PlayerController2>().resetPosition();
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

    void UpdateButtons()
    {
        if(PlayerController2.ExtraSpeed > 0)
        {
            buttonOne.interactable = false;
        }
        else
        {
            buttonOne.interactable = true;
        }

        if(PlayerController2.ExtraJumpForce > 0)
        {
            buttonTwo.interactable = false;
        }
        else
        {
            buttonTwo.interactable = true;
        }

        if(PlayerController2.isRampage)
        {
            buttonThree.interactable = false;
        }
        else
        {
            buttonThree.interactable = true;
        }
    }

    public void BuySpeedPower()
    {
        if(coins > speedPrice)
        {
            coins -= speedPrice;
            PlayerController2.ExtraSpeed = extraSpeed;
        }
        UpdateButtons();
    }

    public void BuyJumpPower()
    {
        if(coins > jumpPrice)
        {
            coins -= jumpPrice;
            PlayerController2.ExtraJumpForce = extraJump;
        }
        UpdateButtons();
    }

    public void BuyRampage()
    {
        if(coins > rampagePrice)
        {
            coins -= rampagePrice;
            PlayerController2.isRampage = true;
        }
        UpdateButtons();
    }
}
