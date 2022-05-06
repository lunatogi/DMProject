using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] obj_Enemy;

    public Transform[] spawnPoints;

    public float gameTime = 3f;
    public float spawnDelay;
    public float spawnTimer;
    public static int enemyCounter = 1;

    public static bool gameOn = false;

    public static int maxEnemyType = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!gameOn)
            return;

        Debug.Log(enemyCounter);

        if (enemyCounter <= 4)             //Generates 5 enemy 
        {
            gameTime -= Time.deltaTime;

            if (gameTime <= 0)
            {
                NextLevel();
                gameTime = 5f;              //CAUTION Dont forget this line when you change gameTime value
            }
        }

        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0)
        {
            for (int counter = 1; counter <= enemyCounter; counter++)
            {
                int rndm_Spawn = Random.Range(0, 12);
                int rndm_Enemy = Random.Range(0, maxEnemyType);
                //Debug.Log(rndm_Spawn);
                Instantiate(obj_Enemy[rndm_Enemy], spawnPoints[rndm_Spawn].position, spawnPoints[rndm_Spawn].rotation);
            }

            spawnTimer = spawnDelay;
            
        }
    }

    public static int level;
    public void NextLevel()
    {
        level++;
        if(level == 1)
        {
            enemyCounter++;
        }else if(level == 2)
        {
            Enemy.IncreaseSpeed();
            //Debug.Log("Level 2");
        }
        else if (level == 3)
        {
            if (maxEnemyType < 3)
            {
                maxEnemyType++;
            }
            //Debug.Log("Level 3");
        }
        else if (level == 4)
        {
            //Debug.Log("Level 4");
            level = 0;
        }

    }

    public void EndGame()
    {
        gameOn = false;
    }

    public void StartGameSp()
    {
        gameOn = true;
    }

    public static void Restart()
    {
        gameOn = true;
        level = 1;
        maxEnemyType = 1;
        enemyCounter = 1;
    }

    public void Pause()
    {
        gameOn = false;
    }
}
