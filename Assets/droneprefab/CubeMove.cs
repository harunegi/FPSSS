using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.3f);

        
    }
    public void Attack()
    {
        Debug.Log("çUåÇÇµÇƒÇ¢Ç‹Ç∑");
    }

}
