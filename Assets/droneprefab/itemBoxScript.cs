using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemBoxScript : MonoBehaviour
{

    public float nowposi;
    // Start is called before the first frame update
    void Start()
    {
        nowposi = this.transform.position.y;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, nowposi + Mathf.PingPong(Time.time, 0.3f), transform.position.z);
        
    }
}
