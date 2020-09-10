using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    private const int QUEUE_LIMIT = 7;
    private const int QUEUE_POSITION_DIFERENCESS = 85;

    static public ItemManager instance;

    //public Queue items;
    public QueueTF items;
    public int itemsCount;

    public ItemModel activeItem; // power up seleccionado de pila y activo hasta aplicar en arma o muro
    private Vector3 itemActivePosition;
    public List<Vector3> queuePositions;

    void Awake()
    {
        instance = this;

        items = new QueueTF();
        items.InitializeQueue();
        itemsCount = 0;

        itemActivePosition = new Vector3(425, 265, -1);
        for (int i = 0; i < QUEUE_LIMIT; i++)
        {
            queuePositions.Add(new Vector3(565, queuePositions.Count == 0 ? 260 : queuePositions[i - 1].y - QUEUE_POSITION_DIFERENCESS, -1));
        }
    }

    /* Almacena el power up en la cola */
    public void EnqueueItem(GameObject item)
    {
        if (itemsCount < QUEUE_LIMIT)
        {
            item.GetComponent<Button>().interactable = false;

            items.Enqueue(item); /* AGREGA OBJETO A LA COLA */
            itemsCount++;

            item.GetComponent<ItemModel>().InQueue = true;
            item.transform.localPosition = GetQueuePosition();

            if (!ItemIsActive())
            {
                UpdateInteractableState(true);
            }

            //Debug.Log($"Items acumulados: {items.Count}");
        }
        else
        {
            Destroy(item);
        }
    }

    /* Setea el último power up en la cola a activo para aplicar */
    public void GetItem()
    {
        var tempItem = items.Peek(); /* OBTIENE OBJETO Y REMUEVE DE LA COLA */
        items.Dequeue();
        itemsCount--;

        activeItem = tempItem.GetComponent<ItemModel>();
        activeItem.transform.localPosition = itemActivePosition;

        UpdatePositions();
    }

    /* Si hay un power up activado destruye el power up sin aplicarlo a un objeto  */
    public void DeleteActiveItem()
    {
        if (ItemIsActive())
        {
            Destroy(activeItem.gameObject);
            activeItem = null;

            UpdateInteractableState(true);
            //Debug.Log($"Items acumulados: {items.Count}");
        }
    }

    private bool ItemIsActive()
    {
        return activeItem != null;
    }

    /* Posiciona el power up en pantalla según la dimensión de la pila */
    private Vector3 GetQueuePosition()
    {
        return queuePositions[itemsCount -1];
    }

    /* Recorre la cola actualizando las posiciones de los objetos restantes tras sacar el primer objeto */
    private void UpdatePositions()
    {
        var count = 0;
        var tempNode = items.PeekNode();

        while (tempNode != null)
        {
            tempNode.data.transform.localPosition = queuePositions[count];
            count++;
            tempNode = tempNode.next;
        }
    }

    /* Recorre la cola desabilitando botones para dejar solo el primero ingresado como activo */
    private void UpdateInteractableState(bool interactable)
    {
        if (itemsCount != 0)
        {
            var item = items.Peek() as GameObject; /* OBTIENE OBJETO DE LA COLA SIN REMOVER */
            item.GetComponent<Button>().interactable = interactable;
        }
    }
}
