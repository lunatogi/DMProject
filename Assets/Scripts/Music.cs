using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private GameObject pnl_Home;
    public bool playMusic = true;
    // Start is called before the first frame update
    void Start()
    {

        if (playMusic)
            GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            pnl_Home = GameObject.FindGameObjectWithTag("Start");
            if (pnl_Home.activeSelf)
            {
                if (!playMusic)
                {
                    Camera.main.SendMessage("CloseMusicTick", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        catch
        {

        }
    }

    public void PauseMusic()
    {
        playMusic = false;
        GetComponent<AudioSource>().Pause();
    }

    public void ContinueMusic()
    {
        playMusic = true;
        GetComponent<AudioSource>().Play();
    }
}
