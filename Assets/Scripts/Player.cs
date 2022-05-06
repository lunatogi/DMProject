using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject soundController;

    public bool gameOn = false;
    public bool paused = false;
    Rigidbody2D rb;

    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = transform.gameObject.GetComponent<Rigidbody2D>();
        soundController = GameObject.FindGameObjectWithTag("Sound");
        ChangeSpeed();
    }

    // Update is called once per frame
    void Update()
    {


        if (!gameOn)
            return;


        if (gameOn && !paused)
        {
            rb.velocity = transform.up * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            soundController.SendMessage("PlayDeath", SendMessageOptions.DontRequireReceiver);
            GameObject[] currEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject enemy in currEnemies)
            {
                enemy.SendMessage("Paused", SendMessageOptions.DontRequireReceiver);
            }
            GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            mainCamera.SendMessage("EndGame", SendMessageOptions.DontRequireReceiver);
            Destroy(transform.gameObject);
        }
    }

    public void StartGamePlay()
    {
        gameOn = true;
    }

    public void Continue()
    {
        paused = false;
    }

    public void Pause()
    {
        rb.velocity = new Vector2(0, 0);
        paused = true;
    }

    public void ChangeSpeed()           //Changes speed according to the plane that is using right now
    {
        int num = Market.p_Index;
        switch (num)
        {
            case 0:
                speed = 3;
                break;
            case 1:
                speed = 3;
                break;
            case 2:
                speed = 3;
                break;
            case 3:
                speed = 3;
                break;
            case 4:
                speed = 4;
                break;
            case 5:
                speed = 4;
                break;
            case 6:
                speed = 4;
                break;
            case 7:
                speed = 4;
                break;
            case 8:
                speed = 5;
                break;
            case 9:
                speed = 5;
                break;
            case 10:
                speed = 5;
                break;
            case 11:
                speed = 6;
                break;
        }
    }
}
