using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI goal;
    public TextMeshProUGUI developers;
    public GameObject startBTN;
    public GameObject backBTN;
    public GameObject goalBTN;
    public GameObject quitBTN;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        showMenu();
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

    public void showInstructions()
    {
        backBTN.SetActive(true);
        goal.enabled = true;

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
        goal.enabled = false;
    }
}
