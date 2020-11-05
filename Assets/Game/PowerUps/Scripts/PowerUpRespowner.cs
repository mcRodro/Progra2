using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpRespowner : MonoBehaviour
{
    private const int RESPOWN_LEFT_LIMIT = -300;
    private const int RESPOWN_RIGHT_LIMIT = 300;
    private const int RESPOWN_HIGTH = 500;
    private const int RESPOWN_TIME = 4;

    public List<GameObject> prefabs;
    public Transform parent;

    private float timer;
    public bool inGame;

    
    void Update()
    {
        if(inGame)
        {
            if(timer >= RESPOWN_TIME)
            {
                Respown();
                //Debug.LogWarning("Respown Power up");
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    private void Respown()
    {
        //instanciar objeto
        var powerUp = Instantiate(prefabs[GetRandomWithProbability()], parent);

        //inicializarlo
        powerUp.transform.localPosition = new Vector3(Random.Range(RESPOWN_LEFT_LIMIT, RESPOWN_RIGHT_LIMIT), RESPOWN_HIGTH, -1);
        powerUp.name = powerUp.GetComponent<PowerUpModel>().Name;
    }

    private int GetRandomWithProbability() 
    {
        var random = Random.Range(0, 100);

        if (random > 95 && random <= 100)
        {
            return 3;
        }
        else if (random > 70 && random <= 95)
        {
            return 1;
        }
        else if (random > 40 && random <= 70)
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }
}
