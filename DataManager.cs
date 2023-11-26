using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;


[System.Serializable]
public class PlayerData
{
    public string name;
    public float Hp;
    public Vector2 position;
    public Scene nowScene;
    public string nowChapter_name;
    //public WeatherData weatherdata;
    public string itemin;
    public int item_count;
    public Sprite itemImages;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance; // �̱�������

    public PlayerData nowPlayer = new PlayerData(); // �÷��̾� ������ ����

    public string path; // ���
    public int nowSlot; // ���� ���Թ�ȣ
    public string nowName;

    private void Awake()
    {
        #region �̱���
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion

        path = Application.persistentDataPath + "/save";	// ��� ����
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + nowSlot.ToString(), data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
        name = nowPlayer.name;
    }
    public void DeleteData()
    {
        File.Delete(path + nowSlot.ToString());
    }
    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }
}