using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class play_game : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public float jumpPower;
    public float doublejump;
    public bool isJumping = false;

    bool isAttack = false;
    float atk = 0;

    public Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    private float curTime; // 공격모션 쿨타임
    public float coolTime = 0.2f; // 공격 쿨타임 : 0.2초

    private float skill_curTime1; // 스킬모션 쿨타임
    public float skill_Cooldown1; // 스킬 쿨다운 시간 : 3초

    private float skill_curTime2; // 스킬모션 쿨타임
    public float skill_Cooldown2; // 스킬 쿨다운 시간 : 3초


    public int Level = 1;
    public float Hp = 100f;
    public float Exp = 0f;
    public Exp_bar eb;
    public float damage;
    public int EC = 0;

    public string currentMapName; // 플레이어가 위치한 챕터, 이동하게 될 챕터
    public int pnum;

    static public play_game instance; // 플레이어 오브젝트 파괴 되었을때 받아올 객체 변수

    public GameObject attscoll; // 플레이어 공격 범위
    public GameObject s1_attscoll;
    public GameObject s2_attscoll;
    
    public melee mel;
    public skill1_melee smel1;
    public skill2_melee smel2;

    public enemyMove enemy;
    public flying_enemy fenemy;


    private float timer = 0f; //챕터6 디버프
    private float decreaseInterval_1 = 3f;

    private float idleTimer = 0f; // 움직임이 없는 시간을 측정하는 타이머
    private float decreaseInterval_2 = 5f;

    game_system gs;

    public GameObject hudDamageText;
    public Transform hudPos;

    List<GameObject> getItem;

    public bg_manager bm;
    public bool hit = false;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        enemy = FindAnyObjectByType<enemyMove>();
        fenemy = FindAnyObjectByType<flying_enemy>();

        gs = FindAnyObjectByType<game_system>();

        smel1 = FindAnyObjectByType<skill1_melee>();
        smel2 = FindAnyObjectByType<skill2_melee>();
        
        bm = FindAnyObjectByType<bg_manager>();

        eb = FindAnyObjectByType<Exp_bar>();
    }

    void Start()
    {
        if (instance == null) // 플레이어 챕터 이동 시 플레이어 오브젝트 파괴되는 것을 방지
        {
            DontDestroyOnLoad(this.gameObject); // 게임 오브젝트 파괴 금지
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        s1_attscoll.SetActive(false);
        s2_attscoll.SetActive(false);
        //transform.position = DataManager.instance.nowPlayer.position;
        //SceneManager.sceneLoaded += OnSceneLoaded; // 씬이 로드될 때마다 이벤트 핸들러 호출
    }

    void Update()
    {
        inputVec.x = Input.GetAxis("Horizontal");
        eb.Level_Up();
        if (curTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                isAttack = true;

                damage = Random.Range(9899, 9999);
                anim.SetFloat("blend", atk);
                anim.SetTrigger("atk");
                atk++;

                if (atk > 1)
                {
                    atk = 0;
                }
                curTime = coolTime;

                attscoll.SetActive(true);
            }
            else
            {
                //anim.SetBool("atk", false);
                isAttack = false;
                attscoll.SetActive(false);
            }
        }
        else
        {
            curTime -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt) && (doublejump < 1)) // c클릭시 점프
        { //조건 추가
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            doublejump++;

            isJumping = true;
            anim.SetBool("isJumping", isJumping);
        }
        
        if (skill_curTime1 <= 0) // 스킬 1번
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                s1_attscoll.SetActive(true);
                smel1.damage = Random.Range(44999, 49999);
                if(spriter.flipX)
                    s1_attscoll.transform.position = new Vector2(transform.position.x + 3.3f, 2);
                else
                    s1_attscoll.transform.position = new Vector2(transform.position.x - 2.91f, 2);
                smel1.isSkill = true;
                skill_curTime1 = skill_Cooldown1;
                smel1.anim.SetBool("skill_1", smel1.isSkill);
                StartCoroutine(smel1.ActivateCollider());
            }
            else
            {
                smel1.isSkill = false;
                smel1.BC2.enabled = false;
                smel1.anim.SetBool("skill_1", smel1.isSkill);
                s1_attscoll.SetActive(false);
            }
        }
        else
        {
            skill_curTime1 -= Time.deltaTime;
        }

        if (skill_curTime2 <= 0) // 스킬 2번
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                s2_attscoll.SetActive(true);
                smel2.damage = Random.Range(18999, 19999);
                smel2.rigid.isKinematic = false;
                smel2.rigid.gravityScale = 2f;
                smel2.BC2.isTrigger = false;
                s2_attscoll.transform.position = transform.position;
                
                if (spriter.flipX)
                    smel2.rigid.AddForce(new Vector2(3.4f, 8.0f), ForceMode2D.Impulse);
                else
                    smel2.rigid.AddForce(new Vector2(-3.4f, 8.0f), ForceMode2D.Impulse);

                smel2.isSkill = true;
                    
                skill_curTime2 = skill_Cooldown2 + 0.4f;
                Invoke("ddd", 0.83f);
                
                StartCoroutine(smel2.ActivateCollider());
            }
            else
            {
                smel2.isSkill = false;
                smel2.BC2.enabled = false;
                smel2.anim.SetBool("skill_2", smel2.isSkill);
                s2_attscoll.SetActive(false);
            }
        }
        else
        {
            skill_curTime2 -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) // 물약 키보드 1번
        {
            Recovery();
        }

        if (bm != null)
        {
            if (SceneManager.GetActiveScene().name == "play_game")
            {
                bm.gameObject.SetActive(false);
            }
            if (SceneManager.GetActiveScene().name != "play_game")
            {
                bm.gameObject.SetActive(true);
            }
        }


        if (SceneManager.GetActiveScene().name == "Chapter3") // 챕터 3일때 디버프
            if (inputVec.magnitude == 0f)
            {
                idleTimer += Time.deltaTime;
                if (idleTimer >= decreaseInterval_2)
                {
                    damaged_3();
                    idleTimer = 0f;
                }
            }
            else
            {
                idleTimer = 0f;
            }

        timer += Time.deltaTime;

        if (SceneManager.GetActiveScene().name == "Chapter6") // 챕터 6일때 디버프
            if (timer >= decreaseInterval_1)
            {
                damaged_6();
                timer = 0f;
            }

        Hp_8(); // 챕터 8일때 디버프
        Dead();
    }

    void OnCollisionEnter2D(Collision2D collision) //충돌 감지
    {
        if (collision.gameObject.tag == "Floor")
        { //tag가 Floor인 오브젝트와 충돌했을 때
            doublejump = 0; //isJumping을 다시 false로
            speed = 6;
        }
        else if (collision.gameObject.tag == "Floor3")
        {
            doublejump = 0;
            speed = 6;
        }
        else if (collision.gameObject.tag == "Floor5") // 챕터 5일 때 디버프
        {
            doublejump = 0;
            speed = 4;
        }
        else if (collision.gameObject.tag == "Floor8")
        {
            doublejump = 0;
            speed = 6;
        }
        else
        {
            doublejump = 0;
            speed = 6;
        }

        anim.SetBool("isJumping", false); 
    }
    void FixedUpdate()
    {
        if (inputVec.x > 0 && Input.GetKey(KeyCode.RightArrow)) //플레이어 이동
        {
            //rigid.AddForce(Vector2.right * inputVec.x, ForceMode2D.Impulse);
            if (spriter.flipX)
            {
                rigid.velocity = new Vector2(speed, rigid.velocity.y);
                attscoll.transform.position = new Vector2(transform.position.x + 1, transform.position.y);
            }
        }
        if (inputVec.x < 0 && Input.GetKey(KeyCode.LeftArrow))
        {
            //rigid.AddForce(Vector2.right * inputVec.x, ForceMode2D.Impulse);
            if (!spriter.flipX)
            {
                rigid.velocity = new Vector2(-speed, rigid.velocity.y);
                attscoll.transform.position = new Vector2(transform.position.x - 0.55f, transform.position.y);
            }
        }
    }

    void LateUpdate()
    {
        anim.SetFloat("speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x > 0;
        }
    }
    public void OnDamaged() //플레이어가 몬스터에게 맞았을때
    {
        if (gameObject.layer == 6)
        {
            spriter.color = new Color(1, 1, 1, 0.4f);
            gameObject.layer = 8;

            if (enemy != null)
            {
                int dirc = instance.transform.position.x - enemy.transform.position.x > 0 ? 1 : -1;
                rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);
            }
            
            Hp = Hp - enemy.damage;
            hit = true;
            Invoke("OffDamaged", 3f); //무적시간
            GameObject hudText = Instantiate(hudDamageText);
            hudText.transform.position = hudPos.position;
            hudText.GetComponent<DamageText>().damage = enemy.damage;
        }
    }
    public void OnDamaged2() //플레이어가 몬스터에게 맞았을때
    {
        if (gameObject.layer == 6)
        {
            spriter.color = new Color(1, 1, 1, 0.4f);
            gameObject.layer = 8;

            if (fenemy != null)
            {
                int dirc = instance.transform.position.x - fenemy.transform.position.x > 0 ? 1 : -1;
                rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);
            }

            Hp = Hp - fenemy.damage;
            hit = true;
            Invoke("OffDamaged", 3f); //무적시간
            GameObject hudText = Instantiate(hudDamageText);
            hudText.transform.position = hudPos.position;
            hudText.GetComponent<DamageText>().damage = fenemy.damage;
        }
    }
    void OffDamaged() // 무적시간
    {
        spriter.color = new Color(1, 1, 1, 1);
        gameObject.layer = 6;
        hit = false;
    }

    void Dead()
    {
        if (Hp <= 0)
            if (SceneManager.GetActiveScene().name == "game_play")
            {
                instance.transform.position = new Vector2(-0.34f, 1.05f);
                Hp = 100;
            } else if (SceneManager.GetActiveScene().name == "Chapter1") {
                instance.transform.position = new Vector2(-48.3f, 1.05f);
                Hp = 100;
            }
    }
    void Recovery()
    {
        Hp = 100f;
    }

    // 아래 3개의 함수가 디버프에 대한 함수
    void damaged_3()
    {
        Hp -= 10;
    }
    void damaged_6()
    {
        Hp -= 6;
    }

    void Hp_8()
    {
        if (SceneManager.GetActiveScene().name == "Chapter8")
            Hp = 50;
    }
    public void gg()
    {
        SceneManager.LoadScene(DataManager.instance.nowPlayer.nowChapter_name);
    }

    public void ddd()
    {
        smel2.anim.SetBool("skill_2", smel2.isSkill);
        smel2.rigid.velocity = Vector2.zero;
        smel2.rigid.gravityScale = 0;
        smel2.rigid.isKinematic = true;
        smel2.BC2.isTrigger = true; 
    }

 
}
