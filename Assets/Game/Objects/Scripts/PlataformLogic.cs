using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformLogic : MonoBehaviour
{
    public GameObject item;
    public List<GameObject> prefabs;
    public GameObject prefabExplotion;

    public Transform itemsHolder;

    public bool isWeaponBase;
    public bool isDefenseBase;
    public bool isNuclearBase;

    public Color normal;
    public Color mouseOver;
    public Color selected;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Renderer>().material.color = normal;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        this.gameObject.GetComponent<Renderer>().material.color = mouseOver;

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse pressed");
            this.gameObject.GetComponent<Renderer>().material.color = selected;

            if (item == null)
            {
                SpawnItem();
            }
            else if (item != null && PowerUpManager.instance.activePowerUp != null)
            {
                ApplyPowerUp();
            }
        }
    }

    private void OnMouseExit()
    {
        this.gameObject.GetComponent<Renderer>().material.color = normal;
    }

    private void SpawnItem()
    {
        if (ItemManager.instance.activeItem != null && !item)
        {
            var tempItem = ItemManager.instance.activeItem.gameObject;
            var uiData = this.gameObject.GetComponent<PlataformUIData>();
            Debug.Log($"Weapon Id: {tempItem.GetComponent<ItemModel>().Id}, name: {tempItem.GetComponent<ItemModel>().Name}");

            if (isWeaponBase && tempItem.GetComponent<ItemModel>().IsWeapon)
            {
                item = Instantiate(prefabs[tempItem.GetComponent<ItemModel>().Id - 1], itemsHolder);
                item.transform.position = this.transform.position;
                item.transform.eulerAngles = this.transform.eulerAngles;
                item.AddComponent<WeaponModel>().Constructor(
                    tempItem.GetComponent<ItemModel>().Id, 
                    tempItem.GetComponent<ItemModel>().Name,
                    uiData,
                    prefabExplotion
                    );
                ItemManager.instance.DeleteActiveItem();
            }
            else if (isDefenseBase && tempItem.GetComponent<ItemModel>().IsDefense)
            {
                item = Instantiate(prefabs[tempItem.GetComponent<ItemModel>().Id - 1], itemsHolder);
                item.transform.position = new Vector3(this.transform.position.x, -0.25f, this.transform.position.z);
                item.transform.eulerAngles = this.transform.eulerAngles;
                item.AddComponent<DefenseModel>();
                item.GetComponent<DefenseModel>().Constructor(
                    tempItem.GetComponent<ItemModel>().Id, 
                    tempItem.GetComponent<ItemModel>().Name,
                    uiData,
                    prefabExplotion);
                ItemManager.instance.DeleteActiveItem();
            }
        }
    }

    private void ApplyPowerUp()
    {
        if (PowerUpManager.instance.activePowerUp != null)
        {
            var powerup = PowerUpManager.instance.activePowerUp.gameObject;

            switch (powerup.GetComponent<PowerUpModel>().Id) 
            {
                case (int)PowerUpManager.PowerUpType.BulletBox:
                    item.GetComponent<WeaponModel>().AddBullet(AddBulletsByWeaponId());
                    break;
                case (int)PowerUpManager.PowerUpType.RepairKit:
                    item.GetComponent<WeaponModel>().AddLife(powerup.GetComponent<PowerUpModel>().Value);
                    break;
                case (int)PowerUpManager.PowerUpType.FirstAidKit:
                    item.GetComponent<DefenseModel>().SetLife(powerup.GetComponent<PowerUpModel>().Value);
                    break;
                case (int)PowerUpManager.PowerUpType.Nuke:
                    if (isNuclearBase) Debug.LogWarning("Destroy all");
                    break;
            }

            PowerUpManager.instance.DeleteActivePowerUp();
        }
    }

    private int AddBulletsByWeaponId()
    {
        switch (item.GetComponent<WeaponModel>().Id)
        {
            case 1: return 100;
            case 2: return 150;
            case 3: return 300;
        }

        return 0;
    }
}
