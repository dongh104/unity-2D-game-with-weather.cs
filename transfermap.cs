using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; //Scene 매니저 라이브러리 추가

public class transfermap : MonoBehaviour
{
    public string transferMapName; // 이동할 맵이름
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

    // 박스 콜라이더에 닿는 순간 이벤트 발생
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