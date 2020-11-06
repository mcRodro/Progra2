using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    const float MAX_NORMAL_LIFE = 100;
    const float MAX_ELITE_LIFE = 300;

    public int Id { get; private set; }
    public string Name { get; private set; }
    public float Life { get; private set; }
    public float Damage { get; private set; }
    public bool IsDead => this.Life <= 0 ? true : false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Constructor(int id, string name, float damage)
    {
        this.Id = id;
        this.Name = name;
        this.gameObject.name = name;
        this.Life = id == 1 ? MAX_NORMAL_LIFE : id == 2 ? MAX_ELITE_LIFE : 0;
        this.Damage = damage;
    }

    public void TakeDamage(float damage)
    {
        this.Life -= damage;
        if (IsDead) 
        {
            DeathAction();
        }
    }

    public void DeathAction()
    {
        Destroy(this.gameObject);
    }
}
