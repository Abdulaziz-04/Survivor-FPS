using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum enemyStates{
    PATROL,
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour
{
    //basic required componenets
    private EnemyAnimations enemy_animations;
    private NavMeshAgent enemy_navmesh;
    private Transform target;
    private EnemySounds enemy_audio;
    [SerializeField] GameObject attack_point;


    private enemyStates enemy_state;
    //properties..

    //delay timers 
    private float patrol_timer;
    private float attack_timer;
    private float attack_delay=2f;
    public float patrol_duration = 15f;
    


    private float chase_distance;
    //constant chasing range
    public float chase_gap= 7f;
    public float walk_speed = 0.5f;
    public float run_speed = 4f;
    //attack range
    public float attack_distance = 1.8f;
    public Vector2 patrol_radius = new Vector2(20f,60f);
    private void Awake()
    {
        //get all required components
        enemy_animations = GetComponent<EnemyAnimations>();
        enemy_navmesh = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("PLAYER_TAG").transform;
        enemy_audio = GetComponent<EnemySounds>();
    }
    private void Start()
    {
        //initial state is patrolling
        enemy_state = enemyStates.PATROL;
        // setting the initial values
        patrol_timer = patrol_duration;
        attack_timer = attack_delay;
        chase_distance= chase_gap;
    }
    private void Update()
    {
        //act as per the state
        if (enemy_state == enemyStates.PATROL)
        {
            Patrol();
        }
        if (enemy_state == enemyStates.CHASE)
        {
            Chase();
        }
        if(enemy_state==enemyStates.ATTACK)
        {

            Attack();
        }
    }
    void Patrol()
    {
        
        //navmesh must be handled for all the motions
        enemy_navmesh.isStopped = false;
        //if patrolling speed is walk_speed
        enemy_navmesh.speed = walk_speed;
        //if patrol timer exceeds current duration spawn at new place
        patrol_timer += Time.deltaTime;
        if (patrol_timer > patrol_duration)
        {
            setRandomLocation();
            //reset the timer
            patrol_timer= 0f;
        }
        //set Animations
        if (enemy_navmesh.velocity.sqrMagnitude > 0)
        {
            enemy_animations.Walk(true);
        }
        else
        {
            enemy_animations.Walk(false);
        }
        if (Vector3.Distance(transform.position, target.position)<=chase_gap)
        {
    
            //set animation and state
            enemy_animations.Walk(false);
            enemy_state = enemyStates.CHASE;
            enemy_audio.playScream();

        }
    }
    void setRandomLocation()
    {
        //set the radius and direction
        float random_radius = Random.Range(patrol_radius.x, patrol_radius.y);
        Vector3 random_direction = Random.insideUnitSphere * random_radius;
        random_direction += transform.position;
        NavMeshHit navhit;
        //get the next position within range
        //-1 means select all layers
        NavMesh.SamplePosition(random_direction, out navhit, random_radius, -1);
        enemy_navmesh.SetDestination(navhit.position);
    }
    void Chase()
    {
        //set the navmesh and speed
        enemy_navmesh.isStopped = false;
        enemy_navmesh.speed = run_speed;
        //set the destination to player itself
        enemy_navmesh.SetDestination(target.position);
        //set Animations
        if (enemy_navmesh.velocity.sqrMagnitude > 0)
        {
            enemy_animations.Run(true);
        }
        else
        {
            enemy_animations.Run(false);
        }
        //if in range, change to attack state
        if (Vector3.Distance(transform.position, target.position) <= attack_distance)
        {
            enemy_animations.Walk(false);
            enemy_animations.Run(false);
            enemy_state = enemyStates.ATTACK;
            //if ranged attack occurs, chase distance value changes so
            //we need to set it back to normal
            if (chase_distance !=chase_gap)
            {
                chase_distance = chase_gap;
            }

        }
        //if player runs away , get back to patrol mode
        if (Vector3.Distance(transform.position, target.position) > chase_gap)
        {
            enemy_animations.Run(false);
            enemy_animations.Walk(true);
            patrol_timer = patrol_duration;
            enemy_state = enemyStates.PATROL;

        }
        

        
        
 

    }
    void Attack()
    {
        //set the navmesh
        enemy_navmesh.velocity = Vector3.zero;
        enemy_navmesh.isStopped = true;
        attack_timer += Time.deltaTime;
        if (attack_timer >= attack_delay)
        {
            enemy_animations.Attack();
            enemy_audio.playAttack();
            attack_timer = 0f;
        }
        //if out of attack range ,start chasing
        if (Vector3.Distance(transform.position, target.position) > attack_distance)
        {
            enemy_state = enemyStates.CHASE;
        }

    }

    //get and set the enemy states (shorthand method)
    public enemyStates EnemyState { get; set; }
    //Animation Events
    public void attPtON()
    {
        attack_point.SetActive(true);
    }
    public void attPtOFF()
    {
        if (attack_point.activeInHierarchy)
        {
            attack_point.SetActive(false);
        }
    }

}
