using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class katanahit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {

        //�U���������肪Enemy�̏ꍇ
        if (other.CompareTag("Enemy"))
        {

            Destroy(other.gameObject);

        }
    }
}
