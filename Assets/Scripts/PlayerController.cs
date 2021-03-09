using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public GameObject obj;

    GameObject gameCtr;

    GameObject[] playerClone;

    Vector2 posOld, posCurrent;

    public GameObject pnLoss, pnWin;
    public Text txtGoal, txtScore, txtPerfect;
    int score = 0, numPlayerCl, countScore = 0;
    float countTime = 0.0f;

    private bool isDie;
    public bool isFire;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
        posOld = transform.position;

        gameCtr = GameObject.FindGameObjectWithTag("GameController");

        pnLoss.SetActive(false);
        pnWin.SetActive(false);
        txtPerfect.transform.gameObject.SetActive(false);

        numPlayerCl = 0;
        txtScore.text = "Score: " + score.ToString();

        isDie = false;
        isFire = false;
    }

    // Update is called once per frame
    void Update()
    {
        posCurrent = transform.position;
        if(posCurrent.y < -4.5f)
        {
            Time.timeScale = 0;
            isDie = true;
            pnLoss.SetActive(true);
        }
        countTime += Time.deltaTime;
        if(countTime > 0.5f)
        {
            txtPerfect.transform.gameObject.SetActive(false);
            countTime = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Item")
        {
            string name;
            transform.position = new Vector3(posCurrent.x, posCurrent.y + 1.0f, 0);
            Instantiate(obj, posCurrent, Quaternion.identity);
            name = collision.gameObject.name;
            Destroy(GameObject.Find(name));
        }

        //if (collision.gameObject.tag == "Map")
        //{
        //    Debug.Log("Destroy Map");
        //}

    }

    private void ShowGoal()
    {
        int point = gameCtr.GetComponent<GameController>().GetPoint;
        txtGoal.text = "Victory: " + (point + score*10 + numPlayerCl * 100).ToString();
        gameCtr.GetComponent<GameController>().txtPoint.transform.gameObject.SetActive(false);
        txtScore.transform.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Loss")
        {
            txtPerfect.transform.gameObject.SetActive(false);
            pnLoss.SetActive(true);
            Time.timeScale = 0;
            isDie = true;
        }

        if (collision.gameObject.tag == "Point")
        {
            gameCtr.GetComponent<GameController>().ShowPoint();
        }
        
        if (collision.gameObject.tag == "Finish")
        {
            playerClone = GameObject.FindGameObjectsWithTag("PlayerClone");
            for(int i = 0; i < playerClone.Length; i++)
            {
                numPlayerCl++;
            }
            ShowGoal();
            pnWin.SetActive(true);
            Time.timeScale = 0;
            isDie = true;
        }

        if (collision.gameObject.tag == "Score")
        {
            score++;
            txtPerfect.transform.gameObject.SetActive(true);
            txtScore.text = "Score: " + score.ToString();

            countScore++;
            if(countScore == 2)
            {
                isFire = true;
                countScore = 0;
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

    public int Score
    {
        get
        {
            return score;
        }
    }
}
