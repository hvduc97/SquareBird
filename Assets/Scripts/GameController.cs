using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Android;

public class GameController : MonoBehaviour
{
    GameObject player, playerClone;
    Vector2 posPlayer;
    public GameObject objClone, bullet;
    public Button btnRestart, btnExit;
    public Text txtPoint;
    private int point;
    private float countTime = 0.0f,  timeFire = 0.0f;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        txtPoint.text = "Point: " + point.ToString();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        posPlayer = player.transform.position;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        posPlayer = player.transform.position;


        if (playerController.isFire)
        {

            countTime += Time.deltaTime;
            if (countTime > 0.2f)
            {
                Instantiate(bullet, new Vector3(posPlayer.x + 0.6f, posPlayer.y, 0), Quaternion.identity);
                countTime = 0.0f;
            }
            timeFire += Time.deltaTime;
            if(timeFire > 3.0f)
            {
                playerController.isFire = false;
                timeFire = 0.0f;
            }
        }


        if (!player.GetComponent<PlayerController>().CheckDie)
        {
            if (posPlayer.y < 3.5f)
            {
#if !UNITY_EDITOR
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Began)
                    {
                        player.transform.position = new Vector3(posPlayer.x, posPlayer.y + 1.0f, 0);
                        playerClone = Instantiate(objClone, posPlayer, Quaternion.identity);
                    }
                }
#else
                if (Input.GetKeyDown("space"))
                {
                    player.transform.position = new Vector3(posPlayer.x, posPlayer.y + 1.1f, 0);
                    playerClone = Instantiate(objClone, posPlayer, Quaternion.identity);
                }
#endif
            }
        }
    }

    public void ShowPoint()
    {
        point++;
        txtPoint.text = "Point: " + point.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    public int GetPoint
    {
        get
        {
            return point;
        }
    }

}
