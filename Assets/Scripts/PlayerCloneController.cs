using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCloneController : MonoBehaviour
{
    //private Rigidbody2D rb2d;
    private Vector2 oldPos, posPlayer;

    private GameObject player;
    private PlayerController playerCtr;

    private GameObject objGameCtrl;
    private GameController compGameCtrl;

    private int level;
    private float timeLife;

    private void Awake()
    {
        objGameCtrl = GameObject.FindGameObjectWithTag("GameController");
        compGameCtrl = objGameCtrl.GetComponent<GameController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //rb2d = transform.GetComponent<Rigidbody2D>();
        oldPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        playerCtr = player.GetComponent<PlayerController>();

        level = playerCtr.GetLevel;
        timeLife = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        switch(level)
        {
            case 1:
                {
                    posPlayer = player.transform.position;

                    if (Mathf.Abs(oldPos.x - transform.position.x) > 2.0f)
                    {
                        Destroy(this.gameObject);
                    }
                    break;
                }
            case 2:
                {
                    timeLife += Time.deltaTime;
                    if(timeLife > 0.7f)
                    {
                        Destroy(this.gameObject);
                        timeLife = 0.0f;
                    }
                    break;
                }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bomb")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Boss")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Loss")
        {
            transform.SetParent(collision.transform.parent);
        }

        if (collision.gameObject.tag == "Item")
        {
            player.transform.position = new Vector3(posPlayer.x, posPlayer.y + 1.0f, 0);
            Instantiate(this, posPlayer, Quaternion.identity);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }
    }


}
