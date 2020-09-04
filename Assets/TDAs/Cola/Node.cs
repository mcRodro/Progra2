using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    /* datos a almacenar, en este caso enteros */
    public GameObject data { get; set; }

    /* Referencia al siguiente nodo */
    public Node next { get; set; }
}
