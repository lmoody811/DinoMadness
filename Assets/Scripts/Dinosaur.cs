using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dinosaur : MonoBehaviour
{
    public TextMeshProUGUI dinos_Text;
    private int collected_Dinos;
    private int num_Dinos;

    // Start is called before the first frame update
    void Start()
    {
        string level_Name = SceneManager.GetActiveScene().name;

        switch(level_Name){ 
            case "Level1": 
                num_Dinos = 6;
                break;
            case "Level2": 
                num_Dinos = 6;
                break;
            case "Level3": 
                num_Dinos = 10;
                break;
        }
        collected_Dinos = 0;
        updateDinoText();
    }

    // Update is called once per frame
    void Update()
    {
        if(collected_Dinos == num_Dinos) { 
            //go to next level
        }
    }


    void updateDinoText() { 
        dinos_Text.text = collected_Dinos + "/" + num_Dinos + " Dinosaurs";
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "Player")
        {
            collected_Dinos++;
            updateDinoText();
            gameObject.SetActive(false);
        }
    }

    
}
