using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPlot : MonoBehaviour
{
    public GameObject buildMenuUI,PlotSelectedUI;

    public GameObject currentTower;
    public TowerPlot activePlot;



    private void Update()
    {
        if (buildMenuUI.activeSelf == false)
        {
            PlotSelectedUI.SetActive(false);
        }
    }
    public void BuildTower(GameObject tower)
    {
        int playerCoins = GameManager.instance.Coins;
        int towerPrice = tower.GetComponent<TowerController>().towerData.BuyPrice;

        if (currentTower == null && playerCoins >= towerPrice)
        {
            //tower.GetComponent<Tower>().BuyPrice
            var test = Instantiate(tower, transform.position, Quaternion.identity);
            test.transform.parent = gameObject.transform;
            currentTower = test.gameObject;

            GameManager.instance.RemoveCoins(towerPrice);
           // playerCoins -= towerPrice;
            buildMenuUI.SetActive(false);
            PlotSelectedUI.SetActive(false);
        }
        else
        {
            Debug.Log("Can't build tower, tower already exists");
            return;
        }
    }

    public void SellTower()
    {
        if (currentTower)
        {
            int sellPrice = currentTower.GetComponent<TowerController>().towerData.BuyPrice / 2;

            GameManager.instance.AddCoins(sellPrice);

            Destroy(currentTower);
            buildMenuUI.SetActive(false);
            PlotSelectedUI.SetActive(false);
        }
        else
        {
            return;
        }
        
    }

    public void EnableBuildMenu()
    {
        buildMenuUI.SetActive(true);
        PlotSelectedUI.SetActive(true);
        activePlot = this;
        BuildManager.instance.activeTowerPlot = activePlot;
    }
}
