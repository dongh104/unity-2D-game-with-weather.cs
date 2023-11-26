using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class elixir : MonoBehaviour
{
    public GameObject img;
    Image imageComponent; // Image 컴포넌트를 저장할 변수
    Color originalColor; // 원래의 이미지 색상

    public GameObject skill_1, skill_2;
    Image imageComponent1, imageComponent2;
    Color originalColor1, originalColor2;
    void Awake()
    {
        imageComponent = img.GetComponent<Image>(); // Image 컴포넌트를 가져와 변수에 저장
        originalColor = imageComponent.color; // 원래의 이미지 색상을 저장

        imageComponent1 = skill_1.GetComponent<Image>();
        originalColor1 = imageComponent1.color;

        imageComponent2 = skill_2.GetComponent<Image>();
        originalColor2 = imageComponent2.color;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            // 이미지 색상을 조정하여 어두워지게 함
            Color darkenAmount = new Color(0.7f, 0.7f, 0.7f); // 어두워질 정도 (0.0f부터 1.0f 사이의 값)
            Color darkenedColor = originalColor * darkenAmount;
            imageComponent.color = darkenedColor;
        }
        else
        {
            // 원래의 이미지 색상으로 복원
            imageComponent.color = originalColor;
        }

        if (Input.GetKey(KeyCode.A))
        {
            // 이미지 색상을 조정하여 어두워지게 함
            Color darkenAmount = new Color(0.7f, 0.7f, 0.7f); // 어두워질 정도 (0.0f부터 1.0f 사이의 값)
            Color darkenedColor = originalColor1 * darkenAmount;
            imageComponent1.color = darkenedColor;
        }
        else
        {
            // 원래의 이미지 색상으로 복원
            imageComponent1.color = originalColor1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            // 이미지 색상을 조정하여 어두워지게 함
            Color darkenAmount = new Color(0.7f, 0.7f, 0.7f); // 어두워질 정도 (0.0f부터 1.0f 사이의 값)
            Color darkenedColor = originalColor2 * darkenAmount;
            imageComponent2.color = darkenedColor;
        }
        else
        {
            // 원래의 이미지 색상으로 복원
            imageComponent2.color = originalColor2;
        }
    }
}
