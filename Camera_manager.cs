using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_manager : MonoBehaviour
{
    [SerializeField]
    play_game playerTransform; // ���� ������ �÷��̾� ������Ʈ�� �޾ƿ�
    [SerializeField]
    Vector3 cameraPosition;

    [SerializeField]
    Vector2 center;
    [SerializeField]
    Vector2 mapSize;

    [SerializeField]
    float cameraMoveSpeed;
    float height;
    float width;

    static public Camera_manager instance; // ���̵��� �ı��Ǵ� ī�޶� ������Ʈ�� �޾ƿ� ����
    private void Awake()
    {
        playerTransform = FindAnyObjectByType<play_game>();
    }
    void Start()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject); // ���� ������Ʈ �ı� ����

            height = Camera.main.orthographicSize;
            width = height * Screen.width / Screen.height;

            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        
        //Transform transform1 = GameObject.Find("Player").GetComponent<Transform>();
        //playerTransform = transform1; 
    }

    void FixedUpdate()
    {
        LimitCameraArea();
    }

    void LimitCameraArea() // ī�޶� �̵��Ҽ� �ִ� ������ ���Ƿ� ����
    {
        transform.position = Vector3.Lerp(transform.position,
                                          playerTransform.transform.position + cameraPosition,
                                          Time.deltaTime * cameraMoveSpeed);
        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }

    private void OnDrawGizmos() // �װ��� ���������� ǥ��
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}
