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
        if (enemies.Count != 0)
        {
            var damage = 0.2f * enemies.Count;
            model.TakeDamage(damage);

            CheckEnemyList();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemies.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemies.Remove(collision.gameObject);
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
}
