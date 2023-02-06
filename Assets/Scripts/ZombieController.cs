using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class ZombieController : MonoBehaviour
{
    //変数の宣言(アニメーター、AI)
    Animator animator;
    NavMeshAgent agent;

    public float walkingSpeed;

    //列挙型の作成
    enum STATE {IDLE,WANDER,ATTACK,CHASE,DEAD,HIT};
    STATE state = STATE.IDLE;
    //変数の宣言(プレイヤーオブジェクト格納：走るスピード)
    GameObject target;
    public float runSpeed;
    //スタート時変数にコンポーネントを格納
    public int attackDamege;
    // Start is called before the first frame update
   
    //ラグドールを格納する変数を作成
    public GameObject ragdoll;

    public AudioSource zomVoice;
    public AudioClip howl, attack;

    public int scoreValue;
  

    public int health = 3;
    void Start()
    {
       
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if(target==null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        Howl();

    }
    public void TurnOffTrigger()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
        animator.SetBool("Death", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Damege", false);
     
    }

    float DistanceToPlyaer()
    {
        if(GameState.GameOver)
        {
            return Mathf.Infinity;
        }
        return Vector3.Distance(target.transform.position, transform.position);
    }
    bool CanSeePlayer()
    {
        if(DistanceToPlyaer()<15)
        {
            return true;
        }
        return false;
    }
    bool ForGetPlayer()
    {
        if(DistanceToPlyaer()>20)
        {
            return true;
        }
        return false;
    }

    public void DamagePlayer()
    {
        if(target!=null)
        {
            AttackSE();
            target.GetComponent<FPSController>().TakeHit(attackDamege);
        }
    }


    public void ZombieDeath()
    {
        TurnOffTrigger();
        animator.SetBool("Death", true);
        state = STATE.DEAD;
    }
    public void ZombieHit()
    {
        TurnOffTrigger();
        animator.SetBool("Damege", true);
        state = STATE.HIT;
    }

    public void Howl()
    {
        if(!zomVoice.isPlaying)
        {
            zomVoice.clip = howl;
            zomVoice.Play();
        }
    }
    public void AttackSE()
    {
        zomVoice.clip = attack;
        zomVoice.Play();
    }
  
    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case STATE.IDLE:
                TurnOffTrigger();


                if(CanSeePlayer())
                {
                    state = STATE.CHASE;
                }
         
              else  if (Random.Range(0,5000) < 5)
                {
                    state = STATE.WANDER;
                }
                if(Random.Range(0,5000)<5)
                {
                    Howl();
                }
                break;

            case STATE.WANDER:
                if(!agent.hasPath)
                {
                    float newX = transform.position.x + Random.Range(-5, 5);
                    float newZ= transform.position.z + Random.Range(-5, 5);

                    Vector3 NextPos = new Vector3(newX, transform.position.y, newZ);

                    agent.SetDestination(NextPos);
                    agent.stoppingDistance = 0;

                    TurnOffTrigger();
                    agent.speed = walkingSpeed;
                    animator.SetBool("Walk", true);
                }
                if(Random.Range(0,5000)<5)
                {
                    state = STATE.IDLE;
                    agent.ResetPath();
                }
                if(CanSeePlayer())
                {
                    state = STATE.CHASE;
                }
                if (Random.Range(0, 5000) < 5)
                {
                    Howl();
                }

                break;

            case STATE.CHASE:

                if(GameState.GameOver)
                {
                    TurnOffTrigger();
                    agent.ResetPath();
                    state = STATE.WANDER;

                    return;
                }

                agent.SetDestination(target.transform.position);
                agent.stoppingDistance = 3;

                TurnOffTrigger();
                agent.speed = runSpeed;
                animator.SetBool("Run", true);

                if (agent.remainingDistance<= agent.stoppingDistance + 2)
                {
                    state = STATE.ATTACK;
                }

                if (ForGetPlayer())
                {
                    agent.ResetPath();
                    state = STATE.WANDER;
                }
                break;

            case STATE.ATTACK:
                if(GameState.GameOver)
                {
                    TurnOffTrigger();
                    agent.ResetPath();
                    state = STATE.WANDER;
                }
                TurnOffTrigger();
                animator.SetBool("Attack", true);

              transform.LookAt(new Vector3(target.transform.position.x,transform.position.y,target.transform.position.z));

                if(DistanceToPlyaer()>agent.stoppingDistance+2)
                {
                    state = STATE.CHASE;
                    Howl();
                }



                break;
            case STATE.DEAD:
                
                zomVoice.Stop();
               Destroy(agent);
               


                gameObject.GetComponent<Destoryzombie>().DeadZombie();


                

                break;
        }
    }
}
