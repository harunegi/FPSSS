using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class FPSController : MonoBehaviour
{
    float x, z;
    float speed = 0.1f;

    public GameObject cam;
    Quaternion cameraRot, charactorRot;
    float Xsensityvity = 3f, Ysensityvity = 3f;


    bool cursorLock = true;
    float minX = -90f, maxX = 90f;
    //変数の宣言
    public Animator animator;
  
    int ammunition=50, maxAmmunition=50, ammoClip=10, maxAmmoClip=10;

    //変数の宣言
    int playerHP = 100,maxPlayerHP = 100;
    public Slider hpBer;
    public Text ammoText;

    //FPSController
    //HPを減らす関数を作成する
    public GameObject meinCamera, subCamera;

    //スピーカー　音源変数
    public AudioSource playerFootStep;
    public AudioClip WalkFootStepSE, RunFootStepSE;
    public AudioSource voice,impact;
    public AudioClip HitVoiceSE,HitImpactSE;

    public AudioSource item;
    public AudioClip healItemSE, ammoSE;
    //回復
    public int ammoBox,medBox;

    public GameObject gameOverText;
    //volumeの宣言
    public Volume volume;
    float damageDisplay;

    [SerializeField]
    ParticleSystem muzzleFlashParticle = null;
   


    void Start()
    {
        cameraRot = cam.transform.localRotation;
        charactorRot = transform.localRotation;

        GameState.canShot = true;
        GameState.GameOver = false;

        gameOverText.SetActive(false);

        hpBer.value = playerHP;
        ammoText.text = ammoClip + "/" + ammunition;

    }

    // Update is called once per frame
    void Update()
    {
        DisplayChange();
        if(GameState.GameOver)
        {
            return;
        }

        float xRot = Input.GetAxis("Mouse X") * Ysensityvity;  
        float yRot = Input.GetAxis("Mouse Y") * Xsensityvity;

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        charactorRot *= Quaternion.Euler(0, xRot, 0);

        cameraRot = ClampRotation(cameraRot);

        cam.transform.localRotation = cameraRot;
        transform.localRotation = charactorRot;

        UpdateCursorLock();

        //射撃
        if (Input.GetMouseButton(0) && GameState.canShot)
        {
            if (ammoClip > 0)
            {
                animator.SetTrigger("Fire");
                muzzleFlashParticle.Play();
                GameState.canShot = false;
                ammoClip--;
                //ammoText.text = ammoClip + "/" + ammunition;
                AmmoUpdate();
            }
            else
            {
                //Debug.Log("弾切れ");

                Weapon.instance.TriggerSE();
            }

        }
            //リロード

        if (Input.GetKeyDown(KeyCode.R))
        {
            int amountNeed = maxAmmoClip - ammoClip;
            int ammoAvailable = amountNeed < ammunition ? amountNeed : ammunition;
            if(amountNeed !=0&&ammunition!=0)
            {
                animator.SetTrigger("Reload");
                ammunition -= ammoAvailable;
                ammoClip += ammoAvailable;
                //ammoText.text = ammoClip + "/" + ammunition;
            }
            
        }
        if(Mathf.Abs(x)> 0 || Mathf.Abs(z) > 0)
        {
            if(!animator.GetBool("Walk"))
            {
                animator.SetBool("Walk",true);

                PlayerWalkFootStep(WalkFootStepSE);
            }
        }
        else if(animator.GetBool("Walk"))
        {
            animator.SetBool("Walk", false);

            StopFootStep();
        }

        if (z > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            if (!animator.GetBool("Run"))
            {
                animator.SetBool("Run", true);
                speed = 0.2f;

                PlayerRunFootStep(RunFootStepSE);
            }
        }
        else if (animator.GetBool("Run"))
        {
            animator.SetBool("Run", false);
            speed = 0.1f;

            StopFootStep();
        }


        if(Input.GetMouseButton(1))
        {
            subCamera.SetActive(true);
            meinCamera.GetComponent<Camera>().enabled = false;
        }
        else if(subCamera.activeSelf)
        {
            subCamera.SetActive(false);
            meinCamera.GetComponent<Camera>().enabled = true;
        }
    }
    private void FixedUpdate()
    {
        if(GameState.GameOver)
        {
            return;
        }

        x = 0;
        z = 0;

        x = Input.GetAxisRaw("Horizontal") * speed;
        z = Input.GetAxisRaw("Vertical") * speed;

        //transform.position += new Vector3(x,0,z);
        transform.position += cam.transform.forward * z + cam.transform.right * x;
    }
   //マウスカーソル
    public void UpdateCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLock = false;
        }
        else if (Input.GetMouseButton(0))
        {
            cursorLock = true;
        }

        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!cursorLock)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public Quaternion ClampRotation(Quaternion q)
    {
        //q　=　x,y,z,w(x,y,zはベクトル(量と向き):wはスカラー(座標とは無関係の量:回転する))

        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;

        float angleX=Mathf.Atan(q.x)*Mathf.Rad2Deg*2f;

        angleX = Mathf.Clamp(angleX,minX,maxX);

        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;
    }
    public void PlayerWalkFootStep(AudioClip clip)
    {
        playerFootStep.loop = true;

        playerFootStep.pitch=1f;

        playerFootStep.clip = clip;

        playerFootStep.Play();
    }
    public void PlayerRunFootStep(AudioClip clip)
    {
        playerFootStep.loop = true;

        playerFootStep.pitch = 1.3f;

        playerFootStep.clip = clip;

        playerFootStep.Play();
    }

    public void StopFootStep()
    {
        playerFootStep.Stop();

        playerFootStep.loop = false;

        playerFootStep.pitch = 1f;
    }
    public void TakeHit(float damege)
    {
        playerHP =(int) Mathf.Clamp(playerHP - damege, 0, playerHP);
        HPUpdate();
        ImpactSE();
        if(Random.Range(0,10)<6)
        {
           VoiceSE(HitVoiceSE);
        }
        if(playerHP<=0&& !GameState.GameOver)
        {
            GameState.GameOver = true;

            gameOverText.SetActive(true);
            Invoke("Restart",3f); 
        }
            
    }
    public void VoiceSE(AudioClip clip)
    {
        voice.Stop();

        voice.clip = clip;
        voice.Play();
    }
    public void ImpactSE()
    {
        voice.clip = HitImpactSE;
        voice.Play();
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ItemSE(AudioClip clip)
    {
        item.clip = clip;
        item.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Ammo")
        {
            if(maxAmmunition>ammunition)
            {
                ammunition += ammoBox;
                if(maxAmmunition < ammunition)
                {
                    ammunition = maxAmmunition;
                }
                AmmoUpdate();
                ItemSE(ammoSE);
                Destroy(other.gameObject);
            }

        }
        else if(other.tag=="Med")
        {
            if (maxPlayerHP >playerHP)
            {
                playerHP += medBox;
                if(maxPlayerHP<playerHP)
                {
                    playerHP = maxPlayerHP;
                }
                HPUpdate();

                ItemSE(healItemSE);
              
                Destroy(other.gameObject);
            }
              
        }
    }
    public void DisplayChange()
    {
        if(playerHP>40)
        {
            damageDisplay = 0;
        }
        else if(playerHP<=40&&playerHP>20)
        {
            damageDisplay = 0.5f;
        }
        else if(playerHP<=20&&playerHP>10)
        {
            damageDisplay = 0.8f;
        }
        else if(playerHP<=10&&playerHP>10)
        {
            damageDisplay =1f;
        }
        volume.weight = damageDisplay;
    }
    public void HPUpdate()
    {
        hpBer.value = playerHP;
    }
    public void AmmoUpdate()
    {
        ammoText.text = ammoClip + "/" + ammunition;
    }
}