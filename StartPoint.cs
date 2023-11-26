using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint; // 이동되어온 맵이름을 체크하기 위한 변수
    private play_game thePlayer; // 캐릭터 객체 가져오기 위한 변수
    private Camera_manager theCamera; // 자연스러운 카메라 이동을 위해 가져온 카메라 변수

    public Transform self;

    public int pnum;
    private void Awake()
    {
        self = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        theCamera = FindObjectOfType<Camera_manager>(); // 카메라 변수에 카메라 객체를 할당
        thePlayer = FindObjectOfType<play_game>(); // 캐릭터 변수에 현재 캐릭터 객체를 할당

        if (startPoint == (thePlayer.currentMapName) && pnum == thePlayer.pnum)
        {
            // 카메라 이동
            theCamera.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
            // 캐릭터 이동
            thePlayer.transform.position = new Vector2(self.position.x, 1.007919f);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}