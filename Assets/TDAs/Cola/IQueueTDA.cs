using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQueueTDA
{
    void InitializeQueue();
    void Enqueue(GameObject x);
    void Dequeue();
    bool IsEmpty();
    GameObject Peek();
}
