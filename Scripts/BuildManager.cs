using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildManager : MonoBehaviour
{
    public TowerController[] towerList;
    public GameObject shopUIObject;

    public static BuildManager instance;

    public TowerPlot activeTowerPlot;

    private void Awake()
    {
        instance = this;
    }

    public void BuildNewTower(GameObject tower)
    {
        Debug.Log("Tower");
        activeTowerPlot.BuildTower(tower);
    }

    public void SellTower()
    {
        activeTowerPlot.SellTower();
    }
}
