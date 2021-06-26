using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBossController : MonoBehaviour
{
    private float speed = 3.5f;
    private float range = -3.0f;

    //private GameObject player;
    //private PlayerController playerCtrl;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //playerCtrl = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-1.0f * Time.deltaTime * speed, 0, 0));
        if (transform.position.x < range)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
