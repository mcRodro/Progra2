using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLogic : MonoBehaviour
{
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
