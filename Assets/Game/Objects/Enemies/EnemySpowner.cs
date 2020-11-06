using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpowner : MonoBehaviour
{
    public List<GameObject> PrifabEnemies;
    public List<Transform> SpownLocations;
    public Transform EnemyGroup;

    public int Stage { get; set; }
    public int EnemyCount { get; set; }

    private float normalEnemyDamage = 0.5f;
    private float eliteEnemyDamage = 2;
    public float spownTimer;
    
    void Start()
    {
        spownTimer = 0;
        Stage = 1;
        EnemyCount = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyCount > 0)
        {
            switch (Stage)
            {
                case 1: Stage1Logic(); break;
                case 2: Stage2Logic(); break;
                case 3: Stage3Logic(); break;
            }
        }
    }

    private void Stage1Logic()
    {
        // 2 spown origins (index 0 y 2)
        // spown 5 enemigos por origen cada medio seg
        int[] originIndexes = { 0, 2};
        StageSpown(originIndexes);
    }
    
    private void Stage2Logic()
    {
        // 3 spown origins (index 0, 3 y 2)
        // spown 10 enemigos por origen cada medio seg
        // spwon 3 elits
        int[] originIndexes = { 0, 2, 3 };
        StageSpown(originIndexes);
    }

    private void Stage3Logic()
    {
        // 4 spown origins
        // spown 15 enemigos por origen cada medio seg
        // spwon 4 elits
        int[] originIndexes = { 0, 1, 2, 3 };
        StageSpown(originIndexes);
    }

    private void StageSpown(int[] indexes)
    {
        if (spownTimer >= 2)
        {
            spownTimer = 0;
            foreach (var index in indexes)
            {
                Respown(index);
            }
            EnemyCount--;
        }
        spownTimer += Time.deltaTime;
    }

    private void Respown(int originIndex)
    {
        var randomIndex = GetRandomWithProbability();
        var enemy = Instantiate(PrifabEnemies[GetRandomWithProbability()-1], EnemyGroup);

        enemy.transform.position = this.SpownLocations[originIndex].position;
        enemy.AddComponent<EnemyModel>().Constructor(randomIndex, randomIndex == 1 ? "Goblin" : "Troll", randomIndex == 1 ? normalEnemyDamage : eliteEnemyDamage);
        enemy.tag = "Enemy";
    }

    private int GetRandomSpownLocations()
    {
        return Random.Range(0, 4);
    }

    private int GetRandomWithProbability()
    {
        var random = Random.Range(0, 100);

        if (random >= 0 && random < 75)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }
}
