using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private Button btnPlay;

    // Start is called before the first frame update
    void Start()
    {
        btnPlay.onClick.AddListener(OnClickButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnClickButton()
    {
        SceneManager.LoadScene("PlayScene");
    }
}
