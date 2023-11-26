using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // 몬스터 프리팹
    public GameObject flyingMonsterPrefab;

    public Transform[] spawnPoints; // 몬스터 스폰 지역들

    public int maxSpawnCount; // 최대 몬스터 생성 수
    public int f_max = 1; // 최대 몬스터 생성 수

    public float spawnInterval = 8f; // 몬스터 생성 간격 (초)
    public float f_Interval = 40f; // 몬스터 생성 간격 (초)

    public int currentSpawnCount = 0; // 현재 생성된 몬스터 수
    public int f_current = 0; // 현재 생성된 몬스터 수

    private float spawnTimer = 0f; // 몬스터 생성 타이머
    private float f_Timer = 0f; // 몬스터 생성 타이머

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

            // 랜덤으로 몬스터 수를 결정
            int randomSpawnCount = Random.Range(1, spawnCount + 1);

            for (int i = 0; i < randomSpawnCount; i++)
            {
                int randomIndex = Random.Range(0, availableSpawnPoints.Count);
                Transform spawnPoint = availableSpawnPoints[randomIndex];

                GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                currentSpawnCount++;

                availableSpawnPoints.RemoveAt(randomIndex);

                // 몬스터를 생성한 후, 플레이어 스크립트에 할당
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
        // 플라잉 몬스터를 스폰할 스폰 포인트 선택 (랜덤하게)
        int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomSpawnPointIndex];

        // 필드에 플라잉 몬스터가 없을 경우에만 스폰
        if (f_current == 0)
        {
            // 플라잉 몬스터 프리팹 스폰
            GameObject flyingMonster = Instantiate(flyingMonsterPrefab, spawnPoint.position, spawnPoint.rotation);
            f_current++;

            // 플라잉 몬스터를 생성한 후, 플레이어 스크립트에 할당
            play_game player = FindObjectOfType<play_game>();
            if (player != null)
            {
                flying_enemy enemyMoveScript = flyingMonster.GetComponent<flying_enemy>();
                player.fenemy = enemyMoveScript;
            }
        }
    }
}
