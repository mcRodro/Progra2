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
    public List<Vector3> stackPositions;

    public enum TypeOfPowerUp { 
        BulletBox = 1,
        RepairKit = 2,
        FirstAidKit = 3,
        Nuke = 4
    };

    void Awake() {
        instance = this;  
        powerUps = new Stack();  
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
    }

    /* Aplica el power up activo al arma o pared seleccionados */
    public void ApplyPowerUp(GameObject item) //recibe un modelo de arma o pared y según el power up activo aplica balas o vida
    {
        if (activePowerUp.GetComponent<PowerUpModel>().Id == (int)TypeOfPowerUp.BulletBox && item.GetComponent<WeaponModel>().Type == 1)
        {
            item.GetComponent<WeaponModel>().SetBullet(activePowerUp.GetComponent<PowerUpModel>().Value);
        }
        else if (activePowerUp.GetComponent<PowerUpModel>().Id == (int)TypeOfPowerUp.RepairKit || activePowerUp.GetComponent<PowerUpModel>().Id == (int)TypeOfPowerUp.FirstAidKit)
        {
            item.GetComponent<WeaponModel>().SetLife(activePowerUp.GetComponent<PowerUpModel>().Value);
        }
        else if (activePowerUp.GetComponent<PowerUpModel>().Id == (int)TypeOfPowerUp.Nuke)
        { 
            // nuke everything
        }

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
            if (element != powerUps.Peek() as GameObject)
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
