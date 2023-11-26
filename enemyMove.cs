using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class enemyMove : MonoBehaviour
{
    //다양한 변수와 필드를 정의하여 적 캐릭터에 대한 정보를 저장
    Rigidbody2D rigid;
    public Animator anim;
    public int nextMove;
    SpriteRenderer spriter;
    public float monsterSpeed;
    bool isAtk = false;

    public Transform target; //적 캐릭터가 공격할 대상의 위치를 저장하는 변수
    private play_game instance;

    public float maxHp; //최대체력
    public float currentHp; //현재 체력
    public bool damaged = false;

    public GameObject attscoll; //공격 판정을 가지고 있는 게임 오브젝트

    enemySpawner ep;
    public int a;
    public float damage;

    public GameObject hudDamageText;
    public Transform hudPos;

    public GameObject Item;
    GameManager gm;

    public imshi stat;
    public BoxCollider2D boxCollider;
    public bool changed = false;
    public new Vector2 newSize;
    public enemy_Melee melee;
    public GameObject hpbar1;
    public GameObject hpbar2;
    void Awake()
    {
        //게임 오브젝트의 컴포넌트들에 대한 참조를 초기화
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();

        instance = FindObjectOfType<play_game>();

        ep = FindObjectOfType<enemySpawner>();

        gm = FindObjectOfType<GameManager>();

        stat = FindObjectOfType<imshi>();
        boxCollider = GetComponent<BoxCollider2D>();
        melee = FindObjectOfType<enemy_Melee>();

        currentHp = maxHp; // �ִ� ü������ �ʱ�ȭ
        //Think 메서드를 4초 뒤에 한번 호출
        Invoke("Think", 4);
    }

    private void Update()
    {
        if (gameObject.name == "haribo(Clone)")
        {
            if (stat.w == "날씨 : 비" && !changed)
            {
                changed = true;
                anim.SetBool("rain", true);
                damage = 15;
                maxHp += 50000;
                currentHp += 50000;
                melee.boxCollider.size = new Vector2(0.9f, 2f);
                hpbar1.transform.position = new Vector2(hpbar1.transform.position.x, 2.0f);
                hpbar2.transform.position = new Vector2(hpbar1.transform.position.x, 2.0f);
                boxCollider.size = new Vector2(2.3f, 2.9f);
            }
            else if (stat.w != "날씨 : 비" && !changed)
            {
            }
            else if (stat.w == "날씨 : 비" && changed)
            {
            }
            else if (stat.w != "날씨 : 비" && changed)
            {
                changed = false;
                anim.SetBool("rain", false);
                damage = 5;
                maxHp = 100000;
                currentHp = currentHp * 2 / 3;
                melee.boxCollider.size = new Vector2(0.52f, 0.82f);
                hpbar1.transform.position = new Vector2(hpbar1.transform.position.x, 2.4f);
                hpbar2.transform.position = new Vector2(hpbar1.transform.position.x, 2.4f);
                boxCollider.size = new Vector2(1.07f, 1.39f);
            }
        }
    }

    void FixedUpdate()
    {
        //공격 상태를 해제
        anim.SetBool("isAtk", false);

        float dis;

        if (target != null)
        {
            //적과 대상 사이의 거리를 계산
            dis = Vector2.Distance(transform.position, target.position);

            if (damaged == true)
            {
                anim.SetInteger("walkSpeed", 0);
                return;
            }
            attscoll.SetActive(false); // 공격판정 비활성화


            if (dis < 4 && dis > 2)
            {
                //일정 범위 내에 있으면 대상 쪽으로 이동 
                if (a != 0)
                {
                    if (transform.position.x > target.position.x)
                    {
                        spriter.flipX = false;
                        anim.SetInteger("walkSpeed", -1);
                        rigid.velocity = new Vector2(-3, rigid.velocity.y);
                    }
                    else if (transform.position.x < target.position.x)
                    {
                        spriter.flipX = true;
                        anim.SetInteger("walkSpeed", 1);
                        rigid.velocity = new Vector2(+3, rigid.velocity.y);
                    }
                } else
                {
                    if (transform.position.x > target.position.x)
                    {
                        spriter.flipX = true;
                        anim.SetInteger("walkSpeed", -1);
                        rigid.velocity = new Vector2(-3, rigid.velocity.y);
                    }
                    else if (transform.position.x < target.position.x)
                    {
                        spriter.flipX = false;
                        anim.SetInteger("walkSpeed", 1);
                        rigid.velocity = new Vector2(+3, rigid.velocity.y);
                    }
                }
            }
            else if (dis <= 2)
            {
                //일정 범위 내에 있으면 공격 상태로 전환
                if (a != 0)
                {
                    if (transform.position.x > target.position.x)
                    {
                        spriter.flipX = false;
                    }
                    else if (transform.position.x < target.position.x)
                    {
                        spriter.flipX = true;
                    }
                }
                else
                {
                    if (transform.position.x > target.position.x)
                    {
                        spriter.flipX = true;
                    }
                    else if (transform.position.x < target.position.x)
                    {
                        spriter.flipX = false;
                    }
                }
                SetAtk(); //공격 상태로 설정 
                

            }
            else
            {
                //대상과 일정 거리 이상 떨어져 있으면 이동 
                rigid.velocity = new Vector2(monsterSpeed * nextMove, rigid.velocity.y);
            }
        }
        else if (target == null)
        {
            // 대상이 없으면 플레이어 캐릭터의 위치와의 거리를 계산 
            dis = Vector2.Distance(transform.position, instance.transform.position);

            if (damaged == true)
            {
                anim.SetInteger("walkSpeed", 0);
                return;
            }
            attscoll.SetActive(false); //공격 판정 비활성화

            if (dis < 5 && dis > 2)
            {
                if (a != 0)
                {
                    if (transform.position.x > instance.transform.position.x)
                    {
                        spriter.flipX = false;
                        anim.SetInteger("walkSpeed", -1);
                        rigid.velocity = new Vector2(-3, rigid.velocity.y);
                    }
                    else if (transform.position.x < instance.transform.position.x)
                    {
                        spriter.flipX = true;
                        anim.SetInteger("walkSpeed", 1);
                        rigid.velocity = new Vector2(+3, rigid.velocity.y);
                    }
                } else
                {
                    if (transform.position.x > instance.transform.position.x)
                    {
                        spriter.flipX = true;
                        anim.SetInteger("walkSpeed", -1);
                        rigid.velocity = new Vector2(-3, rigid.velocity.y);
                    }
                    else if (transform.position.x < instance.transform.position.x)
                    {
                        spriter.flipX = false;
                        anim.SetInteger("walkSpeed", 1);
                        rigid.velocity = new Vector2(+3, rigid.velocity.y);
                    }
                }
            }
            else if (dis <= 2)
            {
                if (a != 0)
                {
                    if (transform.position.x > instance.transform.position.x)
                    {
                        spriter.flipX = false;
                    }
                    else if (transform.position.x < instance.transform.position.x)
                    {
                        spriter.flipX = true;
                    }
                }
                else
                {
                    if (transform.position.x > instance.transform.position.x)
                    {
                        spriter.flipX = true;
                    }
                    else if (transform.position.x < instance.transform.position.x)
                    {
                        spriter.flipX = false;
                    }
                }
                //Invoke("SetAtk", 2f);
                SetAtk(); //공격상태로 설정 
                
            }
            else
            {
                //플레이어 캐릭터와 일정 거리 이상 떨어지면 이동
                rigid.velocity = new Vector2(monsterSpeed * nextMove, rigid.velocity.y);
            }
        }

        //공격 판정 위치를 설정 
        if (gameObject.name == "haribo(Clone)") {
            if (!changed)
            {
                if (spriter.flipX)
                    attscoll.transform.position = new Vector2(transform.position.x - 0.25f, transform.position.y);
                else
                    attscoll.transform.position = new Vector2(transform.position.x - 0.9f, transform.position.y);
            }
            else if (changed)
            {
                if (spriter.flipX)
                    attscoll.transform.position = new Vector2(transform.position.x, transform.position.y);
                else
                    attscoll.transform.position = new Vector2(transform.position.x - 1.1f, transform.position.y);
            }
        } else
        {
            if (spriter.flipX)
                attscoll.transform.position = new Vector2(transform.position.x - 0.25f, transform.position.y);
            else
                attscoll.transform.position = new Vector2(transform.position.x - 0.9f, transform.position.y);
        }
    }

    void Think()
    {
        //대상이 존재하면 대상과의 거리를 계산하고 움직인 다음 방향을 결정 
        float dis = (target != null) ? Vector2.Distance(transform.position, target.position)
                                     : Vector2.Distance(transform.position, instance.transform.position);

        if (dis <= 2)
        {
            nextMove = 0;
        }
        else
        {
            nextMove = Random.Range(-1, 2);
        }

        anim.SetInteger("walkSpeed", nextMove);

        if (nextMove != 0)
            spriter.flipX = nextMove == 1;

        
        //다음 Think 메서드를 호출하기 까지의 대기시간을 랜덤으로 설정
        float nextThinkTime = Random.Range(1f, 3f);
        
        Invoke("Think", nextThinkTime);
    }

    void SetAtk() //공격 상태로 전환하는 메서드
    {
        anim.SetInteger("walkSpeed", 0);
        anim.SetBool("isAtk", true);
        //anim.Play("haribo_atk");

        attscoll.SetActive(true); // 공격 활성화
        //invoke("EndAtk", 2f);

        if (instance != null)
            instance.OnDamaged(); // 공격 시 플레이어가 데미지를 받도록 함
    }
    void SetAtk2() //공격 상태로 전환하는 메서드
    {
        attscoll.GetComponent<enemy_Melee>().attack = true;
        //invoke("EndAtk", 2f);
        
    }

    void ResetAtk()
    {
        //공격 상태 초기화
        attscoll.GetComponent<enemy_Melee>().attack = false;
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

        //Debug.Log(instance.damage);
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
            //적 캐릭터의 체력이 0이하면 적 캐릭터를 파괴하고 스폰 카운트를 감소 
            Destroy(gameObject);

            ep.currentSpawnCount -= 1;

            float DropChance = Random.Range(0f, 1f);

            //if (DropChance < 0.3f)
            //{
                GameObject Drop_Item = Instantiate(Item, new Vector2(transform.position.x, transform.position.y), transform.rotation);
                Rigidbody2D ItemRigidbody = Drop_Item.GetComponent<Rigidbody2D>();
                ItemRigidbody.AddForce(new Vector2(0.0f, 5.0f), ForceMode2D.Impulse);

            //}

            instance.Exp += 10;
        }
    }
}
