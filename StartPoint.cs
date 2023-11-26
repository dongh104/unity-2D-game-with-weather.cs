using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint; // �̵��Ǿ�� ���̸��� üũ�ϱ� ���� ����
    private play_game thePlayer; // ĳ���� ��ü �������� ���� ����
    private Camera_manager theCamera; // �ڿ������� ī�޶� �̵��� ���� ������ ī�޶� ����

    public Transform self;

    public int pnum;
    private void Awake()
    {
        self = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        theCamera = FindObjectOfType<Camera_manager>(); // ī�޶� ������ ī�޶� ��ü�� �Ҵ�
        thePlayer = FindObjectOfType<play_game>(); // ĳ���� ������ ���� ĳ���� ��ü�� �Ҵ�

        if (startPoint == (thePlayer.currentMapName) && pnum == thePlayer.pnum)
        {
            // ī�޶� �̵�
            theCamera.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
            // ĳ���� �̵�
            thePlayer.transform.position = new Vector2(self.position.x, 1.007919f);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}