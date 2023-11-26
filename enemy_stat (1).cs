using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_stat : MonoBehaviour
{
    public enemyMove enemy;
    public imshi stat;

    public new Vector2 newSize;
    private void Awake()
    {
        enemy = FindObjectOfType<enemyMove>();
        stat = FindObjectOfType<imshi>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.gameObject.name == "haribo" || enemy.gameObject.name == "haribo(Clone)")
        {
            if (stat.w == "³¯¾¾ : ºñ" && !enemy.changed)
            {
                enemy.changed = true;
                enemy.anim.SetBool("rain", true);
                enemy.anim.SetInteger("walkSpeed", 0);
                enemy.anim.SetBool("isAtk", false);
                
                newSize = new Vector2(2f, 2f);
                enemy.boxCollider.size = newSize;

            }
            else if (stat.w != "³¯¾¾ : ºñ" && !enemy.changed)
            {

            }
            else if (stat.w == "³¯¾¾ : ºñ" && enemy.changed)
            {
            }
            else if (stat.w != "³¯¾¾ : ºñ" && enemy.changed)
            {
                enemy.changed = false;
                enemy.anim.SetBool("rain", false);
                enemy.anim.SetInteger("walkSpeed", 0);
                enemy.anim.SetBool("isAtk", false);
                
                newSize = new Vector2(1.07f, 1.39f);
                enemy.  boxCollider.size = newSize;
            }
            //1.07, 1.39
        }
    }
}
