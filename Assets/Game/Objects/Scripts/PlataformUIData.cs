using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlataformUIData : MonoBehaviour
{
    public GameObject dataHolder;
    public Image lifeBar;
    public Image bulletsBar;

    void Start()
    {
        SetViewActive(false);
    }

    public void SetViewActive(bool enable)
    {
        dataHolder.SetActive(enable);
    }

    public void UpdateInformation(float life, int bullets, int maxLife, int maxBullets)
    {
        float auxLife = maxLife;
        float auxBullets = maxBullets;

        this.lifeBar.fillAmount = life / auxLife;
        if (this.bulletsBar)
        {
            this.bulletsBar.fillAmount = bullets / auxBullets;
        }
    }
}
