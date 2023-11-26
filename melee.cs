using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melee : MonoBehaviour
{
    private List<enemyMove> enemiesInRange = new List<enemyMove>(); // �÷��̾� ���� ���� ���� ���͵��� ������ ����Ʈ
    private List<flying_enemy> fenemiesInRange = new List<flying_enemy>(); 

    private play_game player;
    public float damage;
    private void Awake()
    {
       player = FindAnyObjectByType<play_game>();
        
    }
    private void Update()
    {
        damage = player.damage;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // �浹�� ��ü�� "Enemy" �±׸� ������ �ִٸ�
        {
            enemyMove enemy = collision.gameObject.GetComponent<enemyMove>(); // �浹�� ��ü�� enemyMove ������Ʈ�� ������
            if (enemy != null && !enemiesInRange.Contains(enemy)) // ������ enemyMove ������Ʈ�� ��ȿ�ϰ� ����Ʈ�� �̹� ���ԵǾ� ���� �ʴٸ�
            {
                enemiesInRange.Add(enemy); // ���͸� ����Ʈ�� �߰�
                enemy.setDmg(damage); // �ش� ���Ϳ� ���� �˹�� ������ ó��
            }

            flying_enemy fenemy = collision.gameObject.GetComponent<flying_enemy>(); // �浹�� ��ü�� enemyMove ������Ʈ�� ������
            if (fenemy != null && !fenemiesInRange.Contains(fenemy)) // ������ enemyMove ������Ʈ�� ��ȿ�ϰ� ����Ʈ�� �̹� ���ԵǾ� ���� �ʴٸ�
            {
                fenemiesInRange.Add(fenemy); // ���͸� ����Ʈ�� �߰�
                fenemy.setDmg(damage); // �ش� ���Ϳ� ���� �˹�� ������ ó��
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // �浹�� ��ü�� "Enemy" �±׸� ������ �ִٸ�
        {
            enemyMove enemy = collision.gameObject.GetComponent<enemyMove>(); // �浹�� ��ü�� enemyMove ������Ʈ�� ������
            if (enemy != null && enemiesInRange.Contains(enemy)) // ������ enemyMove ������Ʈ�� ��ȿ�ϰ� ����Ʈ�� ���ԵǾ� �ִٸ�
            {
                enemiesInRange.Remove(enemy); // ���͸� ����Ʈ���� ����
            }

            flying_enemy fenemy = collision.gameObject.GetComponent<flying_enemy>(); // �浹�� ��ü�� enemyMove ������Ʈ�� ������
            if (fenemy != null && fenemiesInRange.Contains(fenemy)) // ������ enemyMove ������Ʈ�� ��ȿ�ϰ� ����Ʈ�� ���ԵǾ� �ִٸ�
            {
                fenemiesInRange.Remove(fenemy); // ���͸� ����Ʈ���� ����
            }
        }
    }
}
