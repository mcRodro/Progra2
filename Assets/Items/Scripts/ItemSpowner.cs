using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpowner : MonoBehaviour
{
    private const int RESPOWN_TIME = 2;

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
        var tempItem = Instantiate(prefabs[Random.Range(0, prefabs.Count)], parent);

        //inicializarlo
        tempItem.transform.localPosition = SpownPosition;
        tempItem.name = tempItem.GetComponent<ItemModel>().Name;

        //Agregar temporalmente a la cola, TODO: animación de aparición
        ItemManager.instance.EnqueueItem(tempItem);
    }
}
