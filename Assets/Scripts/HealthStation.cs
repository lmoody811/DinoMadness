using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStation : MonoBehaviour
{

    private bool addedHealth = false;
    public float amountToHeal = 15f;
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "Player")
        {
            addedHealth = other.gameObject.GetComponent<HealthScript>().addHealth(amountToHeal);
            if(addedHealth == true)
            {
                gameObject.SetActive(false); 
                //doing this so that player can only use health station once
            }

        }
    }
}
