﻿using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        dino_statue.SetActive(false);
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
        dino_Name.text = "";
        updateDinoText();
    }

    // Update is called once per frame
    void Update()
    {
        if(collected_Dinos == num_Dinos) { 
            //go to next level
            //start timer
        }
    }


    void updateDinoText() { 
        dinos_Text.text = collected_Dinos + "/" + num_Dinos + " Dinosaurs";
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "Player")
        {
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