using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Destoryzombie : MonoBehaviour
{

  
    //変数の作成(ゾンビかrdかの判定)
    public bool rd;

    public int scoreValue;
    private Score sm;

    //ドロップアイテム　
    public int dropToChance;
    public GameObject[] items;
    private bool Judgment;
   
    // Start is called before the first frame update
    
   
    void Start()

    {
        sm = GameObject.Find("ScoreManager").GetComponent<Score>();
        Judgment = true;
        if(rd)
        {
            DeadZombie();
        }
        
    }
  
    // Update is called once per frame
    void Update()
    {
        
    }
    public void DeadZombie()
    {
        if(Judgment)
        {
            Judgment = false;
        }
        else
        {
            return;
        }
        Invoke("DestroyGameObject" ,3f);
        sm.AddScore(scoreValue);

        if (Random.Range(0,100)<dropToChance)
        {
            Instantiate(items[Random.Range(0, items.Length)],transform.position,transform.rotation);
        }
    }
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
