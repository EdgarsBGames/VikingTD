using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyController : MonoBehaviour
{
    public Character characterData;
    public TextMeshPro hpText, coinRewardText;

    private WayPoints wayPoints;
    private int waypointIndex;

    [SerializeField]private float currentHealth;
    private Quaternion lookRotation;
    private Vector3 direction;

    private void Awake()
    {
        currentHealth = characterData.MaxHealth;
        if(hpText !=null)
        hpText.text = currentHealth.ToString();
    }
    void Start()
    {
        wayPoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<WayPoints>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleRotation();
        //DebugActions();
    }

    void HandleMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, wayPoints.waypoints[waypointIndex].position, characterData.MoveSpeed * Time.deltaTime);


        if (Vector3.Distance(transform.position, wayPoints.waypoints[waypointIndex].position) < 0.1f) //Checks distance between character and waypoint.
        {
            if (waypointIndex < wayPoints.waypoints.Length - 1)
            {
                waypointIndex++;
            }
            else
            {
                GameManager.instance.TakeDamage(characterData.Damage); // base takes damange when enemy reaches final checkpoint
                if(GameManager.instance.BaseHealth <= 0)
                {
                    GameManager.instance.Defeat();
                }
                gameObject.SetActive(false);
                //add Object pooling for better performance
            }
        }
    }

    void HandleRotation()
    {
        direction = wayPoints.waypoints[waypointIndex].position - transform.position; // find direction

        lookRotation = Quaternion.LookRotation(direction); //creates rotation

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * characterData.RotationSpeed);//rotates character

    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (hpText != null)
            hpText.text = currentHealth.ToString();

        if (currentHealth <= 0)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        Debug.Log(gameObject.name + " Died");
        gameObject.SetActive(false);

        int coinRewardAmount = Random.Range(characterData.MinReward, characterData.MaxReward);
       /* if (coinRewardText != null)
        {
            var tobePopup = Instantiate(coinRewardText, transform.position, Quaternion.identity);
            tobePopup.GetComponent<TextMeshPro>().text = coinRewardAmount + "G";
        }*/
        GameManager.instance.AddCoins(coinRewardAmount);
    }

    void DebugActions()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            TakeDamage(5);
        }
    }
}
