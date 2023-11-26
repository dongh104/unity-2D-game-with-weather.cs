using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class elixir : MonoBehaviour
{
    public GameObject img;
    Image imageComponent; // Image ������Ʈ�� ������ ����
    Color originalColor; // ������ �̹��� ����

    public GameObject skill_1, skill_2;
    Image imageComponent1, imageComponent2;
    Color originalColor1, originalColor2;
    void Awake()
    {
        imageComponent = img.GetComponent<Image>(); // Image ������Ʈ�� ������ ������ ����
        originalColor = imageComponent.color; // ������ �̹��� ������ ����

        imageComponent1 = skill_1.GetComponent<Image>();
        originalColor1 = imageComponent1.color;

        imageComponent2 = skill_2.GetComponent<Image>();
        originalColor2 = imageComponent2.color;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            // �̹��� ������ �����Ͽ� ��ο����� ��
            Color darkenAmount = new Color(0.7f, 0.7f, 0.7f); // ��ο��� ���� (0.0f���� 1.0f ������ ��)
            Color darkenedColor = originalColor * darkenAmount;
            imageComponent.color = darkenedColor;
        }
        else
        {
            // ������ �̹��� �������� ����
            imageComponent.color = originalColor;
        }

        if (Input.GetKey(KeyCode.A))
        {
            // �̹��� ������ �����Ͽ� ��ο����� ��
            Color darkenAmount = new Color(0.7f, 0.7f, 0.7f); // ��ο��� ���� (0.0f���� 1.0f ������ ��)
            Color darkenedColor = originalColor1 * darkenAmount;
            imageComponent1.color = darkenedColor;
        }
        else
        {
            // ������ �̹��� �������� ����
            imageComponent1.color = originalColor1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            // �̹��� ������ �����Ͽ� ��ο����� ��
            Color darkenAmount = new Color(0.7f, 0.7f, 0.7f); // ��ο��� ���� (0.0f���� 1.0f ������ ��)
            Color darkenedColor = originalColor2 * darkenAmount;
            imageComponent2.color = darkenedColor;
        }
        else
        {
            // ������ �̹��� �������� ����
            imageComponent2.color = originalColor2;
        }
    }
}
