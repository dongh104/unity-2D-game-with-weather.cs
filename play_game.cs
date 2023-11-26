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

    private float curTime; // ���ݸ�� ��Ÿ��
    public float coolTime = 0.2f; // ���� ��Ÿ�� : 0.2��

    private float skill_curTime1; // ��ų��� ��Ÿ��
    public float skill_Cooldown1; // ��ų ��ٿ� �ð� : 3��

    private float skill_curTime2; // ��ų��� ��Ÿ��
    public float skill_Cooldown2; // ��ų ��ٿ� �ð� : 3��


    public int Level = 1;
    public float Hp = 100f;
    public float Exp = 0f;
    public Exp_bar eb;
    public float damage;
    public int EC = 0;

    public string currentMapName; // �÷��̾ ��ġ�� é��, �̵��ϰ� �� é��
    public int pnum;

    static public play_game instance; // �÷��̾� ������Ʈ �ı� �Ǿ����� �޾ƿ� ��ü ����

    public GameObject attscoll; // �÷��̾� ���� ����
    public GameObject s1_attscoll;
    public GameObject s2_attscoll;
    
    public melee mel;
    public skill1_melee smel1;
    public skill2_melee smel2;

    public enemyMove enemy;
    public flying_enemy fenemy;


    private float timer = 0f; //é��6 �����
    private float decreaseInterval_1 = 3f;

    private float idleTimer = 0f; // �������� ���� �ð��� �����ϴ� Ÿ�̸�
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
        if (instance == null) // �÷��̾� é�� �̵� �� �÷��̾� ������Ʈ �ı��Ǵ� ���� ����
        {
            DontDestroyOnLoad(this.gameObject); // ���� ������Ʈ �ı� ����
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        s1_attscoll.SetActive(false);
        s2_attscoll.SetActive(false);
        //transform.position = DataManager.instance.nowPlayer.position;
        //SceneManager.sceneLoaded += OnSceneLoaded; // ���� �ε�� ������ �̺�Ʈ �ڵ鷯 ȣ��
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

        if (Input.GetKeyDown(KeyCode.LeftAlt) && (doublejump < 1)) // cŬ���� ����
        { //���� �߰�
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            doublejump++;

            isJumping = true;
            anim.SetBool("isJumping", isJumping);
        }
        
        if (skill_curTime1 <= 0) // ��ų 1��
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

        if (skill_curTime2 <= 0) // ��ų 2��
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

        if (Input.GetKeyDown(KeyCode.Alpha1)) // ���� Ű���� 1��
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


        if (SceneManager.GetActiveScene().name == "Chapter3") // é�� 3�϶� �����
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

        if (SceneManager.GetActiveScene().name == "Chapter6") // é�� 6�϶� �����
            if (timer >= decreaseInterval_1)
            {
                damaged_6();
                timer = 0f;
            }

        Hp_8(); // é�� 8�϶� �����
        Dead();
    }

    void OnCollisionEnter2D(Collision2D collision) //�浹 ����
    {
        if (collision.gameObject.tag == "Floor")
        { //tag�� Floor�� ������Ʈ�� �浹���� ��
            doublejump = 0; //isJumping�� �ٽ� false��
            speed = 6;
        }
        else if (collision.gameObject.tag == "Floor3")
        {
            doublejump = 0;
            speed = 6;
        }
        else if (collision.gameObject.tag == "Floor5") // é�� 5�� �� �����
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
        if (inputVec.x > 0 && Input.GetKey(KeyCode.RightArrow)) //�÷��̾� �̵�
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
    public void OnDamaged() //�÷��̾ ���Ϳ��� �¾�����
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
            Invoke("OffDamaged", 3f); //�����ð�
            GameObject hudText = Instantiate(hudDamageText);
            hudText.transform.position = hudPos.position;
            hudText.GetComponent<DamageText>().damage = enemy.damage;
        }
    }
    public void OnDamaged2() //�÷��̾ ���Ϳ��� �¾�����
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
            Invoke("OffDamaged", 3f); //�����ð�
            GameObject hudText = Instantiate(hudDamageText);
            hudText.transform.position = hudPos.position;
            hudText.GetComponent<DamageText>().damage = fenemy.damage;
        }
    }
    void OffDamaged() // �����ð�
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

    // �Ʒ� 3���� �Լ��� ������� ���� �Լ�
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
