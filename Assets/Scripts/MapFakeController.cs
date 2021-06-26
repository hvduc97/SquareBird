using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFakeController : MonoBehaviour
{
    private GameObject player;
    private Vector2 posPlayer;
    private bool check;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        check = false;
    }

    // Update is called once per frame
    void Update()
    {
        posPlayer = player.transform.position;
        if (!check)
        {
            if (Vector2.Distance(transform.position, posPlayer) < 3.5f)
            {
                if (transform.position.y < posPlayer.y)
                {
                    transform.Translate(new Vector3(0, 1.0f, 0));
                    check = true;
                }
                else
                {
                    transform.Translate(new Vector3(0, -1.0f, 0));
                    check = true;
                }
            }
        }
    }
}
