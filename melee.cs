using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melee : MonoBehaviour
{
    private List<enemyMove> enemiesInRange = new List<enemyMove>(); // 플레이어 공격 범위 내의 몬스터들을 저장할 리스트
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
        if (collision.gameObject.CompareTag("Enemy")) // 충돌한 객체가 "Enemy" 태그를 가지고 있다면
        {
            enemyMove enemy = collision.gameObject.GetComponent<enemyMove>(); // 충돌한 객체의 enemyMove 컴포넌트를 가져옴
            if (enemy != null && !enemiesInRange.Contains(enemy)) // 가져온 enemyMove 컴포넌트가 유효하고 리스트에 이미 포함되어 있지 않다면
            {
                enemiesInRange.Add(enemy); // 몬스터를 리스트에 추가
                enemy.setDmg(damage); // 해당 몬스터에 대해 넉백과 데미지 처리
            }

            flying_enemy fenemy = collision.gameObject.GetComponent<flying_enemy>(); // 충돌한 객체의 enemyMove 컴포넌트를 가져옴
            if (fenemy != null && !fenemiesInRange.Contains(fenemy)) // 가져온 enemyMove 컴포넌트가 유효하고 리스트에 이미 포함되어 있지 않다면
            {
                fenemiesInRange.Add(fenemy); // 몬스터를 리스트에 추가
                fenemy.setDmg(damage); // 해당 몬스터에 대해 넉백과 데미지 처리
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // 충돌한 객체가 "Enemy" 태그를 가지고 있다면
        {
            enemyMove enemy = collision.gameObject.GetComponent<enemyMove>(); // 충돌한 객체의 enemyMove 컴포넌트를 가져옴
            if (enemy != null && enemiesInRange.Contains(enemy)) // 가져온 enemyMove 컴포넌트가 유효하고 리스트에 포함되어 있다면
            {
                enemiesInRange.Remove(enemy); // 몬스터를 리스트에서 제거
            }

            flying_enemy fenemy = collision.gameObject.GetComponent<flying_enemy>(); // 충돌한 객체의 enemyMove 컴포넌트를 가져옴
            if (fenemy != null && fenemiesInRange.Contains(fenemy)) // 가져온 enemyMove 컴포넌트가 유효하고 리스트에 포함되어 있다면
            {
                fenemiesInRange.Remove(fenemy); // 몬스터를 리스트에서 제거
            }
        }
    }
}
