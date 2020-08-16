using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModel : MonoBehaviour
{
    private const int MAX_LIFE = 500;
    private const int MAX_BULLETS = 1000;

    public int Id;
    public string Name;
    public int Type; //tipo de objeto: 1-arma, 2-pared
    public int Life;
    public int Bullets;

    public enum ObjectType
    {
        Weapon = 1,
        Wall = 2
    };

    public void SetLife(int life)
    {
        this.Life += life;
        Debug.Log($"Vida: {this.Life}");
    }

    public void SetBullet(int bullets)
    {
        this.Bullets += bullets;
        Debug.Log($"Balas: {this.Bullets}");
    }

    public void PowerUpSelection()
    {
        PowerUpManager.instance.ApplyPowerUp(this.gameObject);
    }
}
