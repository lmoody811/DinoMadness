using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class HealthScript : MonoBehaviour
{
    private EnemyAnimator enemy_Anim;
    private NavMeshAgent navAgent;
    private EnemyController enemy_Controller;

    public float health = 100f;
    public float initial_health = 100f;

    public bool is_Player, is_Dragon;

    private bool is_Dead;

    private bool unlimitedHealth = false;
    private static bool maxDamage = false;
    public float damage = 10f;

    private PlayerStats player_Stats;

    public AudioSource die_Sound;
    public AudioSource health_Sound;

    public TextMeshProUGUI die_Text;


    void Awake()
    {
        if(gameObject.name == "Player") { 
            die_Text.text = "";
        }

        if (is_Dragon)
        {
            enemy_Anim = GetComponent<EnemyAnimator>();
            enemy_Controller = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();

            //get enemy audio
        }

        if (is_Player)
        {
            player_Stats = GetComponent<PlayerStats>();
        }
    }


    string getLevelText()
    {
        int curLevel = Dinosaur.level;
        string levelText = "";

        switch (curLevel)
        {
            case 1:
                levelText = "Level 1: Triassic Period";
                break;
            case 2:
                levelText = "Level 2: Jurassic Period";
                break;
            case 3:
                levelText = "Level 3: Cretaceous Period";
                break;
        }
        return levelText;

    }

    public void ApplyDamage(float damage)
    {
        if (is_Dead)
        {
            return;
        }


        die_Sound.Play();

        if (!unlimitedHealth)
            health -= damage;

        if (is_Dragon && maxDamage)
            health = 0;

        if (is_Player)
        {
            player_Stats.Display_HealthStats(health);
        }
        if (is_Dragon)
        {
            if (enemy_Controller.Enemy_State == EnemyState.PATROL)
            {
                enemy_Controller.chase_Distance = 50f;
            }
        }

        if (health <= 0f)
        {
            PlayerDied();

            is_Dead = true;
        }
    }

    void PlayerDied()
    {
        if (is_Dragon)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            //GetComponent<Rigidbody>().AddTorque(-transform.forward * 50f);

            GetComponent<EnemyController>().Die();

            enemy_Controller.enabled = false;
            navAgent.enabled = false;
            enemy_Anim.enabled = false;

            //Start Coroutine

            EnemyManager.instance.EnemyDied(true);
        }

        if (is_Player)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }

            EnemyManager.instance.StopSpawning();

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);

        }

        if (tag == Tags.PLAYER_TAG)
        {
            die_Text.text = "You died.";
            Invoke("RestartGame", 3f);
        }
        else
        {
            Invoke("TurnOffGameObject", 3f);
        }
    }


    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }

    public bool addHealth(float heal)
    {
        bool playerHealed = false;
        if (health == 100f)
        {
            //don't do anything
        }
        else
        {
            health_Sound.Play();
            if ((health + heal) >= initial_health)
            {
                health = 100f;
                playerHealed = true;
            }
            else
            {
                health += heal;
                playerHealed = true;
            }
            player_Stats.Display_HealthStats(health);
        }
        return playerHealed;

    }

    void Update()
    {
        ApplyCheats();
    }

    void ApplyCheats()
    {
        MaxDamage();
        UnlimitedHealth();
    }

    void MaxDamage()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5) && is_Player)
        {
            maxDamage = !maxDamage;
        }
    }

    void UnlimitedHealth()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6) && is_Player)
        {
            health = initial_health;
            player_Stats?.Display_HealthStats(health);
            unlimitedHealth = !unlimitedHealth;
        }
    }
}
