using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveSpawner : MonoBehaviour
{
    
    public enum SpawnState { SPAWNING, WAITING, COUNTING};

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] wave;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float SearchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    void start()
    {
        waveCountdown = timeBetweenWaves;
    }


    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                //BEGAN NEW ROUNDNDNNN
                Debug.Log("WAVE DONE");
                return;
            }
            else
            {
                return;
            }

        }


        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine( SpawnWave ( wave[nextWave] ) );
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }

        bool EnemyIsAlive()
        {
            SearchCountdown -= Time.deltaTime;
            if (SearchCountdown <= 0f)
            {
                SearchCountdown = 1f;
                if (GameObject.FindGameObjectWithTag("Enemy") == null)
                {
                return false;
                }
            }
            return true;
        }
        
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log ("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds( 1f/_wave.rate );
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {   
        Debug.Log("Spawning Enemy: " + _enemy.name);
        Instantiate (_enemy, transform.position, transform.rotation); 
    }

}
