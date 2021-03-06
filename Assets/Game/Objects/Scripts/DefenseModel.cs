﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseModel : MonoBehaviour
{
    private const int MAX_LIFE = 2000;
    private const int BASE_MAX_LIFE = 7500;

    public int Id;
    public string Name;
    public float Life;

    public PlataformUIData uiData;
    public GameObject prefabExplotion;

    public void ConstructorMainBase()
    {
        this.uiData = this.GetComponent<PlataformUIData>();

        SetLife(BASE_MAX_LIFE);
        this.uiData.SetViewActive(true);
    }

    public void Constructor(int id, string name, PlataformUIData uiData, GameObject prefabExplotion)
    {
        this.Id = id;
        this.Name = name;

        this.prefabExplotion = prefabExplotion;
        this.uiData = uiData;

        SetLife(MAX_LIFE);
        this.uiData.SetViewActive(true);
    }

    private void Update()
    {
        if (!this.uiData.isActiveAndEnabled)
        {
            this.uiData.SetViewActive(true);
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            TakeDamage(50);
        }
    }

    public void SetLife(int life)
    {
        this.Life += life;
        uiData.UpdateInformation(this.Life, 0, this.Id == 5 ? BASE_MAX_LIFE : MAX_LIFE, 0);
        //Debug.Log($"Id: {this.Id} -> Vida: {this.Life}");
    }

    public void TakeDamage(float life)
    {
        this.Life -= life;
        if (IsDead())
        {
            DeathAction();
        }

        uiData.UpdateInformation(this.Life, 0, this.Id == 5 ? BASE_MAX_LIFE : MAX_LIFE, 0);
        //Debug.Log($"Id: {this.Id} -> Vida: {this.Life}");
    }

    private bool IsDead()
    {
        return this.Life <= 0 ? true : false;
    }

    private void DeathAction()
    {
        uiData.SetViewActive(false);
        //Debug.Log($"Id: {this.Id} -> Dead");

        var explotion = Instantiate(prefabExplotion);
        explotion.transform.position = this.transform.position;

        Destroy(this.gameObject);
    }
}
