using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // ���� ������
    public GameObject flyingMonsterPrefab;

    public Transform[] spawnPoints; // ���� ���� ������

    public int maxSpawnCount; // �ִ� ���� ���� ��
    public int f_max = 1; // �ִ� ���� ���� ��

    public float spawnInterval = 8f; // ���� ���� ���� (��)
    public float f_Interval = 40f; // ���� ���� ���� (��)

    public int currentSpawnCount = 0; // ���� ������ ���� ��
    public int f_current = 0; // ���� ������ ���� ��

    private float spawnTimer = 0f; // ���� ���� Ÿ�̸�
    private float f_Timer = 0f; // ���� ���� Ÿ�̸�

    private void Update()
    {
        if (currentSpawnCount < maxSpawnCount)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnInterval)
            {
                SpawnEnemy();
                spawnTimer = 0f;
            } 
        }
        if (f_current < f_max)
        {
            f_Timer += Time.deltaTime;

            if (f_Timer >= f_Interval)
            {
                SpawnFlyingMonster();
                f_Timer = 0f;
            }
        }
    }

    private void SpawnEnemy()
    {
        int remainingEnemies = maxSpawnCount - currentSpawnCount;

        if (remainingEnemies > 0)
        {
            int spawnCount = Mathf.Min(remainingEnemies, spawnPoints.Length - 1);
            List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);

            // �������� ���� ���� ����
            int randomSpawnCount = Random.Range(1, spawnCount + 1);

            for (int i = 0; i < randomSpawnCount; i++)
            {
                int randomIndex = Random.Range(0, availableSpawnPoints.Count);
                Transform spawnPoint = availableSpawnPoints[randomIndex];

                GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                currentSpawnCount++;

                availableSpawnPoints.RemoveAt(randomIndex);

                // ���͸� ������ ��, �÷��̾� ��ũ��Ʈ�� �Ҵ�
                play_game player = FindObjectOfType<play_game>();
                if (player != null)
                {
                    enemyMove enemyMoveScript = enemy.GetComponent<enemyMove>();
                    player.enemy = enemyMoveScript;
                }
            }
        }
    }
    private void SpawnFlyingMonster()
    {
        // �ö��� ���͸� ������ ���� ����Ʈ ���� (�����ϰ�)
        int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomSpawnPointIndex];

        // �ʵ忡 �ö��� ���Ͱ� ���� ��쿡�� ����
        if (f_current == 0)
        {
            // �ö��� ���� ������ ����
            GameObject flyingMonster = Instantiate(flyingMonsterPrefab, spawnPoint.position, spawnPoint.rotation);
            f_current++;

            // �ö��� ���͸� ������ ��, �÷��̾� ��ũ��Ʈ�� �Ҵ�
            play_game player = FindObjectOfType<play_game>();
            if (player != null)
            {
                flying_enemy enemyMoveScript = flyingMonster.GetComponent<flying_enemy>();
                player.fenemy = enemyMoveScript;
            }
        }
    }
}
