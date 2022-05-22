using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private static bool gameIsPaused;
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject GameOverMenu;
    public GameObject teleport;
    public int enemyCount= 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void GameOver()
    {
        GameOverMenu.SetActive(true);
        PauseGame();
        
    }

    public void PauseGame()
    {
        gameIsPaused = !gameIsPaused;
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            HUD.SetActive(false);
            Menu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            HUD.SetActive(true);
            Menu.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();// for built appss
        UnityEditor.EditorApplication.isPlaying = false;
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {           
            PauseGame();
        }

        if(enemyCount <=0)
        {
            teleport.SetActive(true);
        }
    }
}
