using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseLogic : MonoBehaviour
{
    private DefenseModel model;
    public List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        model = this.GetComponent<DefenseModel>();
        if (model.Id == 5)
        {
            model.ConstructorMainBase();
        }
    }
    
    void Update()
    {
        if (!GameManager.instance.IsGameOver && !GameManager.instance.GamePause)
        {
            if (enemies.Count != 0)
            {
                CheckEnemyList();

                //var damage = 0.2f * enemies.Count;
                var damage = 0;
                foreach (var enemy in enemies)
                {
                    damage += (int)enemy.GetComponent<EnemyModel>().Damage;
                }
                model.TakeDamage(damage);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemies.Add(collision.gameObject);
            collision.gameObject.GetComponent<EnemyLogic>().ChangeSpeedToNormal(false);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemies.Remove(collision.gameObject);
            collision.gameObject.GetComponent<EnemyLogic>().ChangeSpeedToNormal(true);
        }
    }

    private void CheckEnemyList()
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }
    }

    void OnDestroy()
    {
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<EnemyLogic>().ChangeSpeedToNormal(true);
        }
    }
}
