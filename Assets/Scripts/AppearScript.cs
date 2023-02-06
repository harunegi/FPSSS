using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearScript : MonoBehaviour
{
    [SerializeField] GameObject[] enemys;
    [SerializeField] float appearNextTime;
    [SerializeField] int maxNumOfEnemys;
    private int numberOfEnemys;
    private float elapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        numberOfEnemys = 0;
        elapsedTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(numberOfEnemys>=maxNumOfEnemys)
        {
            return;
        }
        elapsedTime += Time.deltaTime;

        if(elapsedTime>appearNextTime)
        {
            elapsedTime = 0f;

            AppearEnemy();
        }

        
    }
    void AppearEnemy()
    {
        var randomValue = Random.Range(0, enemys.Length);
        var randomRotaionY = Random.value * 360f;

        GameObject.Instantiate(enemys[randomValue], transform.position, Quaternion.Euler(0f, randomRotaionY, 0f));
        numberOfEnemys++;
        elapsedTime = 0f;

    }

}
