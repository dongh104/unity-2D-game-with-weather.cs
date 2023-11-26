using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_manager : MonoBehaviour
{
    [SerializeField]
    play_game playerTransform; // 같이 움직일 플레이어 오브젝트를 받아옴
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

    static public Camera_manager instance; // 맵이동시 파괴되는 카메라 오브젝트르 받아올 변수
    private void Awake()
    {
        playerTransform = FindAnyObjectByType<play_game>();
    }
    void Start()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject); // 게임 오브젝트 파괴 금지

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

    void LimitCameraArea() // 카메라가 이동할수 있는 범위를 임의로 지정
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

    private void OnDrawGizmos() // 그것을 빨간색으로 표현
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}
