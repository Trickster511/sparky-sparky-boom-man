using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MultiplayerManager : MonoBehaviour {

    #region Singleton

    public static MultiplayerManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject pm;
    public GameObject wm;
    public GameObject hor;
    public Text wText;
    public Text p1Text;
    public Text p2Text;
    public Text p3Text;
    public Text p4Text;
    public Text gwText;
    public Button btmmButton;

    private int p1Score;
    private int p2Score;
    private int p3Score;
    private int p4Score;

    GameObject[] allPlayers;

    public Transform[] spawnPoints;
    public GameObject player3;
    public GameObject player4;
    public Button restartButton;

    public Tilemap tilemap;

    private string levelToRestart;

    void Start ()
    {
        p1Score = PlayerPrefs.GetInt("P1Score");
        p2Score = PlayerPrefs.GetInt("P2Score");
        p3Score = PlayerPrefs.GetInt("P3Score");
        p4Score = PlayerPrefs.GetInt("P4Score");

        if (PlayerPrefs.GetInt("Players") > 2)
        {
            SpawnPlayers();
        }
        Physics2D.IgnoreLayerCollision(8, 8);
        levelToRestart = SceneManager.GetActiveScene().name;
    }
	
	void Update ()
    {
        if (Input.GetButtonDown("Start"))
        {
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

    public void WToggle()
    {
        wm.SetActive(!wm.activeSelf);

        if (wm.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    void SpawnPlayers()
    {
        if(PlayerPrefs.GetInt("Players") == 3)
        {
            Instantiate(player3, spawnPoints[0].position, Quaternion.identity);
            p3Text.gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Players") == 4)
        {
            Instantiate(player3, spawnPoints[0].position, Quaternion.identity);
            p3Text.gameObject.SetActive(true);

            Instantiate(player4, spawnPoints[1].position, Quaternion.identity);
            p4Text.gameObject.SetActive(true);
        }
    }

    public IEnumerator CheckForWinner(float cDelay)
    {
        yield return new WaitForSeconds(cDelay);
        allPlayers = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log("Length: " + allPlayers.GetLength(0));
        if(allPlayers.Length != 0)
        {
            for (int i = 0; i < allPlayers.GetLength(0); i++)
            {
                Debug.Log("Players left: " + allPlayers.GetValue(i));
            }
        }
        
        if (allPlayers.Length == 1)
        {
            int gWinner = 0;
            bool isGWinner = false;
            GameObject winner = (GameObject)allPlayers.GetValue(0);
            wText.text = "Player " + winner.GetComponent<PlayerController>().numPlayer + " Wins!!";
            if(winner.GetComponent<PlayerController>().numPlayer == 1)
            {
                p1Score++;
                PlayerPrefs.SetInt("P1Score", p1Score);
                if(p1Score == PlayerPrefs.GetInt("MaxPoints"))
                {
                    gWinner = 1;
                    isGWinner = true;
                }
            }
            if (winner.GetComponent<PlayerController>().numPlayer == 2)
            {
                p2Score++;
                PlayerPrefs.SetInt("P2Score", p2Score);
                if (p2Score == PlayerPrefs.GetInt("MaxPoints"))
                {
                    gWinner = 2;
                    isGWinner = true;
                }
            }
            if (winner.GetComponent<PlayerController>().numPlayer == 3)
            {
                p3Score++;
                PlayerPrefs.SetInt("P3Score", p3Score);
                if (p3Score == PlayerPrefs.GetInt("MaxPoints"))
                {
                    gWinner = 3;
                    isGWinner = true;
                }
            }
            if (winner.GetComponent<PlayerController>().numPlayer == 4)
            {
                p4Score++;
                PlayerPrefs.SetInt("P4Score", p4Score);
                if (p4Score == PlayerPrefs.GetInt("MaxPoints"))
                {
                    gWinner = 4;
                    isGWinner = true;
                }
            }
            p1Text.text = "P1 " + PlayerPrefs.GetInt("P1Score");
            p2Text.text = "P2 " + PlayerPrefs.GetInt("P2Score");
            p3Text.text = "P3 " + PlayerPrefs.GetInt("P3Score");
            p4Text.text = "P4 " + PlayerPrefs.GetInt("P4Score");

            WToggle();
            yield return new WaitForSecondsRealtime(5f);
            if(isGWinner)
            {
                GrandWinner(gWinner);
                yield break;
            }
            WToggle();
            SceneManager.LoadScene(levelToRestart);
        }
    }

    public IEnumerator CheckForAllDead(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (allPlayers.Length == 0)
        {
            p1Text.text = "P1 " + PlayerPrefs.GetInt("P1Score");
            p2Text.text = "P2 " + PlayerPrefs.GetInt("P2Score");
            p3Text.text = "P3 " + PlayerPrefs.GetInt("P3Score");
            p4Text.text = "P4 " + PlayerPrefs.GetInt("P4Score");
            wText.text = "Draw!!";
            wm.SetActive(true);
            yield return new WaitForSecondsRealtime(3f);
            SceneManager.LoadScene(levelToRestart);
        }
        yield break;
    }

    public void PlayerDied(GameObject player)
    {
        Destroy(player);
        StartCoroutine(CheckForWinner(0.1f));
        StartCoroutine(CheckForAllDead(0.5f));
    }

    void GrandWinner(int thewinner)
    {
        if(thewinner == 1)
        {
            hor.SetActive(false);
            wText.gameObject.SetActive(false);
            gwText.gameObject.SetActive(true);
            gwText.text = "Congrats Player " + thewinner + ", you won the game!";
            btmmButton.gameObject.SetActive(true);
            btmmButton.Select();
        }
    }

    public void BackToMainMenu()
    {
        WToggle();
        SceneManager.LoadScene(0);
    }

    public void MainMenu()
    {
        PToggle();
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        PToggle();
        SceneManager.LoadScene(levelToRestart);
    }

    public void Quit()
    {
        Debug.Log("Quiting...");
        Application.Quit();
    }
}
