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

    public TextMeshProUGUI win_Text;






    void Awake()
    {
        win_Text.text = "";
        weapon_Manager = GetComponent<WeaponManager>();

        Transform root = transform.Find("LookRoot");
        zoomCameraAnim = root.GetComponentInChildren<Animator>();

        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);

        mainCam = Camera.main;

    }
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("totalCollected")) { 
            PlayerPrefs.SetInt("totalCollected", 0);
        }
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
                print("Collected:" + Dinosaur.collected_Dinos);
                int total_Collected_Dinos = PlayerPrefs.GetInt("totalCollected") + Dinosaur.collected_Dinos;
                PlayerPrefs.SetInt("totalCollected", total_Collected_Dinos);
                button_Hit = true;
                if(Dinosaur.level == 3) { 
                    playerWon();
                }
                else { 
                    Invoke("GoToNextLevel", 2.0f);
                }
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
            SceneManager.LoadScene("Level3");
        }
        else if(Dinosaur.level == 3)
        {
            //Player won game
        }
    }

    void playerWon() { 
        StartCoroutine(ShowWinningMessage(7));
    }

    IEnumerator ShowWinningMessage(float delay) { 
        win_Text.text = getScore();
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainMenu");
    }

    string getScore() { 
        int total_Collected_Dinos = PlayerPrefs.GetInt("totalCollected");
        print(total_Collected_Dinos);
        string winningText = "";

        if(total_Collected_Dinos <= 18 && total_Collected_Dinos >= 16) { 
            winningText = "Congrats! You collected " + total_Collected_Dinos + " dinosaurs. You made it out by the skin of your teeth. Hopefully you survive your injuries.";
        }
        else if(total_Collected_Dinos == 22) { 
            winningText = "Congrats! You collected " + total_Collected_Dinos + " dinosaurs. You made it out with all of the dinosaur DNA! Good luck selling these and making a fortune.";
        }
        else if(total_Collected_Dinos <= 21 && total_Collected_Dinos >= 19) { 
            winningText = "Congrats! You collected " + total_Collected_Dinos + " dinosaurs. You survived the island. Stand by for your next mission.";
        }
        else if(total_Collected_Dinos <= 15) { 
            winningText = "You collected " + total_Collected_Dinos + " dinosaurs. You did not collect enough DNA. You are stuck on the island forever. Good luck with that.";
        }

        return winningText;
    }
}
