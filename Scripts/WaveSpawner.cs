using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    private int nextWave = 0;
    public bool GameComplete;

    public float timeBetweenWaves = 5f;
    public float waveCountDown;

    public enum SpawnState { Spawning,Waiting,Counting};
    public SpawnState state = SpawnState.Counting;

    private float searchCountDown = 2f;

    [Header("UI Elements")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI waveDisplayText;

    private void Start()
    {
        waveCountDown = timeBetweenWaves;
    }

    private void Update()
    {
        if (!GameComplete)
        {
            if (state == SpawnState.Waiting)
            {
                if (!CheckIfEnemiesAreAlive())// check if enemies are alive //Change this later to ignore just wait for timer to run out and start new wave
                {
                    //update UI with next wave number
                    WaveComplete();
                }
                else
                {
                    return;
                }
            }

            if (waveCountDown <= 0)
            {
                if (state != SpawnState.Spawning)
                {
                    //spawn wave
                    StartCoroutine(SpawnWave(waves[nextWave]));
                }
            }
            else
            {
                waveCountDown -= Time.deltaTime;
                if (timerText)
                    timerText.text = string.Format("{0:0}", waveCountDown);
                //UI to display
            }
        }
    }

    void WaveComplete()
    {
        Debug.Log("wave done, starting next wave");
        state = SpawnState.Counting;
        waveCountDown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            Debug.Log("Level Complete"); // add logic to run UI stuff
            GameManager.instance.Victory();
            //GameComplete = true;
        }
        else
        {
            nextWave++;
        }
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        if(waveDisplayText)
        waveDisplayText.text = "Wave " + (nextWave+1) + "/" + waves.Length;

        state = SpawnState.Spawning;

        for(int i = 0; i < _wave.enemyCount; i++)
        {
            int randomNum = Random.Range(0, _wave.enemy.Length);
            SpawnEnemy(_wave.enemy[randomNum]); // spawns random enemies that ar available in wavedata
            yield return new WaitForSeconds(1f / _wave.spawnRate);
        }

        state = SpawnState.Waiting;

        yield break;
    }

    bool CheckIfEnemiesAreAlive()
    {
        searchCountDown -= Time.deltaTime;
        if(searchCountDown <= 0f)
        {
            searchCountDown = 2f;
            if(GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Instantiate(_enemy, transform.position, Quaternion.identity);
        Debug.Log("Spawning enemy");
    }
}
