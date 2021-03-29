using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModel : MonoBehaviour
{
    private const int MAX_LIFE = 500;
    private const int MAX_BULLETS = 1000;

    public int Id;
    public string Name;
    public int Life;
    public int Bullets;

    public void Constructor(int id, string name)
    {
        this.Id = id;
        this.Name = name;
        switch (id)
        {
            case 1: SetBullet(200); SetLife(100); break;
            case 2: SetBullet(500); SetLife(200); break;
            case 3: SetBullet(1000); SetLife(100); break;
        }
    }

    public void SetLife(int life)
    {
        this.Life += life;
        Debug.Log($"Id: {this.Id} -> Vida: {this.Life}");
    }

    public void SetBullet(int bullets)
    {
        this.Bullets += bullets;
        Debug.Log($"Id: {this.Id} -> Balas: {this.Bullets}");
    }
}
