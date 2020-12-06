using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    

    public float radius = 1f;

    public LayerMask layerMask;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);

       
        if (hits.Length > 0)
        {
            hits[0].gameObject.GetComponent<HealthScript>().ApplyDamage(damage);

            print("Enemy is applying damage");

            gameObject.SetActive(false);
        }*/
    }

    void Turn_On_AttackPoint()
    {
        gameObject.SetActive(true);
    }

    void Turn_Off_AttackPoint()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }
}
