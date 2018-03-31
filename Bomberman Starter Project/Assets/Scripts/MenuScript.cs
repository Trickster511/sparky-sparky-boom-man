using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class MenuScript : MonoBehaviour {

    #region Singleton

    public static MenuScript instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject pm;
    public GameObject gm;
    public Text livesText;
    public GameObject gom;
    public int lives = 3;
    public GameObject player;
    public Transform spawnPoint;

    public Button nextButton;
    public Button restartButton;
    public Button mainMenuButton;

    public Tilemap tilemap;

    private string levelToRestart;

    void Start ()
    {
        levelToRestart = SceneManager.GetActiveScene().name;
        lives = PlayerPrefs.GetInt("Lives");
        livesText.text = "Lives: " + lives;
        if(lives <= 0)
        {
            restartButton.interactable = false;
        }
        else
        {
            restartButton.interactable = true;
        }
    }

    void Update()
    {
        if(Input.GetButtonDown("Start"))
        {
            if(!gm.activeSelf)
            PToggle();
        }
    }

    public void PToggle()
    {
        pm.SetActive(!pm.activeSelf);

        restartButton.Select();

        if (pm.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void GToggle()
    {
        gm.SetActive(!gm.activeSelf);

        nextButton.Select();

        if (gm.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void GOToggle()
    {
        gom.SetActive(!gom.activeSelf);

        mainMenuButton.Select();

        if (gom.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public IEnumerator LoseLife(float delay)
    {
        Destroy(player);
        if (lives == 0)
        {
            yield return new WaitForSeconds(.5f);
            GOToggle();
            Debug.Log("Game over bitch!!");
            PlayerPrefs.SetInt("Lives", 3);
            yield break;
        }
        yield return new WaitForSeconds(delay);
        lives--;
        livesText.text = "Lives: " + lives;
        PlayerPrefs.SetInt("Lives", lives);
        SceneManager.LoadScene(levelToRestart);
    }

    public void PlayerDied()
    {
        StartCoroutine(LoseLife(2f));
    }

    public void NextLevel ()
    {
        Debug.Log("Heading to next level...");
    }

    public void Restart ()
    {
        if(lives <= 0)
        {
            return;
        }
        lives--;
        PlayerPrefs.SetInt("Lives", lives);
        PToggle();
        SceneManager.LoadScene(levelToRestart);
    }

    public void GOMainMenu()
    {
        GOToggle();
        SceneManager.LoadScene(0);
    }

    public void MainMenu()
    {
        GToggle();
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Debug.Log("Quiting...");
        Application.Quit();
    }
}
