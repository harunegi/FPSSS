using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMove : MonoBehaviour
{
    private float time;
    private int vecX;
    private int vecY;
    private int vecZ;

    void Start()
    {
        time = 0;
    }

    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0.0f)
        {
            vecX = Random.Range(-10, 30);
            vecY = Random.Range(-10, 30);
            vecZ = Random.Range(-10, 30);
            transform.position = new Vector3(vecX, vecY, vecZ);
            time = 3.0f;
        }
    }
}