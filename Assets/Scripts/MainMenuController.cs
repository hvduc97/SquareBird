using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnExit;

    // Start is called before the first frame update
    void Start()
    {
        btnPlay.onClick.AddListener(BtnPlayOnClick);
        btnExit.onClick.AddListener(BtnExitOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BtnPlayOnClick()
    {
        SceneManager.LoadScene(1);
    }

    private void BtnExitOnClick()
    {
        Application.Quit();
    }
}
