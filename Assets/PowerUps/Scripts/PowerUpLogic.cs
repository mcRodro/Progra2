using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpLogic : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!this.GetComponent<PowerUpModel>().inStack)
        {
            this.transform.Translate(Vector3.down / 5);
        }
    }

    public void Selection()
    {
        if (!this.GetComponent<PowerUpModel>().inStack) // entonces cayendo
        {
            //guardarlo en la pila
            PowerUpManager.instance.StackPowerUp(this.gameObject);
        }
        else // entonces aplica power up a objeto
        {
            PowerUpManager.instance.GetPowerUp();
        }
    }
}
