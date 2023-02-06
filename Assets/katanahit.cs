using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class katanahit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {

        //UŒ‚‚µ‚½‘Šè‚ªEnemy‚Ìê‡
        if (other.CompareTag("Enemy"))
        {

            Destroy(other.gameObject);

        }
    }
}
