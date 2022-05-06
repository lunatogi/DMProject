using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    public static float speed = 3;
    // Start is called before the first frame update
    void Start()
    {
        rb = transform.gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(speed);
    }

    public static void IncreaseSpeed()
    {
        speed += 0.5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player")
        {
            Destroy(transform.gameObject);
        }
    }

    public static void Restart()
    {
        speed = 3;
    }

    public void Pause()
    {
        rb.velocity = new Vector2(0, 0);
    }

    public void Continue()
    {
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }
}
