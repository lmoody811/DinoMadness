using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour
{
    private EnemyAnimator enemy_Anim;
    private NavMeshAgent navAgent;

    private EnemyState enemy_State;

    public float walk_Speed = 0.5f;

    public float run_Speed = 4f;

    public float chase_Distance = 7f;

    private float current_Chase_Distance;

    public float attack_Distance = 1.8f;

    public float chase_After_Attack_Distance = 2f;

    public float patrol_Radius_Min = 20f, patrol_Radius_Max = 60f;

    public float patrol_For_This_Time = 15f;

    private float patrol_Timer;

    public float wait_Before_Attack = 2f;

    private float attack_Timer;

    private Transform target;

    public GameObject attack_Point;

    public float damage = 10f;

    private EnemyAudio enemy_Audio;

    public Camera FPCamera;
    // Start is called before the first frame update

    void Awake()
    {

        enemy_Anim = GetComponent<EnemyAnimator>();
        navAgent = GetComponent<NavMeshAgent>();

        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;

        enemy_Audio = GetComponentInChildren<EnemyAudio>();
    }
    void Start()
    {
        enemy_State = EnemyState.PATROL;
        patrol_Timer = patrol_For_This_Time;

        //when the enemy first gets to the player, attack it right away
        attack_Timer = wait_Before_Attack;

        //memorize the value of chase distance so that we can put it back
        current_Chase_Distance = chase_Distance;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy_State == EnemyState.PATROL)
        {
            Patrol();
        }
        if (enemy_State == EnemyState.CHASE)
        {
            Chase();
        }
        if (enemy_State == EnemyState.ATTACK)
        {
            Attack();
        }
    }

    void Patrol()
    {
        navAgent.isStopped = false;
        navAgent.speed = walk_Speed;

        //add to patrol timer
        patrol_Timer += Time.deltaTime;

        if(patrol_Timer > patrol_For_This_Time)
        {
            SetNewRandomDestination();

            patrol_Timer = 0f;

        }

        if(navAgent.velocity.sqrMagnitude > 0)
        {
            enemy_Anim.Walk(true);
        }
        else
        {
            enemy_Anim.Walk(false);
        }

        if(Vector3.Distance(transform.position, target.position) <= chase_Distance)
        {

            enemy_Anim.Walk(false);
            enemy_State = EnemyState.CHASE;

            enemy_Audio.Play_ScreamSound();
        }
    }

    void Chase()
    {
        //enable agent to move again
        navAgent.isStopped = false;

        navAgent.speed = run_Speed;

        //set the player's position as the destination because we are chasing the player
        navAgent.SetDestination(target.position);

        //print("Dino thinks I'm here: " + target.position);



        if (navAgent.velocity.sqrMagnitude > 0)
        {
            //transform.LookAt(FPCamera.transform);
            enemy_Anim.Run(true);
        }
        else
        {
            enemy_Anim.Run(false);

        }

        if(Vector3.Distance(transform.position, target.position) <= attack_Distance)
        {
            enemy_Anim.Run(false);
            enemy_Anim.Walk(false);
            enemy_State = EnemyState.ATTACK;

            if(chase_Distance != current_Chase_Distance)
            {
                chase_Distance = current_Chase_Distance;
            }
            else if(Vector3.Distance(transform.position, target.position) > chase_Distance)
            {
                //player run away from enemy, stop running
                enemy_Anim.Run(false);
                enemy_Anim.Walk(false);

                enemy_State = EnemyState.PATROL;

                //reset patrol timer so that function can calculate new patrol destination right away
                patrol_Timer = patrol_For_This_Time;

                if (chase_Distance != current_Chase_Distance)
                {
                    chase_Distance = current_Chase_Distance;
                }
            }
        }

    }

    void Attack()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        attack_Timer += Time.deltaTime;
        //enemy_Audio.Play_AttackSound();

        if(attack_Timer > wait_Before_Attack)
        {
            enemy_Anim.Attack();
            attack_Timer = 0f;

        }

        if(Vector3.Distance(transform.position, target.position) > attack_Distance + chase_After_Attack_Distance)
        {
            enemy_State = EnemyState.CHASE;
        }
    }

    void SetNewRandomDestination()
    {
        /*float rand_Radius = Random.Range(patrol_Radius_Min, patrol_Radius_Max);
        Vector3 randDir = Random.insideUnitSphere * rand_Radius;

        randDir += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDir, out navHit, rand_Radius, -1);

        navAgent.SetDestination(navHit.position);*/

    }

    public EnemyState Enemy_State
    {
        get
        {
            return enemy_State;
        }
        set
        {
            enemy_State = value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            //enemy_Audio.Play_AttackSound();
            enemy_Anim.Attack();

            other.GetComponent<HealthScript>().ApplyDamage(damage);
        }
    }
}
