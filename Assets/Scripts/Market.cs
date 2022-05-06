using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Market : MonoBehaviour
{
    private GameObject soundController;

    public GameObject pnl_Market;
    public Text txt_Money;

    public GameObject[] canvas_Planes;
    public GameObject o_Tick;
    public Transform[] tick_Transform;
    public GameObject[] p_planes;
    public static int p_Index;

    public bool[] ownedItemsBool;
    public Sprite[] boughtImages;
    public Sprite[] sellingImages;

    public int cheatClicked = 0;
    public float cheatTimer = 2;

    string ownedItemsSt = "1.0.0.0.0.0.0.0.0.0.0.0";
    public string[] ownedItemsArr;

    public int money;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetString("OwnedItems", "1.0.0.0.0.0.0.0.0.0.0.0");
        ownedItemsSt = PlayerPrefs.GetString("OwnedItems", "1.0.0.0.0.0.0.0.0.0.0.0");         //Gets the items that player owned
        Debug.Log("uçaklar : "+ownedItemsSt);
        money = PlayerPrefs.GetInt("Money", 0);
        //ownedItemsSt = "1.0.1.0.0.0.0.0.0.1.1.1";
        ownedItemsArr = ownedItemsSt.Split('.');
        for(int s = 0; s < 12; s++)
        {
            Debug.Log(s);
            if(ownedItemsArr[s] == "1")
            {
                ownedItemsBool[s] = true;
            }
            else
            {
                ownedItemsBool[s] = false;
            }
        }

        p_Index = PlayerPrefs.GetInt("planeIndex", 2);
        Camera.main.SendMessage("ChangePlane", p_planes[p_Index], SendMessageOptions.DontRequireReceiver);
        soundController = GameObject.FindGameObjectWithTag("Sound");

        UpdateMarketPics();
        UpdateTick();
        txt_Money.text = money + " #";
        pnl_Market.SetActive(false);        //In order to set tick transform and market pic, we start with market panel open and then close it later
    }

    // Update is called once per frame
    void Update()
    {
        

        int mn = PlayerPrefs.GetInt("Money", 0);
        Debug.Log("Money: " + mn);

        if(cheatClicked > 0)
        {
            cheatTimer -= Time.deltaTime;
        }

        if(cheatTimer <= 0)
        {
            cheatClicked = 0;
            cheatTimer = 2f;
        }
    }

    public void SelectPlane(int num)
    {
        money = PlayerPrefs.GetInt("Money", 0);
        if (ownedItemsBool[num] == true)
        {

            p_Index = num;
            PlayerPrefs.SetInt("planeIndex", p_Index);
            Camera.main.SendMessage("ChangePlane", p_planes[num], SendMessageOptions.DontRequireReceiver);
            GameObject currPlane = GameObject.FindGameObjectWithTag("Player");
            currPlane.SendMessage("ChangeSpeed", num, SendMessageOptions.DontRequireReceiver);

            UpdateTick();
        }
        else
        {

            switch (num)
            {
                case 0:
                    BuyItem(0, 10);
                    break;
                case 1:
                    BuyItem(1, 10);
                    break;
                case 2:
                    BuyItem(2, 10);
                    break;
                case 3:
                    BuyItem(3, 10);
                    break;
                case 4:
                    BuyItem(4, 20);
                    break;
                case 5:
                    BuyItem(5, 20);
                    break;
                case 6:
                    BuyItem(6, 20);
                    break;
                case 7:
                    BuyItem(7, 20);
                    break;
                case 8:
                    BuyItem(8, 50);
                    break;
                case 9:
                    BuyItem(9, 50);
                    break;
                case 10:
                    BuyItem(10, 50);
                    break;
                case 11:
                    BuyItem(11, 100);
                    break;
            }                       //Happens if player doesnt have that item
            Debug.Log("owneditenms: " + ownedItemsSt);
        }


    }

    public void BuyItem(int num_Plane, int price)
    {
        if (money >= price)
        {
            soundController.SendMessage("PlayBuy", SendMessageOptions.DontRequireReceiver);
            ownedItemsBool[num_Plane] = true;
            UpdateOwnedItems();
            UpdateMarketPics();
            SelectPlane(num_Plane);
            money -= price;
            txt_Money.text = money + " #";
            PlayerPrefs.SetInt("Money", money);
        }
    }

    public void UpdateTick()
    {
        Debug.Log("Tick Updated" + p_Index);
        GameObject tick = GameObject.FindGameObjectWithTag("Tick");
        tick.transform.position = new Vector2(tick_Transform[p_Index].position.x, tick_Transform[p_Index].position.y) ;
    }

    public void OpenAllCheat()
    {
        if (cheatClicked == 8)
        {
            for (int k = 0; k < ownedItemsBool.Length; k++)
            {
                ownedItemsBool[k] = true;
            }
            UpdateOwnedItems();
        }
        else
        {
            cheatClicked++;
        }
        UpdateMarketPics();
    }

    public void UpdateOwnedItems()
    {
        ownedItemsSt = "1";
        for (int ind = 1; ind < 12; ind++)
        {
            if (ownedItemsBool[ind])
            {
                ownedItemsSt += ".1";
            }
            else
            {
                ownedItemsSt += ".0";
            }
        }
        PlayerPrefs.SetString("OwnedItems", ownedItemsSt);
        Debug.Log("updated st : " + ownedItemsSt);
    }

    public void UpdateMarketPics()
    {
        for(int p = 0; p < ownedItemsBool.Length; p++)
        {
            Image cnv_Img = canvas_Planes[p].GetComponent<Image>();
            if (ownedItemsBool[p])
            {
                cnv_Img.sprite = boughtImages[p];
            }
            else
            {
                cnv_Img.sprite = sellingImages[p];
            }
        }
    }

}
