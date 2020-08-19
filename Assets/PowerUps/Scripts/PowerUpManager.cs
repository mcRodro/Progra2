using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    private const int STACK_LIMIT = 7;

    static public PowerUpManager instance;

    public Stack powerUps;
    public PowerUpModel activePowerUp; // power up seleccionado de pila y activo hasta aplicar en arma o muro
    private Vector3 powerUpActivePosition;
    public List<Vector3> stackPositions;

    public enum PowerUpType { 
        BulletBox = 1,
        RepairKit = 2,
        FirstAidKit = 3,
        Nuke = 4
    };

    void Awake() {
        instance = this;  
        powerUps = new Stack();
        powerUpActivePosition = new Vector3(-425, 250, -1);
    }

    /* Almacena el power up en la pila */
    public void StackPowerUp(GameObject powerUp)
    {
        if (powerUps.Count < STACK_LIMIT)
        {
            powerUps.Push(powerUp);

            powerUp.GetComponent<PowerUpModel>().inStack = true;
            powerUp.transform.localPosition = GetStackPosition();

            UpdateEnableStateOfStackElements();

            Debug.Log($"PowerUps acumulados: {powerUps.Count}");
        }
        else
        {
            Destroy(powerUp);
        }
    }

    /* Setea el último power up en la pila a activo para aplicar */
    public void GetPowerUp()
    {
        var powerUp = powerUps.Pop() as GameObject;
        activePowerUp = powerUp.GetComponent<PowerUpModel>();
        activePowerUp.transform.localPosition = powerUpActivePosition;
    }

    /* Aplica el power up activo al arma o pared seleccionados */
    public void ApplyPowerUp(GameObject item) //recibe un modelo de arma o pared y según el power up activo aplica balas o vida
    {
        if (activePowerUp.GetComponent<PowerUpModel>().Id == (int)PowerUpType.BulletBox && item.GetComponent<WeaponModel>().Type == 1)
        {
            item.GetComponent<WeaponModel>().SetBullet(activePowerUp.GetComponent<PowerUpModel>().Value);
        }
        else if (activePowerUp.GetComponent<PowerUpModel>().Id == (int)PowerUpType.RepairKit || activePowerUp.GetComponent<PowerUpModel>().Id == (int)PowerUpType.FirstAidKit)
        {
            item.GetComponent<WeaponModel>().SetLife(activePowerUp.GetComponent<PowerUpModel>().Value);
        }
        else if (activePowerUp.GetComponent<PowerUpModel>().Id == (int)PowerUpType.Nuke)
        { 
            // nuke everything
        }

        Destroy(activePowerUp.gameObject);
        activePowerUp = null;

        UpdateEnableStateOfStackElements();
        Debug.Log($"PowerUps acumulados: {powerUps.Count}");
    }

    /* Si hay un power up activado destruye el power up sin aplicarlo a un objeto  */
    public void DeleteActivePowerUp()
    {
        Destroy(activePowerUp.gameObject);
        activePowerUp = null;

        UpdateEnableStateOfStackElements();
        Debug.Log($"PowerUps acumulados: {powerUps.Count}");
    }

    /* Posiciona el power up en pantalla según la dimensión de la pila */
    private Vector3 GetStackPosition()
    {
        return stackPositions[powerUps.Count - 1];
    }

    /* Recorre la pila desabilitando botones para dejar solo el último ingresado como activo */
    private void UpdateEnableStateOfStackElements()
    {
        foreach (GameObject element in powerUps)
        {
            if (activePowerUp != null)
            {
                element.GetComponent<Button>().interactable = false;
            }
            else if (activePowerUp == null && element != powerUps.Peek() as GameObject)
            {
                element.GetComponent<Button>().interactable = false;
            }
            else
            {
                element.GetComponent<Button>().interactable = true;
            }
        }
    }
}
