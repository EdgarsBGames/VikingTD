using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCharacter : MonoBehaviour
{
    TowerController towerController;
    public Transform characterToRotate;
    public Transform target;

    private void Start()
    {
       // towerController = GetComponent<TowerController>();
        towerController = GetComponentInParent<TowerController>();

        Debug.Log(characterToRotate.name);
        if(characterToRotate == null)
        {
            this.transform.position = characterToRotate.position;
            //characterToRotate = transform.position;
        }
    }
    void Update()
    {
        target = towerController.target;
        if(target != null)
        {
            Vector3 targetPos = new Vector3(target.transform.position.x, characterToRotate.transform.position.y, target.transform.position.z);

            transform.LookAt(targetPos);
        }
        else
        {
            return;
        }
        
    }


}
