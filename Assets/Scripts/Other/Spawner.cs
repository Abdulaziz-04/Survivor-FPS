using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    [SerializeField] Transform[] cannibal_spawnPoints,boar_spawnPoints;
    [SerializeField] GameObject boar_prefab, cannibal_prefab;
    private int cannibal_count, boar_count;
    //keeping constants for future references
    private int cannibal_init=6, boar_init=3;
    public float spawn_delay = 20f;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        cannibal_count = cannibal_init;
        boar_count = boar_init;
        SpawnEnemies();
        StartCoroutine(checktoSpawn());
    }
    void SpawnEnemies()
    {
        SpawnCannibals();
        SpawnBoars();
        

    }
    void SpawnCannibals()
    {
        //spawn and reduce count to zero
           for(int i = 0; i < cannibal_count; i++)
            {
                Instantiate(cannibal_prefab, cannibal_spawnPoints[i].position, Quaternion.identity);

            }
            cannibal_count = 0;

    }
    void SpawnBoars()
    {
        

            for (int i = 0; i < boar_count; i++)
            {
                Instantiate(boar_prefab, boar_spawnPoints[i].position, Quaternion.identity);

            }
            boar_count = 0;

    }
    public void Respawn(bool is_cannibal)
    {
        //respawn and maintain count limit
        if (is_cannibal)
        {
            cannibal_count++;
            if (cannibal_count > cannibal_init)
            {
                cannibal_count = cannibal_init;
            }
        }
        else
        {
            boar_count++;
            if (boar_count > boar_init)
            {
                boar_count = boar_init;
            }
        }

    }
     IEnumerator checktoSpawn()
     {
        //recursive routine to keep track of characters
        yield return new WaitForSeconds(spawn_delay);
        SpawnCannibals();
        SpawnBoars();
        StartCoroutine(checktoSpawn());

     }
    public void stopSpawning()
    {
        StopCoroutine(checktoSpawn());

    }

}
