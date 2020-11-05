using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpowner : MonoBehaviour
{
    private const int RESPOWN_TIME = 4;

    public List<GameObject> prefabs;
    public Transform parent;

    private Vector3 SpownPosition;
    private float timer;
    public bool inGame;

    void Start()
    {
        SpownPosition = new Vector3(550, -400, -1);
    }

    void Update()
    {
        if (inGame)
        {
            if (timer >= RESPOWN_TIME)
            {
                Respown();
                //Debug.LogWarning("Respown Item");
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
        var tempItem = Instantiate(prefabs[GetRandomWithProbability()], parent);

        //inicializarlo
        tempItem.transform.localPosition = SpownPosition;
        tempItem.name = tempItem.GetComponent<ItemModel>().Name;

        //Agregar temporalmente a la cola, TODO: animación de aparición
        ItemManager.instance.EnqueueItem(tempItem);
    }

    private int GetRandomWithProbability()
    {
        var random = Random.Range(0, 100);

        if (random > 90 && random <= 100)
        {
            return 1;
        }
        else if (random > 70 && random <= 90)
        {
            return 0;
        }
        else if (random > 35 && random <= 70)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }
}
