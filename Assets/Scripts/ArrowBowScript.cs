using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBowScript : MonoBehaviour
{
    private Rigidbody myBody;

    public float speed = 30f;

    public float deactivate_Timer = 3f;

    public float damage = 15f;

    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeactivateGameObject", deactivate_Timer);
    }

    /*void DeactivateGameObject()
    {
        if(DeactivateGameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }*/

    public void Launch(Camera mainCamera)
    {
        myBody.velocity = Camera.main.transform.forward * speed;

        transform.LookAt(transform.position + myBody.velocity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider target)
    {

    }
}
