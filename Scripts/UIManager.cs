using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI HealthDisplayText, CoinsDisplayText;

    public static UIManager instance;

    public TextMeshProUGUI tower1PriceText, tower2PriceText, tower3PriceText, tower4PriceText;

    [Header("ScreenObjects")]
    public GameObject _VictoryScreen;
    public GameObject _DefeatScreen;
    public GameObject _Background;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetDefaultValues();
        SetShopPrices();
    }

    public void UpdateHpUI()
    {
        if (HealthDisplayText)
        {
            HealthDisplayText.text = GameManager.instance.BaseHealth.ToString();
        }
        else
        {
            return;
        }
        
    }

    public void UpdateCoinDisplayUI()
    {
        if (CoinsDisplayText)
        {
            CoinsDisplayText.text = GameManager.instance.Coins.ToString();
        }
        else
        {
            return;
        }
        
    }


    void SetDefaultValues()
    {
        UpdateHpUI();
        UpdateCoinDisplayUI();
    }

    void SetShopPrices() //temp solution
    {
        tower1PriceText.text = BuildManager.instance.towerList[0].towerData.BuyPrice.ToString();
        tower2PriceText.text = BuildManager.instance.towerList[1].towerData.BuyPrice.ToString();
        tower3PriceText.text = BuildManager.instance.towerList[2].towerData.BuyPrice.ToString();
        tower4PriceText.text = BuildManager.instance.towerList[3].towerData.BuyPrice.ToString();
    }
}
