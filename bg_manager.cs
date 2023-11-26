using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class bg_manager : MonoBehaviour
{
    Rigidbody2D bg1; // 배경1
    Rigidbody2D bg2; // 배경2
    public GameObject background;

    public play_game player;

    void Awake()
    {
        player = FindAnyObjectByType<play_game>();
    }
    private void Start()
    {
        gameObject.transform.position = player.transform.position;
    }
    private void Update()
    {
        if (player.hit)
            gameObject.transform.position = new Vector2(player.transform.position.x, gameObject.transform.position.y);
    }
    private void FixedUpdate()
    {
        if (player.inputVec.x > 0 && Input.GetKey(KeyCode.RightArrow))
        {  
                background = GameObject.Find("background");
                bg1 = background.GetComponent<Rigidbody2D>();
                bg1.velocity = new Vector2(player.speed, bg1.velocity.y);
        }
        if (player.inputVec.x < 0 && Input.GetKey(KeyCode.LeftArrow))
        {
                background = GameObject.Find("background");
                bg1 = background.GetComponent<Rigidbody2D>();
                bg1.velocity = new Vector2(-player.speed, bg1.velocity.y);
        }
        if (player.inputVec.x == 0 || player.rigid.velocity.x > -5.5f || player.rigid.velocity.x < 5.5f)
        {
            bg1.velocity *= 0.95555555f;
        }
    }
}
