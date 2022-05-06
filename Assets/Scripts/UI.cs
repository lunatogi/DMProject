using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject pnlStart;
    public GameObject pnlPause;
    public GameObject pnlMarket;
    public GameObject pnlSettings;
    public GameObject pnlResetGame;
    public GameObject pnlRestart;

    public GameObject tickMusic;
    public GameObject tickSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        pnlStart.SetActive(false);
        Camera.main.SendMessage("StartGameJoy", SendMessageOptions.DontRequireReceiver);
        Camera.main.SendMessage("StartGameSp", SendMessageOptions.DontRequireReceiver);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.SendMessage("StartGamePlay", SendMessageOptions.DontRequireReceiver);
    }

    public void PauseGame(bool end)
    {
        Debug.Log("game paused");
        if(!end)
            pnlPause.SetActive(true);
        GameObject[] currEnemies = GameObject.FindGameObjectsWithTag("Enemy");                                  //Destroys all left bullets
        foreach (GameObject enemy in currEnemies)
        {
            enemy.SendMessage("Pause",SendMessageOptions.DontRequireReceiver);
        }
        Camera.main.SendMessage("Pause", SendMessageOptions.DontRequireReceiver);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.SendMessage("Pause", SendMessageOptions.DontRequireReceiver);
    }

    public void ContinueGame()
    {
        pnlPause.SetActive(false);
        GameObject[] currEnemies = GameObject.FindGameObjectsWithTag("Enemy");                                  //Destroys all left bullets
        foreach (GameObject enemy in currEnemies)
        {
            enemy.SendMessage("Continue", SendMessageOptions.DontRequireReceiver);
        }
        Camera.main.SendMessage("StartGame", SendMessageOptions.DontRequireReceiver);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.SendMessage("Continue", SendMessageOptions.DontRequireReceiver);
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }

    public void Market()
    {
        pnlStart.SetActive(false);
        pnlMarket.SetActive(true);
    }

    public void Settings()
    {
        pnlStart.SetActive(false);
        pnlSettings.SetActive(true);
    }

    public void ToggleMusic()
    {
        GameObject soundGameObject = GameObject.FindGameObjectWithTag("Music");
        if(soundGameObject.GetComponent<Music>().playMusic)
        {
            soundGameObject.SendMessage("PauseMusic", SendMessageOptions.DontRequireReceiver);
            tickMusic.SetActive(false);
        }
        else
        {
            soundGameObject.SendMessage("ContinueMusic", SendMessageOptions.DontRequireReceiver);
            tickMusic.SetActive(true);
        }
    }

    public void CloseMusicTick()
    {
        tickMusic.SetActive(false);
    }

    public void ToggleSound()
    {
        GameObject soundGameObject = GameObject.FindGameObjectWithTag("Sound");
        if (soundGameObject.GetComponent<Sound>().playSounds)
        {
            soundGameObject.SendMessage("ToggleSound", SendMessageOptions.DontRequireReceiver);
            tickSound.SetActive(false);
        }
        else
        {
            soundGameObject.SendMessage("ToggleSound", SendMessageOptions.DontRequireReceiver);
            tickSound.SetActive(true);
        }
    }

    public void CloseSoundTick()
    {
        tickSound.SetActive(false);
    }

    public void ResetGame()
    {
        pnlResetGame.SetActive(true);
    }

    public void ResetYes()
    {
        Camera.main.SendMessage("SelectPlane", 0, SendMessageOptions.DontRequireReceiver);
        PlayerPrefs.SetString("OwnedItems", "1.0.0.0.0.0.0.0.0.0.0.0");
        PlayerPrefs.SetInt("highscore", 0);
        PlayerPrefs.SetInt("Money", 0);
        pnlResetGame.SetActive(false);
    }

    public void ResetNo()
    {
        pnlResetGame.SetActive(false);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus && pnlMarket.activeSelf == false && pnlStart.activeSelf == false && pnlPause.activeSelf == false && pnlSettings.activeSelf == false && pnlRestart.activeSelf == false)
        {
            PauseGame(false);
        }
    }
}
