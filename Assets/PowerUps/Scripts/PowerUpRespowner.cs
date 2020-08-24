using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpRespowner : MonoBehaviour
{
    private const int RESPOWN_LEFT_LIMIT = -300;
    private const int RESPOWN_RIGHT_LIMIT = 500;
    private const int RESPOWN_HIGTH = 500;
    private const int RESPOWN_TIME = 2;

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
                Debug.LogWarning("Respown Power up");
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
        var powerUp = Instantiate(prefabs[Random.Range(0, prefabs.Count)], parent);

        //inicializarlo
        powerUp.transform.localPosition = new Vector3(Random.Range(RESPOWN_LEFT_LIMIT, RESPOWN_RIGHT_LIMIT), RESPOWN_HIGTH, -1);
        powerUp.name = powerUp.GetComponent<PowerUpModel>().Name;
    }
}
