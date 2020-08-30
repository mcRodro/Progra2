using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseModel : MonoBehaviour
{
    private const int MAX_LIFE = 500;
    private const int MAX_BULLETS = 1000;

    public int Id;
    public string Name;
    public int Life;

    public void Constructor(int id, string name)
    {
        this.Id = id;
        this.Name = name;
        SetLife(1000);
    }

    public void SetLife(int life)
    {
        this.Life += life;
        Debug.Log($"Id: {this.Id} -> Vida: {this.Life}");
    }
}
