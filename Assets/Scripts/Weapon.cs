using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public static Weapon instans;

    public Transform shotDirection;

    public static Weapon instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
    }
  


//スピーカー音源変数
    public AudioSource weapon;
    public AudioClip relodingSE, fireSE, triggerSE;
    void Start()
    {
      
    }


    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(shotDirection.position, shotDirection.transform.forward * 10, Color.green);
    }
   
    public void CanShoot()
    {
        GameState.canShot = true;
    }

    public void FireSE()
    {
        weapon.clip = fireSE;
        weapon.Play();
    }
    public void RelodingSE()
    {
        weapon.clip = relodingSE;
        weapon.Play();
    }
    public void TriggerSE()
    {
        weapon.clip = triggerSE;
        weapon.Play();
    }


    public void Shooting()
    {
        RaycastHit hitInfo;

        if(Physics.Raycast(shotDirection.transform.position,shotDirection.transform.forward,out hitInfo,3000))
        {
            GameObject hitGameObject = hitInfo.collider.gameObject;

            if(hitInfo.collider.gameObject.GetComponent<ZombieController>() !=null)
            {
                var hitZombie = hitInfo.collider.gameObject.GetComponent<ZombieController>();
                if(hitZombie.health>0)
                {
                    hitZombie.health--;
                    var animator = hitZombie.gameObject.GetComponent<Animator>();
                    animator.SetBool("Attack", false);
                    animator.SetBool("Run", false);
                    animator.SetBool("Walk", false);
                    animator.SetBool("Damege", true);

                    if (hitZombie.health>0)
                    {
                        return;
                    }
                }
                if(Random.Range(0,10)<5)
                {
                    hitZombie.ZombieDeath();
                    GameObject rdPrefab = hitZombie.ragdoll;

                    GameObject NewRD = Instantiate(rdPrefab, hitGameObject.transform.position, hitGameObject.transform.rotation);
                    NewRD.transform.Find("mixamorig:Hips").GetComponent<Rigidbody>().AddForce(shotDirection.transform.forward * 1000);

                    Destroy(hitGameObject);
                }
                else
                {
                    hitZombie.ZombieDeath();
                }

                hitZombie.ZombieDeath();
            }
        }
    }
}
