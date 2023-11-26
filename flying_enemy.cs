using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class flying_enemy : MonoBehaviour
{
    //다양한 변수와 필드를 정의하여 적 캐릭터에 대한 정보를 저장
    Rigidbody2D rigid;
    Animator anim;
    public int nextMove;
    SpriteRenderer spriter;
    public float monsterSpeed;
    public int a;
    public Transform target; //적 캐릭터가 공격할 대상의 위치를 저장하는 변수
    private play_game instance;

    public float maxHp; //최대체력
    public float currentHp; //현재 체력
    public bool damaged = false;

    enemySpawner ep;
    public float damage;
    public bool attack = false;

    public GameObject hudDamageText;
    public Transform hudPos;

    public GameObject Item;
    GameManager gm;
    void Awake()
    {
        //게임 오브젝트의 컴포넌트들에 대한 참조를 초기화
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();

        instance = FindObjectOfType<play_game>();

        ep = FindObjectOfType<enemySpawner>();

        gm = FindObjectOfType<GameManager>();

        currentHp = maxHp; // �ִ� ü������ �ʱ�ȭ
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            if (a != 0)
            {
                if (transform.position.x > instance.transform.position.x)
                {
                    spriter.flipX = false;
                    rigid.velocity = new Vector2(-monsterSpeed, rigid.velocity.y);
                }
                else if (transform.position.x < instance.transform.position.x)
                {
                    spriter.flipX = true;
                    rigid.velocity = new Vector2(monsterSpeed, rigid.velocity.y);
                }
            }
            else
            {
                if (transform.position.x > instance.transform.position.x)
                {
                    spriter.flipX = true;
                    rigid.velocity = new Vector2(-monsterSpeed, rigid.velocity.y);
                }
                else if (transform.position.x < instance.transform.position.x)
                {
                    spriter.flipX = false;
                    rigid.velocity = new Vector2(monsterSpeed, rigid.velocity.y);
                }
            }
        }
        else if (target == null)
        {
            if (a != 0)
            {
                if (transform.position.x > instance.transform.position.x)
                {
                    spriter.flipX = false;
                    rigid.velocity = new Vector2(-monsterSpeed, rigid.velocity.y);
                }
                else if (transform.position.x < instance.transform.position.x)
                {
                    spriter.flipX = true;
                    rigid.velocity = new Vector2(monsterSpeed, rigid.velocity.y);
                }
            }
            else
            {
                if (transform.position.x > instance.transform.position.x)
                {
                    spriter.flipX = true;
                    rigid.velocity = new Vector2(-monsterSpeed, rigid.velocity.y);
                }
                else if (transform.position.x < instance.transform.position.x)
                {
                    spriter.flipX = false;
                    rigid.velocity = new Vector2(monsterSpeed, rigid.velocity.y);
                }
            }
        }
    }

    public void EndDmg()
    {
        //데미지를 받은 후의 처리를 수행
        spriter.color = new Color(1, 1, 1, 1);
        gameObject.layer = 7;
        damaged = false;
        Dead();
    }

    public void setDmg(float damage)
    {
        //데미지를 설정하고 플레이어 방향으로 밀어냄
        spriter.color = new Color(1, 1, 1, 0.4f);

        int dirc;
        if (target != null)
            dirc = transform.position.x - target.position.x > 0 ? 1 : -1;
        else
            dirc = transform.position.x - instance.transform.position.x > 0 ? 1 : -1;

        rigid.AddForce(new Vector2(dirc, 1) * 3, ForceMode2D.Impulse);

        gameObject.layer = 9;
        damaged = true;

        currentHp -= damage;

        //일정 시간 후에 EndDmg 메서드를 호출하여 데미지 상태를 해제함
        Invoke("EndDmg", 0.3f);
        GameObject hudText = Instantiate(hudDamageText);
        hudText.transform.position = hudPos.position;
        hudText.GetComponent<DamageText>().damage = damage;
    }

    public void Dead()
    {
        if (currentHp <= 0)
        {
            Destroy(gameObject);

            float DropChance = Random.Range(0f, 1f);

            //if (DropChance < 0.3f)
            //{
            GameObject Drop_Item = Instantiate(Item, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            Rigidbody2D ItemRigidbody = Drop_Item.GetComponent<Rigidbody2D>();
            ItemRigidbody.AddForce(new Vector2(0.0f, 5.0f), ForceMode2D.Impulse);

            //}
        }
    }
}
