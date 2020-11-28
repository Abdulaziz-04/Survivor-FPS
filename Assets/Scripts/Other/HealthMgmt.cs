using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class HealthMgmt : MonoBehaviour
{
    private EnemyController enemy;
    private NavMeshAgent enemy_navmesh;
    private EnemyAnimations enemy_anim;
    private EnemySounds enemy_audio;
    private bool is_dead;
    private PlayerStats player_stats;
    public float health;
    public bool is_boar, is_cannibal, is_player;
        private void Awake()
        {
            if (is_boar || is_cannibal)
            {
                enemy = GetComponent<EnemyController>();
                enemy_navmesh = GetComponent<NavMeshAgent>();
                enemy_anim = GetComponent<EnemyAnimations>();
                enemy_audio = GetComponent<EnemySounds>();
        }
            if (is_player)
            {
                player_stats = GetComponent<PlayerStats>();
            }
        }
    

    public void dealDamage(float damage)
    {
        if (is_dead)
        {
            return;
        }
        health -= damage;
        if (is_player)
        {
            player_stats.displayHealth(health/10f);

        }
        if (is_boar || is_cannibal)
        {
            //if patrolling start chasing
            if (enemy.EnemyState== enemyStates.PATROL)
            {
                enemy.chase_gap = 1000f;
            }
        }
        if (health < 0f)
        {
            characterDead();
            is_dead = true;

        }

    }
    
    void characterDead()
    {
        if (is_boar)
        {
            //deactivate components
            enemy_navmesh.isStopped = true;
            enemy_navmesh.velocity = Vector3.zero;
            enemy.enabled = false;
            enemy_anim.dead();
            enemy_audio.playDead();
            StartCoroutine(removeCharacter());
            Spawner.instance.Respawn(false);
        }
        else if (is_cannibal)
        {
            //deactivate components
            GetComponent<Animator>().enabled = false;
            GetComponent<Rigidbody>().AddTorque(-transform.forward *5f);
            enemy_audio.playDead();
            GetComponent<BoxCollider>().isTrigger = false;
            enemy.enabled = false;
            enemy_navmesh.enabled = false;
            enemy_anim.enabled = false;
            StartCoroutine(removeCharacter());
            Spawner.instance.Respawn(true);
        }
        else if(is_player){
            //play the dead animation for player or transform the camera angle
        }

    }
    IEnumerator removeCharacter()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
