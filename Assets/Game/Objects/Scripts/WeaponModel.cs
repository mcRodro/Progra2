using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModel : MonoBehaviour
{
    private const int MAX_LIFE = 500;
    private const int MAX_BULLETS = 300;

    public int Id;
    public string Name;
    public float Life;
    public int Bullets;
    public PlataformUIData uiData;

    public void Constructor(int id, string name, PlataformUIData uiData)
    {
        this.Id = id;
        this.Name = name;

        this.uiData = uiData;
        uiData.SetViewActive(true);

        switch (id)
        {
            case 1: AddBullet(MAX_BULLETS); AddLife(MAX_LIFE); break;
            case 2: AddBullet(500); AddLife(200); break;
            case 3: AddBullet(1000); AddLife(100); break;
        }
        
        uiData.UpdateInformation(this.Life, this.Bullets, MAX_LIFE, MAX_BULLETS);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TakeDamage(50);
        }
    }

    public void AddLife(int life)
    {
        this.Life += life;
        this.Life = this.Life >= MAX_LIFE ? MAX_LIFE : this.Life;

        uiData.UpdateInformation(this.Life, this.Bullets, MAX_LIFE, MAX_BULLETS);
        Debug.Log($"Id: {this.Id} -> Vida: {this.Life}");
    }
    
    public void TakeDamage(float life)
    {
        this.Life -= life;
        if (IsDead()) 
        {
            DeathAction();
        }

        uiData.UpdateInformation(this.Life, this.Bullets, MAX_LIFE, MAX_BULLETS);
        Debug.Log($"Id: {this.Id} -> Vida: {this.Life}");
    }

    public void AddBullet(int bullets)
    {
        this.Bullets += bullets;
        this.Bullets = this.Bullets >= MAX_BULLETS ? MAX_BULLETS : this.Bullets;

        uiData.UpdateInformation(this.Life, this.Bullets, MAX_LIFE, MAX_BULLETS);
        Debug.Log($"Id: {this.Id} -> Balas: {this.Bullets}");
    }

    public void RemoveBullet(int bullets)
    {
        this.Bullets -= bullets;
        this.Bullets = this.Bullets <= 0 ? 0 : this.Bullets;

        uiData.UpdateInformation(this.Life, this.Bullets, MAX_LIFE, MAX_BULLETS);
        Debug.Log($"Id: {this.Id} -> Balas: {this.Bullets}");
    }

    public bool HasBullets()
    {
        return this.Bullets > 0 ? true : false;
    }

    private bool IsDead() 
    {
        return this.Life <= 0 ? true : false;
    }

    private void DeathAction()
    {
        if (Life <= 0)
        {
            uiData.SetViewActive(false);
            Debug.Log($"Id: {this.Id} -> Dead");
            Destroy(this.gameObject);
            // see to free base ocupation
        }
    }
}
