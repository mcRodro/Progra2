using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpowner : MonoBehaviour
{
    const int FIRST_SPOWN_TIME = 2;
    const int WAVE_TIME_LAPSE = 20;

    public List<GameObject> PrifabEnemies;
    public List<Transform> SpownLocations;
    private List<int> StarterNodes;
    public Transform EnemyGroup;

    public int Stage { get; set; }
    public int EnemyCount { get; set; }

    private float normalEnemyDamage = 0.5f;
    private float eliteEnemyDamage = 2;
    public float spownTimer;

    private float waveTimer;
    private bool activeWave;
    
    void Start()
    {
        spownTimer = 0;
        Stage = 3;
        EnemyCount = 5;

        StarterNodes = new List<int> {1, 17, 23, 10};
    }

    // Update is called once per frame
    void Update()
    {
        if (activeWave)
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
            else
            {
                activeWave = false;
                waveTimer = 0;
            }
        }
        else
        {
            WaveTimerControl();
        }
    }

    private void Stage1Logic()
    {
        int[] originIndexes = { 0, 2 };
        StageSpown(originIndexes);
    }
    
    private void Stage2Logic()
    {
        int[] originIndexes = { 0, 2, 3 };
        StageSpown(originIndexes);
    }

    private void Stage3Logic()
    {
        int[] originIndexes = { 0, 1, 2, 3 };
        StageSpown(originIndexes);
    }

    private void StageSpown(int[] indexes)
    {
        if (spownTimer >= FIRST_SPOWN_TIME)
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
        enemy.GetComponent<EnemyLogic>().camino = this.GetComponent<EnemyManager>().AccionCalcularCamino(StarterNodes[originIndex]);
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

    private void WaveTimerControl()
    {
        if (waveTimer >= WAVE_TIME_LAPSE)
        {
            EnemyCount = 5;
            activeWave = true;
        }
        else
        {
            waveTimer += Time.deltaTime;
        }
    }
}
