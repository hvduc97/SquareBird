using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bomb;
    private GameObject player;
    private PlayerController playerCtr;
    private Vector2 posPlayer, posBomb;
    private float speedFire, timeFire;
    private int level;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCtr = player.GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        level = playerCtr.GetLevel;

        switch (level)
        {
            case 1:
                {
                    //GameObject level1 = Instantiate(Resources.Load("Prefabs/Level1", typeof(GameObject))) as GameObject;

                    speedFire = 0.0f;
                    timeFire = 0.0f;

                    for (int i = 0; i < 4; i++)
                    {
                        int num = Random.Range(2, 50);
                        posBomb.x = num + 0.5f;
                        Instantiate(bomb, new Vector3(posBomb.x, 0, 0), Quaternion.identity);
                    }
                    break;
                }
            case 2:
                {
                    //GameObject level2 = Instantiate(Resources.Load("Prefabs/LevelBoss1", typeof(GameObject))) as GameObject;

                    player = GameObject.FindGameObjectWithTag("Player");
                    playerCtr = player.GetComponent<PlayerController>();

                    speedFire = 0.0f;

                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        level = playerCtr.GetLevel;

        switch (level)
        {
            case 1:
                {
                    posPlayer = player.transform.position;

                    if (playerCtr.CheckFire)
                    {
                        speedFire += Time.deltaTime;
                        if (speedFire > 0.2f)
                        {
                            Instantiate(bullet, new Vector3(posPlayer.x + 0.7f, posPlayer.y, 0), Quaternion.identity);
                            speedFire = 0.0f;
                        }

                        timeFire += Time.deltaTime;
                        if (timeFire > 2.0f)
                        {
                            playerCtr.CheckFire = false;
                            timeFire = 0.0f;
                        }

                    }
                    break;
                }
            case 2:
                {
                    posPlayer = player.transform.position;
                    speedFire += Time.deltaTime;
                    if (speedFire > 0.2f)
                    {
                        Instantiate(bullet, new Vector3(posPlayer.x + 0.7f, posPlayer.y, 0), Quaternion.identity);
                        speedFire = 0.0f;
                    }
                    break;
                }
        }
        
    }

}
