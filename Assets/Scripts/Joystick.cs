using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Joystick : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject analog;
    public GameObject player;
    public GameObject playerPrefab;
    
    public Transform playerTransAfterAd;

    public GameObject pnl_End;

    public Text txt_Time;
    public Text txt_End_Score;
    public Text txt_End_HighScore;

    private Vector2 startPos;
    private Vector2 currentPos;
    private Vector2 newPos;

    public Transform playerStartPos;

    Rigidbody2D rb;

    private Transform rot;

    public float distance;
    public int radius;

    public bool moveJoystick = false;

    float x;
    float y;
    float z;
    float angle;
    float roty;

    float dashTimer = 0.05f;
    public bool dashBool = false;
    public float dash_Cooldown = 1f;

    public float score_Timer = 0;
    public float highScore;

    public bool gameOn = true;

    public int score = 0;
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highscore", 0);
        startPos = analog.transform.position;
        rot = player.transform;
        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!gameOn)
        {
            return;
        }

        if (gameOn)
        {
            if(score_Timer >= 0.11f)
            {
                score += 1;
                score_Timer = 0;
            }
            score_Timer += Time.deltaTime;
            txt_Time.text = score.ToString();
        }

        if (dash_Cooldown > 0)
            dash_Cooldown -= Time.deltaTime;

        if (dashBool)
        {
            dashTimer -= Time.deltaTime;
            rb.AddForce(player.transform.up * 300, ForceMode2D.Force);

        }

        if (dashTimer <= 0)
        {
            
            dashTimer = 0.03f;           //CAUTION IF dash timer changes, you have to change this one too
            dash_Cooldown = 2f;               //CAUTION If dash cooldown changes, you have to change this one too
            dashBool = false;
        }



#if MOBILE_INPUT                        //Codes for mobile gameplay
        Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));     //Lets the player control the plane only if touch position is from below
        float bottomY = stageDimensions.y;
        float topY = stageDimensions.y + Screen.height;
        float upGap = topY - Input.touches[0].position.y;
        float downGap = Input.touches[0].position.y - bottomY;
        if (Input.touches[0].phase == TouchPhase.Began && downGap <= upGap/2)
        {
            JoystickTriggered(false);
            startPos = Input.touches[0].position;
        }else if (Input.touches[0].phase == TouchPhase.Ended)
        {
            JoystickTriggered(true);
        }

        if (moveJoystick)
        {

            rb.velocity = player.transform.up * 3;                 //Moves the player

            currentPos = Input.touches[0].position;
            distance = Vector3.Distance(startPos, currentPos);

            x = currentPos.x - startPos.x;                          //Calculates the tangent, in order to turn the player                  
            y = currentPos.y - startPos.y;                      
            z = y / x;

            if (x < 0 && y > 0 || x < 0 && y < 0)
            {                                                   
                angle = Mathf.Atan(z);                          
                roty = angle * Mathf.Rad2Deg + 90;                  //Transforms the tangent to needed degree
                rot.rotation = Quaternion.Euler(0, 0, roty);        //Turns the player to that angle 
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rot.rotation, Time.deltaTime * 100);
            }
            else
            {
                angle = Mathf.Atan(z);
                roty = angle * Mathf.Rad2Deg - 90;                  //Transforms the tangent to needed degree
                rot.rotation = Quaternion.Euler(0, 0, roty);        //Turns the player to that angle 
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rot.rotation, Time.deltaTime * 100);
            }

        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
#endif

#if !MOBILE_INPUT                       //Codes for desktop gameplay


        Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));     //Lets the player control the plane only if touch position is from below
        float bottomY = stageDimensions.y;
        float topY = stageDimensions.y + Screen.height;
        float upGap = topY - Input.mousePosition.y;
        float downGap = Input.mousePosition.y - bottomY;
        if (Input.GetMouseButtonDown(0) && downGap <= upGap/2)
        {
            JoystickTriggered(false);
            startPos = Input.mousePosition;
        }else if (Input.GetMouseButtonUp(0))
        {
            JoystickTriggered(true);
        }

        if (moveJoystick)
        {

            rb.velocity = player.transform.up * 3;                 //Moves the player

            currentPos = Input.mousePosition;
            distance = Vector3.Distance(startPos, currentPos);

            x = currentPos.x - startPos.x;                          //Calculates the tangent, in order to turn the player                  
            y = currentPos.y - startPos.y;                      
            z = y / x;

            if (x < 0 && y > 0 || x < 0 && y < 0)
            {                                                   
                angle = Mathf.Atan(z);                          
                roty = angle * Mathf.Rad2Deg + 90;                  //Transforms the tangent to needed degree
                rot.rotation = Quaternion.Euler(0, 0, roty);        //Turns the player to that angle 
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rot.rotation, Time.deltaTime * 100);
            }
            else
            {
                angle = Mathf.Atan(z);
                roty = angle * Mathf.Rad2Deg - 90;                  //Transforms the tangent to needed degree
                rot.rotation = Quaternion.Euler(0, 0, roty);        //Turns the player to that angle 
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rot.rotation, Time.deltaTime * 100);
            }

        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
#endif
    }

    public void JoystickTriggered(bool triggered)
    {
        if (triggered)
        {              //PointerUp

            moveJoystick = false;
            analog.transform.position = startPos;
        }
        else                        //PointerDown
        {
            moveJoystick = true;
        }
            
    }

    public void Dash()
    {
        if (dash_Cooldown <= 0)
        {
            dashBool = true;
        }
    }

    public void EndGame()
    {
        Camera.main.SendMessage("GameBreak", SendMessageOptions.DontRequireReceiver);
        Camera.main.SendMessage("PauseGame", true, SendMessageOptions.DontRequireReceiver);
        int earned_golds = score / 100;
        int money = PlayerPrefs.GetInt("Money", 0);
        money += earned_golds;
        PlayerPrefs.SetInt("Money", money);

        if (score > highScore)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
        gameOn = false;
        txt_End_HighScore.text = "HIGH SCORE \n " + PlayerPrefs.GetInt("highscore");
        txt_End_Score.text = "YOUR SCORE \n " + score.ToString();
        pnl_End.SetActive(true);
    }

    public void Restart(bool watched)
    {

        gameOn = true;
        GameObject newPlayer;
        newPlayer = Instantiate(playerPrefab, playerStartPos.position, playerStartPos.rotation) as GameObject;   //Creates new plane and sets all needed variables
        if (!watched)
        {
            //REKLAMLA DEVAM ETME BURADAN OLACAK REKLAM
            
            GameObject[] currEnemies = GameObject.FindGameObjectsWithTag("Enemy");                                  //Destroys all left bullets
            foreach (GameObject enemy in currEnemies)
            {
                Destroy(enemy);
            }
            score = 0;
        }
        else
        {
            /*
            GameObject[] currEnemies = GameObject.FindGameObjectsWithTag("Enemy");                                  //Destroys all left bullets
            foreach (GameObject enemy in currEnemies)
            {
                enemy.SendMessage("Continue", SendMessageOptions.DontRequireReceiver);
            }
            newPlayer = Instantiate(playerPrefab, playerTransAfterAd.position, playerTransAfterAd.rotation) as GameObject;   //Creates new plane and sets all needed variables*/
            //Camera.main.SendMessage("ShowRewarded", SendMessageOptions.DontRequireReceiver);
            
        }
        player = newPlayer;
        rb = player.GetComponent<Rigidbody2D>();
        rot = player.transform;
        newPlayer.SendMessage("StartGamePlay", SendMessageOptions.DontRequireReceiver);             //Sends message to others codes in order to play again
        Spawner.Restart();
        Enemy.Restart();
        pnl_End.SetActive(false);
        
    }

    public void CallRewardedAd()
    {
        Camera.main.SendMessage("BringRewardedAd", SendMessageOptions.DontRequireReceiver);
    }

    public void RestartWithReward()
    {
        gameOn = true;
        GameObject newPlayer;
        GameObject[] currEnemies = GameObject.FindGameObjectsWithTag("Enemy");                                  //Destroys all left bullets
        foreach (GameObject enemy in currEnemies)
        {
            enemy.SendMessage("Continue", SendMessageOptions.DontRequireReceiver);
        }
        newPlayer = Instantiate(playerPrefab, playerTransAfterAd.position, playerTransAfterAd.rotation) as GameObject;   //Creates new plane and sets all needed variables
        player = newPlayer;
        rb = player.GetComponent<Rigidbody2D>();
        rot = player.transform;
        newPlayer.SendMessage("StartGamePlay", SendMessageOptions.DontRequireReceiver);             //Sends message to others codes in order to play again
        Spawner.Restart();
        Enemy.Restart();
        pnl_End.SetActive(false);
    }

    public void ChangePlane(GameObject plane)
    {
        Destroy(player);
        playerPrefab = plane;
        GameObject currPlayer = GameObject.FindGameObjectWithTag("Player");
        GameObject newPlayer = Instantiate(playerPrefab, playerStartPos.position, playerStartPos.rotation) as GameObject;   //Creates new plane and sets all needed variables
        player = newPlayer;
        rb = player.GetComponent<Rigidbody2D>();
        rot = player.transform;
        player.SendMessage("ChangePlane", SendMessageOptions.DontRequireReceiver);
    }

    public void StartGameJoy()
    {
        gameOn = true;
    }

    public void Pause()
    {
        gameOn = false;
    }
}
