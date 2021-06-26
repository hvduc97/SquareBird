using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float speed = 10.0f;
    private float range = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(1.0f * Time.deltaTime * speed, 0, 0));
        if (transform.position.x > range)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Loss")
        {
            Destroy(collision.gameObject);
            
        }

        if (collision.gameObject.tag == "MapDown")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            
        }

        if (collision.gameObject.tag == "MapUp")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }

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



}
