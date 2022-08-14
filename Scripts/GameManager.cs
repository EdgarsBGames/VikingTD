using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float BaseHealth = 20f;
    public int Coins;

    public WaveSpawner _WaveSpawner;

    public GameObject MainCameraReference;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {

    }


    public void TakeDamage(float amount)
    {
        BaseHealth -= amount;
        UIManager.instance.UpdateHpUI();
        if (BaseHealth <= 0)
        {
            Defeat();
        }
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
        UIManager.instance.UpdateCoinDisplayUI();
        //play event to update UI
    }

    public void RemoveCoins(int amount)
    {
        Coins -= amount;
        UIManager.instance.UpdateCoinDisplayUI();
    }

    public void Defeat()
    {
        UIManager.instance._DefeatScreen.SetActive(true);
        UIManager.instance._Background.SetActive(true);
        _WaveSpawner.GameComplete = true;
        // enable lose display
    }

    public void Victory()
    {
        UIManager.instance._VictoryScreen.gameObject.SetActive(true);
        UIManager.instance._Background.SetActive(true);
        _WaveSpawner.GameComplete = true;
        //enable victory screen
    }
}
