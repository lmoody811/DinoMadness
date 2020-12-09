using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerAttack : MonoBehaviour
{

    private WeaponManager weapon_Manager;

    public float fireRate = 15f;

    private float nextTimeToFire;
    public float damage = 20f;

    private Animator zoomCameraAnim;

    private bool zoomed;

    private Camera mainCam;

    private GameObject crosshair;

    private bool is_Aiming;

    public AudioSource button_Sound;

    private bool button_Hit = false;






    void Awake()
    {
        weapon_Manager = GetComponent<WeaponManager>();

        Transform root = transform.Find("LookRoot");
        zoomCameraAnim = root.GetComponentInChildren<Animator>();

        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);

        mainCam = Camera.main;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WeaponShoot();
        ZoomInAndOut();
    }

    void WeaponShoot()
    {
        if (weapon_Manager.GetCurrentSelectedWeapon().fireType == WeaponFireType.MULTIPLE)
        {
            if(Input.GetMouseButton(0) && Time.time > nextTimeToFire)
            {
                BulletFired();
                nextTimeToFire = Time.time + 1f / fireRate;

                weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                if (weapon_Manager.GetCurrentSelectedWeapon().tag == Tags.AXE_TAG) {
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                }

                if(weapon_Manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.BULLET)
                {
                    BulletFired();
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();

                }
                
            }
        }
    }

    void ZoomInAndOut()
    {
        if (weapon_Manager.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.AIM)
        {
            if(Input.GetMouseButtonDown(1))
            {
                zoomCameraAnim.Play(AnimationTags.ZOOM_IN_ANIM);

                crosshair.SetActive(false);
            }
            if(Input.GetMouseButtonUp(1))
            {
                zoomCameraAnim.Play(AnimationTags.ZOOM_OUT_ANIM);

                crosshair.SetActive(true);
            }

        }

        if(weapon_Manager.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.SELF_AIM)
        {
            if(Input.GetMouseButtonDown(1))
            {
                weapon_Manager.GetCurrentSelectedWeapon().Aim(true);
                is_Aiming = true;

            }
            if (Input.GetMouseButtonUp(1))
            {
                weapon_Manager.GetCurrentSelectedWeapon().Aim(false);
                is_Aiming = false;

            }
        }
    }

    void BulletFired()
    {
        RaycastHit hit;



        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit))
        {
            if (hit.transform.tag == Tags.ENEMY_TAG)
            {
                hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);

            }

            if(hit.transform.tag == Tags.BUTTON_TAG && button_Hit == false)
            {
                button_Sound.Play();
                Invoke("GoToNextLevel", 2.0f);
                button_Hit = true;
            }
        }
    }

    void GoToNextLevel()
    {
        if(Dinosaur.level == 1)
        {
            SceneManager.LoadScene("Level2");
        }
        else if(Dinosaur.level == 2)
        {
            //SceneManager.LoadScene("Level3");
        }
        else if(Dinosaur.level == 3)
        {
            //Player won game
        }
    }
}
