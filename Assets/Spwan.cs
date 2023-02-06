using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spwan : MonoBehaviour
{
    //•Ï”‚ÌéŒ¾
    public GameObject[] zombiePrefab;
    public int number;
    public float spawnRadius;
    public bool spawnOnstart;
    // Start is called before the first frame update
    void Start()
    {
        if(spawnOnstart)
        {
            SpawnAll();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnAll()
    {
        for (int i = 0; i < number; i++)
        {
            Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;
            int randomIndex = RandomIndex(zombiePrefab);

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPos, out hit, 5.0f,NavMesh.AllAreas))
            {
                Instantiate(zombiePrefab[randomIndex], randomPos,Quaternion.identity);
            }
            else
            {
                i--;
            }

        }
    }
    public int RandomIndex(GameObject[]games)
    {
        return Random.Range(0, games.Length);
    }    
}
