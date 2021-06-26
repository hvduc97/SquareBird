using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float speed;
    private int hp;
    private Vector2 curPos;
    private int dir;

    private GameObject player;
    private PlayerController playerCtrl;
    
    private Vector2 posPlayer, pos0, pos1, pos2, pos3;
    private float timeStay, timeFire, speedFire;
    private bool isBack, isDie;

    // Start is called before the first frame update
    void Start()
    {
        isDie = false;
        hp = 100;
        speed = 5.0f;
        dir = Random.Range(0, 3);

        player = GameObject.FindGameObjectWithTag("Player");
        playerCtrl = player.GetComponent<PlayerController>();

        pos0 = new Vector2(-1.5f, -3.25f);
        pos1 = new Vector2(2.0f, 4.0f);
        pos2 = new Vector2(2.0f, -3.25f);
        pos3 = new Vector2(2.0f, 0.0f);

        timeStay = 0.0f;
        timeFire = 0.0f;
        speedFire = 0.7f;
        isBack = false;

        MenuController.instance.SetScoreTxt(hp);

    }

    // Update is called once per frame
    void Update()
    {
        posPlayer = player.transform.position;
        curPos = transform.position;

        if(!isBack)
        {
            Atack(Time.deltaTime,  dir);
        }
        else
        {
            MoveBack(Time.deltaTime);
        }
    }

    private void Atack(float deltaTime, int dir)
    {
        switch(dir)
        {
            case 0:
                {
                    transform.position = Vector2.MoveTowards(curPos, pos0, speed * deltaTime);
                    if (curPos == pos0)
                    {
                        
                        timeStay += deltaTime;
                        if (timeStay > 2.0f)
                        {
                            isBack = true;
                            timeStay = 0;
                        }
                    }
                    break;
                }
            case 1:
                {
                    transform.position = Vector2.MoveTowards(curPos, pos2, speed * deltaTime);
                    if (curPos == pos2)
                    {
                        timeFire += Time.deltaTime;
                        if (timeFire > speedFire)
                        {
                            Instantiate(bullet, new Vector3(curPos.x - 1.1f, curPos.y, 0), Quaternion.identity);
                            timeFire = 0.0f;
                        }

                        timeStay += deltaTime;
                        if (timeStay > 3.0f)
                        {
                            isBack = true;
                            timeStay = 0;
                        }
                    }
                    break;
                }
            case 2:
                {
                    transform.position = Vector2.MoveTowards(curPos, pos3, speed * deltaTime);
                    if (curPos == pos3)
                    {
                        timeFire += Time.deltaTime;
                        if (timeFire > speedFire)
                        {
                            Instantiate(bullet, new Vector3(curPos.x - 1.1f, curPos.y, 0), Quaternion.identity);
                            timeFire = 0.0f;
                        }

                        timeStay += deltaTime;
                        if (timeStay > 2.5f)
                        {
                            isBack = true;
                            timeStay = 0;
                        }
                    }
                    break;
                }
        }
        
    }

    private void MoveBack(float deltaTime)
    {
        transform.position = Vector2.MoveTowards(curPos, pos1, speed * deltaTime);
        if(curPos == pos1)
        {
            timeFire += Time.deltaTime;
            if (timeFire > speedFire)
            {
                Instantiate(bullet, new Vector3(curPos.x - 1.1f, curPos.y, 0), Quaternion.identity);
                timeFire = 0.0f;
            }

            timeStay += deltaTime;
            if (timeStay > 1.0f)
            {
                isBack = false;
                dir = Random.Range(0, 3);
                timeStay = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            hp -= 2;
            MenuController.instance.SetHPBossTxt(hp);

            if (hp == 0)
            {
                isDie = true;
                playerCtrl.CheckFinish = true;

                MenuController.instance.SetGiftPanel(isDie);
                Time.timeScale = 0;
            }
        }
    }

    public bool CheckDie
    {
        get
        {
            return isDie;
        }
    }

    public int GetHP
    {
        get
        {
            Debug.Log(hp);

            return hp;
        }
    }
}
