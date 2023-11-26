using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;

public class fenemy_Melee : MonoBehaviour // ���� ���� ������ ���� ��ũ��Ʈ
{
    private play_game player;

    static public melee instance;

    public Transform self;

    void Awake()
    {
        player = FindObjectOfType<play_game>();
        self = GetComponent<Transform>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6){
            if (collision.gameObject.tag == "Player"){
                    player.OnDamaged2();
            }
        }
    }
}