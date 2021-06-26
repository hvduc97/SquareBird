using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject pnLoss, pnWin, pnGift, pnRevival;
    [SerializeField] private Button btnReplayLoss, btnPlayWin, btnExitLoss, btnExitWin, btnHPBoss, btnRevival;
    [SerializeField] private Text txtScore, txtPoint, txtPerfect, txtGoalLoss, txtGoalWin, txtHPBoss;

    private GameObject player;
    private PlayerController playerCtrl;

    private GameObject objGameCtrl;
    private GameController compGameCtrl;

    private int level;
    private float timeShowPerfect;
    public static MenuController instance;

    private GameObject level1, level2;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCtrl = player.GetComponent<PlayerController>();
        
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        level = playerCtrl.GetLevel;
        //Debug.Log(level);

        level1 = Instantiate(Resources.Load("Prefabs/Level1", typeof(GameObject))) as GameObject;

        txtScore.gameObject.SetActive(true);
        txtPoint.gameObject.SetActive(true);

        txtPerfect.gameObject.SetActive(false);

        txtHPBoss.gameObject.SetActive(false);
        btnHPBoss.gameObject.SetActive(false);

        pnGift.gameObject.SetActive(false);
        pnLoss.gameObject.SetActive(false);
        pnWin.gameObject.SetActive(false);



        btnReplayLoss.onClick.AddListener(BtnReplayOnClick);
        btnPlayWin.onClick.AddListener(BtnPlayOnClick);

        btnExitLoss.onClick.AddListener(BtnExitOnClick);
        btnExitWin.onClick.AddListener(BtnExitOnClick);

        btnRevival.onClick.AddListener(BtnRevivalOnClick);

        timeShowPerfect = 0.0f;

        //switch (level)
        //{
        //    case 1:
        //        {
        //            GameObject level1 = Instantiate(Resources.Load("Prefabs/Level1", typeof(GameObject))) as GameObject;

        //            txtPerfect.gameObject.SetActive(false);

        //            btnReplayLoss.onClick.AddListener(BtnReplayOnClick);
        //            btnReplayWin.onClick.AddListener(BtnReplayOnClick);

        //            btnExitLoss.onClick.AddListener(BtnExitOnClick);
        //            btnExitWin.onClick.AddListener(BtnExitOnClick);

        //            timeShowPerfect = 0.0f;
        //            break;
        //        }
        //    case 2:
        //        {
        //            GameObject level2 = Instantiate(Resources.Load("Prefabs/LevelBoss1", typeof(GameObject))) as GameObject;

        //            btnReplayLoss.onClick.AddListener(BtnReplayOnClick);
        //            btnReplayWin.onClick.AddListener(BtnReplayOnClick);

        //            btnExitLoss.onClick.AddListener(BtnExitOnClick);
        //            btnExitWin.onClick.AddListener(BtnExitOnClick);

        //            txtPoint.gameObject.SetActive(false);
        //            //txtScore.transform.position = new Vector3(730.0f, -80.0f, 0.0f);

        //            break;
        //        }
        //}

    }

    // Update is called once per frame
    void Update()
    {
        level = playerCtrl.GetLevel;

        switch (level)
        {
            case 1:
                {
                    
                    if (txtPerfect.IsActive())
                    {
                        timeShowPerfect += Time.deltaTime;
                        if(timeShowPerfect > 0.5f)
                        {
                            txtPerfect.gameObject.SetActive(false);
                            timeShowPerfect = 0.0f;
                        }
                    }
                    

                    
                    break;
                }
            case 2:
                {
                    break;
                }
        }

    }

    private void BtnPlayOnClick()
    {
        if (level2 == null)
        {
            
            Destroy(level1);
            Time.timeScale = 1;

            playerCtrl.CheckFinish = false;

            pnWin.gameObject.SetActive(false);
            playerCtrl.CheckDie = false;

            level2 = Instantiate(Resources.Load("Prefabs/LevelBoss1", typeof(GameObject))) as GameObject;
            txtPoint.gameObject.SetActive(false);
            txtScore.gameObject.SetActive(false);

            txtHPBoss.gameObject.SetActive(true);
            btnHPBoss.gameObject.SetActive(true);
        }
        else
        {
            Destroy(level2);
            Time.timeScale = 1;

            SceneManager.LoadScene(1);
        }
    }

    private void BtnReplayOnClick()
    {
        if(level2 == null)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        }
        else
        {
            Destroy(level2);
            Time.timeScale = 1;

            playerCtrl.CheckDie = false;

            level2 = Instantiate(Resources.Load("Prefabs/LevelBoss1", typeof(GameObject))) as GameObject;
            pnLoss.gameObject.SetActive(false);
            txtScore.gameObject.SetActive(false);

            txtHPBoss.text = "100";
            btnHPBoss.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }

    private void BtnExitOnClick()
    {
        switch (level)
        {
            case 1:
                {
                    SceneManager.LoadScene(0);
                    break;
                }
            case 2:
                {
                    SceneManager.LoadScene(0);
                    break;
                }
        }
    }

    private void BtnRevivalOnClick()
    {
        switch (level)
        {
            case 1:
                {
                    Time.timeScale = 1.0f;
                    ObjectMoveController.instance.Revival(playerCtrl.SetRevival(1));
                    pnRevival.gameObject.SetActive(false);
                    
                    break;
                }
            case 2:
                {
                    Time.timeScale = 1.0f;
                    playerCtrl.SetRevival(2);
                    pnRevival.gameObject.SetActive(false);

                    break;
                }
        }
    }

    public void SetPerfectTxt(bool isPerfect)
    {
        txtPerfect.gameObject.SetActive(isPerfect);
    }

    public void SetScoreTxt(int score)
    {
        txtScore.text = "Score:" + score.ToString();
    }

    public void SetPointTxt(int point)
    {
        txtPoint.text = "Point:" + point.ToString();
    }

    public void SetHPBossTxt(int hp)
    {
        txtHPBoss.text = hp.ToString();
        btnHPBoss.transform.localScale = new Vector3((float)hp / 100.0f, 1, 1);
    }

    public void SetWinPanel(bool isWin)
    {
        switch(level)
        {
            case 1:
                {
                    txtPerfect.gameObject.SetActive(false);

                    txtScore.gameObject.SetActive(false);
                    txtPoint.gameObject.SetActive(false);

                    txtHPBoss.gameObject.SetActive(false);
                    btnHPBoss.gameObject.SetActive(false);

                    pnLoss.gameObject.SetActive(false);
                    pnGift.gameObject.SetActive(false);

                    txtGoalWin.text = (playerCtrl.GetNumOfChild * 100 + playerCtrl.GetScore * 10 + playerCtrl.GetPoint).ToString();
                    pnWin.gameObject.SetActive(isWin);

                    break;
                }
            case 2:
                {
                    txtGoalWin.gameObject.SetActive(false);
                    pnWin.gameObject.SetActive(isWin);

                    break;
                }
        }
    }

    public void SetLossPanel(bool isLoss)
    {
        switch (level)
        {
            case 1:
                {
                    txtPerfect.gameObject.SetActive(false);

                    txtScore.gameObject.SetActive(false);
                    txtPoint.gameObject.SetActive(false);

                    txtHPBoss.gameObject.SetActive(false);
                    btnHPBoss.gameObject.SetActive(false);

                    pnWin.gameObject.SetActive(false);
                    pnGift.gameObject.SetActive(false);


                    txtGoalLoss.text = (playerCtrl.GetScore * 10 + playerCtrl.GetPoint).ToString();
                    pnLoss.gameObject.SetActive(isLoss);

                    break;
                }
            case 2:
                {
                    txtGoalLoss.gameObject.SetActive(false);
                    pnLoss.gameObject.SetActive(isLoss);

                    break;
                }
        }
    }

    public void SetGiftPanel(bool isPass)
    {
        txtPerfect.gameObject.SetActive(false);
        txtScore.gameObject.SetActive(false);
        txtPoint.gameObject.SetActive(false);

        txtHPBoss.gameObject.SetActive(false);
        btnHPBoss.gameObject.SetActive(false);

        pnGift.gameObject.SetActive(isPass);
    }

    public void SetRevivalPanel(bool isRevial)
    {
        pnRevival.gameObject.SetActive(isRevial);

        pnGift.gameObject.SetActive(false);
        pnLoss.gameObject.SetActive(false);
        pnWin.gameObject.SetActive(false);
    }

}
