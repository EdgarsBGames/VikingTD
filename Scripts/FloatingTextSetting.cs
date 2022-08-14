using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextSetting : MonoBehaviour
{
    public float objectLifeTime = 1f;
    void Start()
    {
        transform.LookAt(GameManager.instance.MainCameraReference.transform.position);
        Destroy(gameObject, objectLifeTime);
    }

}
