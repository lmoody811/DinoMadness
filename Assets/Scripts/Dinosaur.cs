using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dinosaur : MonoBehaviour
{
    public TextMeshProUGUI dinos_Text;
    public TextMeshProUGUI dino_Name;
    static public int collected_Dinos;
    public GameObject dino_statue;
    private int num_Dinos;
    static public int level;
    public AudioSource collected_Sound;
    private bool alreadyCollected = false;

    // Start is called before the first frame update
    void Start()
    {
        dino_statue.SetActive(false);
        string level_Name = SceneManager.GetActiveScene().name;

        switch(level_Name){ 
            case "Level1":
                level = 1;
                num_Dinos = 6;
                break;
            case "Level2":
                level = 2;
                num_Dinos = 6;
                break;
            case "Level3":
                level = 3;
                num_Dinos = 10;
                break;
        }
        collected_Dinos = 0;
        dino_Name.text = "";
        updateDinoText();
    }

    // Update is called once per frame
    void Update()
    {
        if(collected_Dinos == num_Dinos) { 
            PlayerStats.showMessage = true;
        }
    }


    void updateDinoText() { 
        dinos_Text.text = collected_Dinos + "/" + num_Dinos + " Dinosaurs";
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "Player" && alreadyCollected == false)
        {
            alreadyCollected = true;
            collected_Sound.Play();
            dino_statue.SetActive(true);
            collected_Dinos++;
            updateDinoText();
            dino_Name.text = "Collected: " + gameObject.name;
            Invoke("deactivateDino", 3.0f);
        }
    }

    void deactivateDino()
    {
        dino_Name.text = "";
        gameObject.SetActive(false);
    }

    
}
