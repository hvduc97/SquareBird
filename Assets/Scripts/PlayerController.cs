using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 curPos, revPos;
    private Rigidbody2D rb2d;
    private bool isBare, isDie, isFinish, isPerfect, isFire, isWaitRevival;

    private int score, point, countScore, numChild;
    private float timePoint;
    
    [SerializeField] private GameObject playerClone;
    private GameObject[] childs;

    private int level;
    private int revival;

    private GameObject rev;

    private void Awake()
    {
        level = 1;
        revival = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;

        rb2d = transform.GetComponent<Rigidbody2D>();

        isDie = false;
        isFinish = false;
        isPerfect = false;
        isFire = false;
        isWaitRevival = false;

        score = 0;
        point = 0;
        MenuController.instance.SetScoreTxt(score);
        MenuController.instance.SetPointTxt(point);

        countScore = 0;
        numChild = 0;

        timePoint = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timePoint += Time.deltaTime;
        if(timePoint > 1.0f)
        {
            point++;
            MenuController.instance.SetPointTxt(point);
            timePoint = 0;
        }

        curPos = transform.position;
        if(!isBare && !isDie && !isFinish &&!isWaitRevival)
        {
#if !UNITY_EDITOR
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                Time.timeScale = 1;
                Instantiate(playerClone, curPos, Quaternion.identity);
                transform.position = new Vector3(curPos.x, curPos.y + 1.0f, 0);
            }
        }
#else
            if (Input.GetKeyDown("space"))
            {
                Time.timeScale = 1;
                Instantiate(playerClone, curPos, Quaternion.identity);
                transform.position = new Vector3(curPos.x, curPos.y + 1.0f, 0);
            }
#endif
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Finish")
        {
            level = 2;
            childs = GameObject.FindGameObjectsWithTag("PlayerClone");
            numChild = childs.Length;
            revival += numChild;

            for(int i = 0; i < numChild; i++)
            {
                Destroy(childs[i]);
            }

            isFinish = true;
            MenuController.instance.SetWinPanel(isFinish);
            Time.timeScale = 0;
        }

        if (collision.gameObject.tag == "MapUp")
        {
            isBare = true;
            //Debug.Log("Enter");
        }

        if (collision.gameObject.tag == "Bomb")
        {
            Destroy(collision.gameObject);

            Time.timeScale = 0;

            if(revival > 0)
            {
                revival--;
                MenuController.instance.SetRevivalPanel(!isDie);
                isWaitRevival = true;
            }
            else
            {
                isDie = true;
                MenuController.instance.SetLossPanel(isDie);
            }
            
        }

        if (collision.gameObject.tag == "Boss")
        {
            Time.timeScale = 0;
            if (revival > 0)
            {
                revival--;
                MenuController.instance.SetRevivalPanel(!isDie);
                isWaitRevival = true;
            }
            else
            {
                isDie = true;
                MenuController.instance.SetLossPanel(isDie);
            }
           
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MapUp")
        {
            isBare = true;
            //Debug.Log("Stay");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MapUp")
        {
            isBare = false;
            //Debug.Log("Exit");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Loss")
        {
            Time.timeScale = 0;
            if(revival > 0)
            {
                revival--;
                MenuController.instance.SetRevivalPanel(!isDie);
                isWaitRevival = true;
            }
            else
            {
                isDie = true;
                MenuController.instance.SetLossPanel(isDie);
            }
            
        }

        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);

            Time.timeScale = 0;
            if (revival > 0)
            {
                revival--;
                MenuController.instance.SetRevivalPanel(!isDie);
                isWaitRevival = true;
            }
            else
            {
                isDie = true;
                MenuController.instance.SetLossPanel(isDie);
            }
        }

        if (collision.gameObject.tag == "Score")
        {
            Destroy(collision.gameObject);

            score++;
            MenuController.instance.SetScoreTxt(score);
            isPerfect = true;
            MenuController.instance.SetPerfectTxt(isPerfect);

            countScore++;
            if(countScore == 3)
            {
                isFire = true;
                countScore = 0;
            }
        }

        if (collision.gameObject.tag == "Item")
        {
            transform.position = new Vector3(curPos.x, curPos.y + 1.0f, 0);
            Instantiate(playerClone, curPos, Quaternion.identity);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Revival")
        {
            rev = collision.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Score")
        {
            isPerfect = true;
            MenuController.instance.SetPerfectTxt(isPerfect);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Score")
        {
            isPerfect = false;
            MenuController.instance.SetPerfectTxt(isPerfect);
        }
    }

    public int GetPoint
    {
        get
        {
            return point;
        }
    }

    public int GetScore
    {
        get
        {
            return score;
        }
    }

    public int GetNumOfChild
    {
        get
        {
            return numChild;
        }
    }

    public bool CheckPerfect
    {
        get
        {
            return isPerfect;
        }
    }

    public bool CheckDie
    {
        get
        {
            return isDie;
        }
        set
        {
            isDie = value;
        }
        
    }

    public bool CheckFinish
    {
        get
        {
            return isFinish;
        }
        set
        {
            isFinish = value;
        }
    }

    public bool CheckFire
    {
        get
        {
            return isFire;
        }
        set
        {
            isFire = value;
        }
    }

    public int GetLevel
    {
        get
        {
            return level;
        }
    }

    public float SetRevival(int level)
    {
        switch(level)
        {
            case 1:
                {
                    childs = GameObject.FindGameObjectsWithTag("PlayerClone");
                    numChild = childs.Length;

                    for (int i = 0; i < numChild; i++)
                    {
                        Destroy(childs[i]);
                    }

                    revPos = rev.transform.position;

                    transform.position = new Vector3(transform.position.x, revPos.y, 0);

                    isWaitRevival = false;

                    break;
                }
            case 2:
                {
                    childs = GameObject.FindGameObjectsWithTag("PlayerClone");
                    numChild = childs.Length;

                    for (int i = 0; i < numChild; i++)
                    {
                        Destroy(childs[i]);
                    }

                    revPos = new Vector2(-1.5f, 2.5f);
                    transform.position = new Vector3(transform.position.x, revPos.y, 0);
                    isWaitRevival = false;

                    break;
                }
        }

        return (transform.position.x - revPos.x);

    }
}
