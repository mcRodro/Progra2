using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModel : MonoBehaviour
{
    private const int MAX_TURRET_LIFE = 2000;
    private const int MAX_SHOTGUN_LIFE = 3000;
    private const int MAX_FLAMETHROWER_LIFE = 2500;

    private const int MAX_TURRET_BULLETS = 300;
    private const int MAX_SHOTGUN_BULLETS = 400;
    private const int MAX_FLAMETHROWER_BULLETS = 600;

    public int Id;
    public string Name;
    public float Life;
    public int Bullets;
    private int MaxLife;
    private int MaxBullets;
    public PlataformUIData uiData;
    public GameObject prefabExplotion;

    public void Constructor(int id, string name, PlataformUIData uiData, GameObject prefabExplotion)
    {
        this.Id = id;
        this.Name = name;

        this.prefabExplotion = prefabExplotion;

        this.uiData = uiData;
        uiData.SetViewActive(true);

        switch (id)
        {
            case 1: MaxBullets = MAX_TURRET_BULLETS; MaxLife = MAX_TURRET_LIFE; break;
            case 2: MaxBullets = MAX_SHOTGUN_BULLETS; MaxLife = MAX_SHOTGUN_LIFE; break;
            case 3: MaxBullets = MAX_FLAMETHROWER_BULLETS; MaxLife = MAX_FLAMETHROWER_LIFE; break;
        }

        AddBullet(MaxBullets); 
        AddLife(MaxLife);
        uiData.UpdateInformation(this.Life, this.Bullets, MaxLife, MaxBullets);
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
        this.Life = this.Life >= MaxLife ? MaxLife : this.Life;

        uiData.UpdateInformation(this.Life, this.Bullets, MaxLife, MaxBullets);
        //Debug.Log($"Id: {this.Id} -> Vida: {this.Life}");
    }
    
    public void TakeDamage(float life)
    {
        this.Life -= life;
        if (IsDead()) 
        {
            DeathAction();
        }

        uiData.UpdateInformation(this.Life, this.Bullets, MaxLife, MaxBullets);
        //Debug.Log($"Id: {this.Id} -> Vida: {this.Life}");
    }

    public void AddBullet(int bullets)
    {
        this.Bullets += bullets;
        this.Bullets = this.Bullets >= MaxBullets ? MaxBullets: this.Bullets;

        uiData.UpdateInformation(this.Life, this.Bullets, MaxLife, MaxBullets);
        //Debug.Log($"Id: {this.Id} -> Balas: {this.Bullets}");
    }

    public void RemoveBullet(int bullets)
    {
        this.Bullets -= bullets;
        this.Bullets = this.Bullets <= 0 ? 0 : this.Bullets;

        uiData.UpdateInformation(this.Life, this.Bullets, MaxLife, MaxBullets);
        //Debug.Log($"Id: {this.Id} -> Balas: {this.Bullets}");
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
            //Debug.Log($"Id: {this.Id} -> Dead");
            
            var explotion = Instantiate(prefabExplotion);
            explotion.transform.position = this.transform.position;
            
            Destroy(this.gameObject);
        }
    }
}
