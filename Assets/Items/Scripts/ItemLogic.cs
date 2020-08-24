using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Selection()
    {
        if (!this.GetComponent<ItemModel>().InQueue) // entonces cayendo
        {
            //guardarlo en la cola
            PowerUpManager.instance.StackPowerUp(this.gameObject);
        }
        else // coloca objeto en mapa
        {
            ItemManager.instance.GetItem();
        }
    }
}
