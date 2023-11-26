using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using Unity.VisualScripting;
using System;
//using UnityEditorInternal.Profiling.Memory.Experimental;

public class game_system : MonoBehaviour
{
    public GameObject start;
    public GameObject load;
    public GameObject creat;
    public Text[] slotText;
    public Text[] slotText2;
    public Text[] slotText3;
    public Text newPlayerName;

    bool[] savefile = new bool[3];

    public GameObject cancel;
    public GameObject EG1;
    public GameObject EG2;
    public GameObject EG3;

    public GameManager gm;
    public Texture2D texture;
    public Inven inven;
    public List<Item_info> items;
    public asdf asdf;
    private void Awake()
    {
        gm = FindAnyObjectByType<GameManager>();
        inven = FindObjectOfType<Inven>();
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(DataManager.instance.path + i))
            {
                savefile[i] = true;
                DataManager.instance.nowSlot = i;
                DataManager.instance.LoadData();
                slotText[i].text = DataManager.instance.nowPlayer.name; // DataManager에 저장된 이름을 가져와 슬롯에 띄움

                slotText2[i].text = DataManager.instance.nowPlayer.name; // DataManager에 저장된 이름을 가져와 슬롯에 띄움
                
                slotText3[i].text = DataManager.instance.nowPlayer.name; // DataManager에 저장된 이름을 가져와 슬롯에 띄움
                //DataManager.instance.name = DataManager.instance.nowPlayer.name;
            } else
            {
                slotText[i].text = "빈 데이터";
            }
        }
        //Debug.Log(DataManager.instance.name);
        DataManager.instance.DataClear();
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
           
            //Exit_game();
        //}
    }

    public void New_game()
    {
        start.SetActive(true);
        //SceneManager.LoadScene("game_play");
    }
    public void back()
    {
        start.SetActive(false);
    }

    public void Slot(int number)
    {
        DataManager.instance.nowSlot = number;

        if (savefile[number])
        {
            ExistGame();

            //DataManager.instance.LoadData();
            //GoGame();
        }
        else
        {
            Creat();
        }
    }
    public void Slot2(int number)
    {
        DataManager.instance.nowSlot = number;

        if (savefile[number])
        {
            DataManager.instance.LoadData();
            Load();
        }
        else
        {
            EG3.SetActive(true);
        }
    }
    public void Creat()
    {
        creat.gameObject.SetActive(true);  
    }
    public void ExistGame()
    {
        EG1.SetActive(true); 
    }
    public void GoGame()
    {
        if (!savefile[DataManager.instance.nowSlot])
        {
            DataManager.instance.nowPlayer.name = newPlayerName.text;
            DataManager.instance.SaveData();
        }
        SceneManager.LoadScene("game_play");
        gm.PlayerName.text = DataManager.instance.nowPlayer.name;
        DataManager.instance.nowPlayer.nowChapter_name = "play_game";
    }
    public void Load()
    {
        if (!savefile[DataManager.instance.nowSlot])
        {
            DataManager.instance.nowPlayer.name = newPlayerName.text;
            DataManager.instance.SaveData();
        }
        if (DataManager.instance.nowPlayer.nowChapter_name == "")
        {
            SceneManager.LoadScene("game_play");
            play_game.instance.Hp = 100;
        }
        else
        {
            SceneManager.LoadScene(DataManager.instance.nowPlayer.nowChapter_name);
            play_game.instance.Hp = DataManager.instance.nowPlayer.Hp;
        }
        gm.getItem.Clear(); // 리스트 초기화

        byte[] imageBytes = Convert.FromBase64String(DataManager.instance.nowPlayer.itemin);
        Texture2D decodedTexture = new Texture2D(1, 1);
        decodedTexture.LoadImage(imageBytes);

        // 이미지 디코딩 및 스프라이트 업데이트
        Sprite itemSprite = Sprite.Create(decodedTexture, new Rect(0, 0, decodedTexture.width, decodedTexture.height), new Vector2(0.5f, 0.5f));

        // 아이템 리스트를 업데이트
        for (int i = 0; i < DataManager.instance.nowPlayer.item_count; i++)
        {
            Item_info newItem = new Item_info();
            newItem.itemImage = itemSprite;
            gm.getItem.Add(newItem);
        }

        play_game.instance.transform.position = DataManager.instance.nowPlayer.position;
        gm.PlayerName.text = DataManager.instance.nowPlayer.name;

        //play_game.instance.gg();
    }
    public void DeleteGame(int number)
    {
        DataManager.instance.nowSlot = number;

        if (savefile[DataManager.instance.nowSlot])
        {
            DataManager.instance.DeleteData();
            savefile[DataManager.instance.nowSlot] = false;
            slotText[DataManager.instance.nowSlot].text = "빈 데이터"; // 해당 슬롯의 텍스트를 "빈 데이터"로 변경
            slotText2[DataManager.instance.nowSlot].text = "빈 데이터";
            slotText3[DataManager.instance.nowSlot].text = "빈 데이터";
        } else
        {
            EG2.SetActive(true);
        }
    }
    public void ccc()
    {
        cancel.SetActive(false);
    }

    public void Load_game()
    {
        load.SetActive(true);
    }

    public void Exit_game()
    {
        Application.Quit();
    }

}
