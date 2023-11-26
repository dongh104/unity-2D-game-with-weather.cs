using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; //Scene �Ŵ��� ���̺귯�� �߰�

public class transfermap : MonoBehaviour
{
    public string transferMapName; // �̵��� ���̸�
    private play_game thePlayer;

    public int pnum;

    public float request_Level;
    public GameObject go;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<play_game>();
        //thePlayer.currentMapName = now_map;
    }

    // �ڽ� �ݶ��̴��� ��� ���� �̺�Ʈ �߻�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            thePlayer.currentMapName = transferMapName;
            thePlayer.pnum = pnum;
        }
        else
        {
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "Player" && Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (request_Level > thePlayer.Level)
            {
                go.SetActive(true);
            }
            else
            {
                SceneManager.LoadScene(transferMapName);
            }
        }
    }
    public void escape()
    {
        go.SetActive(false);
    }
}