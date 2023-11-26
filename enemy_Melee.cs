using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;

public class enemy_Melee : MonoBehaviour // ���� ���� ������ ���� ��ũ��Ʈ
{
    private play_game player;

    static public melee instance;

    public Transform self;
    public bool attack = false;
    public BoxCollider2D boxCollider;

    void Awake()
    {
        player = FindObjectOfType<play_game>();
        self = GetComponent<Transform>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6){
            if (collision.gameObject.tag == "Player"){
                if(attack == true){
                    attack = false;
                    player.OnDamaged();
                }
            }
        }
    }
}