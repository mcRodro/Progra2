using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueTF : IQueueTDA
{
    /* Primer elemento de la cola */
    Node first;
    /* Último elemento agregado */
    Node last;

    public void InitializeQueue()
    {
        first = null;
        last = null;
    }

    public void Enqueue(GameObject x)
    {
        /* Creo el nuevo nodo a agregar */
        Node node = new Node();
        node.data = x;
        node.next = null;

        /* Si la cola no esta vacia */
        if (last != null)
        {
            /* Al nodo "último" le asigno como siguiente el nodo "nuevo" */
            last.next = node;
        }

        /* El "último" debe referenciar al "nuevo" que entró */
        last = node;

        /* Si la cola estaba vacia */
        if (first == null)
        {
            /* Si hay un solo nodo, "primero" y "último" hacen referencia al mismo nodo */
            first = last;
        }
    }

    public void Dequeue()
    {
        /* Quitar el primer es hacer que el primero sea el siguiente */
        first = first.next;

        /* Si la cola queda vacia (si primero.siguiente era null) */
        if (first == null)
        {
            last = null;
        }
    }

    public bool IsEmpty()
    {
        return (last == null);
    }

    public Node PeekNode()
    {
        /* Devuelve los datos del primer valor */
        return first;
    }

    public GameObject Peek()
    {
        /* Devuelve los datos del primer valor */
        return first.data;
    }
}
