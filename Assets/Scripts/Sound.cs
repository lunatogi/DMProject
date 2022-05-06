using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private GameObject pnl_Home;

    public bool playSounds = true;

    public AudioClip aud_Death;
    public AudioClip aud_Buy;
    // Start is called before the first frame update

    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);

        GameObject[] sounds = GameObject.FindGameObjectsWithTag("Sound");

        if(sounds.Length > 1)
        {
            for(int k = 1; k < sounds.Length; k++)
            {
                Destroy(sounds[k]);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            pnl_Home = GameObject.FindGameObjectWithTag("Start");
            if (pnl_Home.activeSelf)
            {
                if (!playSounds)
                {
                    Camera.main.SendMessage("CloseSoundTick", SendMessageOptions.DontRequireReceiver);
                }

            }
        }
        catch
        {

        }


    }

    public void PlayDeath()
    {
        if (playSounds)
        {
            GetComponent<AudioSource>().PlayOneShot(aud_Death);
        }
    }

    public void PlayBuy()
    {
        if(playSounds)
            GetComponent<AudioSource>().PlayOneShot(aud_Buy);
    }



    public void ToggleSound()
    {
        if (playSounds)
            playSounds = false;
        else
            playSounds = true;
    }
}
