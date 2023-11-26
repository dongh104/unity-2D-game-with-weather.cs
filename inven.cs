using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inven : MonoBehaviour
{
    GameManager gm;
    public Image slot;
    public TextMeshProUGUI itemcount;

    private void Awake()
    {
        gm = FindAnyObjectByType<GameManager>();
    }

    void Start()
    {
    }

    void Update()
    {
        // 인벤토리의 첫 번째 아이템을 표시합니다.
        if (gm.getItem.Count > 0)
        {
            slot.color = new Color(255, 255, 255);
            slot.sprite = gm.getItem[0].itemImage;

            // 아이템 획득 수를 텍스트로 표시합니다.
            itemcount.text = gm.getItem.Count.ToString();
        }
        else
        {
            // 아이템이 없을 때 슬롯 이미지와 텍스트를 초기화합니다.
            slot.color = new Color(255, 255, 255, 0);
            slot.sprite = null;
            itemcount.text = "";
        }
    }
}
