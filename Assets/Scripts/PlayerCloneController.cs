using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCloneController : MonoBehaviour
{
    Vector2 oldPos, posPlayer, currPos;
    GameObject player;
    [SerializeField] private Rigidbody2D rb;
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        oldPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (playerController.CheckDie)
        {
            return;
        }
        posPlayer = player.transform.position;

        if (Mathf.Abs(oldPos.x - transform.position.x) > 2.0f)
        {
            Destroy(this.transform.gameObject);
        }

        currPos = this.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string name;
        if (collision.gameObject.tag == "Item")
        {
            player.transform.position = new Vector3(posPlayer.x, posPlayer.y + 1.0f, 0);
            Instantiate(this, posPlayer, Quaternion.identity);
            name = collision.gameObject.name;
            Destroy(GameObject.Find(name));
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Loss")
        {
            transform.SetParent(collision.transform.parent);
        }
    }
}
