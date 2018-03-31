using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {

    public GameObject mm;
    public GameObject mpm;
    public GameObject psm;

    private int page;

    public Button startButton;
    public Button localButton;
    public Button twoPButton;

    public Text selectedText;
    public Text pointsText;
    public Text stageSelectedText;

    private int maxPoints = 3;
    private int players;
    private int levelToLoad = 2;

	// Use this for initialization
	void Start ()
    {
        page = 1;
        PlayerPrefs.SetInt("Lives", 3);
        PlayerPrefs.SetInt("MaxPoints", maxPoints);
	}

    public void MMToggle()
    {
        mm.SetActive(!mm.activeSelf);
        page = 1;
        startButton.Select();
        mpm.SetActive(false);
        psm.SetActive(false);
    }
    public void MPToggle()
    {
        mpm.SetActive(!mpm.activeSelf);
        page = 2;
        localButton.Select();
        mm.SetActive(false);
        psm.SetActive(false);
    }
    public void PSMToggle()
    {
        psm.SetActive(!psm.activeSelf);
        page = 3;
        twoPButton.Select();
        mpm.SetActive(false);
        mm.SetActive(false);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void Multiplayer()
    {
        MPToggle();
    }

    public void Local()
    {
        PSMToggle();
    }

    public void TwoP ()
    {
        PlayerPrefs.SetInt("Players", 2);
        players = PlayerPrefs.GetInt("Players");
        if (!selectedText.gameObject.activeSelf)
            selectedText.gameObject.SetActive(true);
        selectedText.text = players + " Players Selected!";
    }

    public void ThreeP()
    {
        PlayerPrefs.SetInt("Players", 3);
        players = PlayerPrefs.GetInt("Players");
        if (!selectedText.gameObject.activeSelf)
            selectedText.gameObject.SetActive(true);
        selectedText.text = players + " Players Selected!";
    }

    public void FourP()
    {
        PlayerPrefs.SetInt("Players", 4);
        players = PlayerPrefs.GetInt("Players");
        if (!selectedText.gameObject.activeSelf)
            selectedText.gameObject.SetActive(true);
        selectedText.text = players + " Players Selected!";
    }

    public void Stage1()
    {
        levelToLoad = 2;
        if (!stageSelectedText.gameObject.activeSelf)
            stageSelectedText.gameObject.SetActive(true);
        stageSelectedText.text = "Stage " + (levelToLoad - 1) + " Selected!";
    }
    public void Stage2()
    {
        levelToLoad = 3;
        if (!stageSelectedText.gameObject.activeSelf)
            stageSelectedText.gameObject.SetActive(true);
        stageSelectedText.text = "Stage " + (levelToLoad - 1) + " Selected!";
    }
    public void Stage3()
    {
        levelToLoad = 4;
        if (!stageSelectedText.gameObject.activeSelf)
            stageSelectedText.gameObject.SetActive(true);
        stageSelectedText.text = "Stage " + (levelToLoad - 1) + " Selected!";
    }
    public void Stage4()
    {
        levelToLoad = 5;
        if (!stageSelectedText.gameObject.activeSelf)
            stageSelectedText.gameObject.SetActive(true);
        stageSelectedText.text = "Stage " + (levelToLoad - 1) + " Selected!";
    }
    public void Stage5()
    {
        levelToLoad = 6;
        if (!stageSelectedText.gameObject.activeSelf)
            stageSelectedText.gameObject.SetActive(true);
        stageSelectedText.text = "Stage " + (levelToLoad - 1) + " Selected!";
    }

    public void Less()
    {
        if(maxPoints > 1)
        {
            maxPoints--;
            PlayerPrefs.SetInt("MaxPoints", maxPoints);
            pointsText.text = maxPoints + "";
        }
    }

    public void More()
    {
        if (maxPoints < 9)
        {
            maxPoints++;
            PlayerPrefs.SetInt("MaxPoints", maxPoints);
            pointsText.text = maxPoints + "";
        }
    }

    public void Quit()
    {
        Debug.Log("Quiting...");
        Application.Quit();
    }

    public void Back()
    {
        if(page == 2)
        {
            MMToggle();
            return;
        }
        if(page == 3)
        {
            MPToggle();
        }
    }

    public void Begin()
    {
        PlayerPrefs.SetInt("P1Score", 0);
        PlayerPrefs.SetInt("P2Score", 0);
        PlayerPrefs.SetInt("P3Score", 0);
        PlayerPrefs.SetInt("P4Score", 0);
        SceneManager.LoadScene(levelToLoad);
    }
}
