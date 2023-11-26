using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public Rigidbody2D rigid;
    public float rotationSpeed = 180f;
    private float destroyTime;
    SpriteRenderer SR;
    
    public play_game player;
    public Inven Inven;

    private Item_info item_info;
    public Sprite image;

    GameManager gm;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<play_game>();
        Inven = FindObjectOfType<Inven>();

        gm = FindObjectOfType<GameManager>();

        item_info = new Item_info();
        item_info.itemImage = SR.sprite;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //DestroyItem();
            TryToCollectItem();
        }
    }

    // 충돌 감지
    void TryToCollectItem()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f); // 아이템 주변의 콜라이더 검출

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                CollectItem();
                break;
            }
        }
    }

    void CollectItem()
    {
        Destroy(gameObject); // 아이템 개체 파괴
        gm.getItem.Add(item_info);
        player.EC++;
        // 여기에 필요한 플레이어의 획득 처리 코드를 추가할 수 있습니다.
    }
}
