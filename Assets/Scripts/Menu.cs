using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI[] goals;
    public TextMeshProUGUI developers;
    public GameObject startBTN;
    public GameObject backBTN;
    public GameObject nextBTN;
    public GameObject goalBTN;
    public GameObject quitBTN;

    private int goalPage;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        showMenu();

        PlayerPrefs.SetInt("totalCollected", 0);
    }
    // Start is called before the first frame update
    public void startGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void nextPage()
    {
        goals[goalPage].enabled = false;
        goals[++goalPage].enabled = true;

        if (goalPage == goals.Length - 1)
        {
            nextBTN.SetActive(false);
        }

        GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    public void showInstructions()
    {
        backBTN.SetActive(true);
        goalPage = 0;
        goals[0].enabled = true;

        if (goalPage == goals.Length - 1)
        {
            nextBTN.SetActive(false);
        } else
        {
            nextBTN.SetActive(true);
        }

        goalBTN.SetActive(false);
        title.enabled = false;
        developers.enabled = false;
        startBTN.SetActive(false);
        quitBTN.SetActive(false);
    }

    public void goBack()
    {
        showMenu();
    }

    void showMenu()
    {
        title.enabled = true;
        developers.enabled = true;
        startBTN.SetActive(true);
        quitBTN.SetActive(true);
        goalBTN.SetActive(true);

        backBTN.SetActive(false);
        nextBTN.SetActive(false);

        foreach (var goal in goals)
        {
            goal.enabled = false;
        }
    }
}
