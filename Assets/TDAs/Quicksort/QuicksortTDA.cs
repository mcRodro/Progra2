using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuicksortTDA : MonoBehaviour
{
    private int Partition(List<RankingModel> arr, int left, int right)
    {
        RankingModel pivot;
        int aux = (left + right) / 2;   //tomo el valor central del vector
        pivot = arr[aux];

        // en este ciclo debo dejar todos los valores menores al pivot
        // a la izquierda y los mayores a la derecha
        while (true)
        {
            while (arr[left].ScoreValue < pivot.ScoreValue)
            {
                left++;
            }
            while (arr[right].ScoreValue > pivot.ScoreValue)
            {
                right--;
            }
            if (left < right)
            {
                var temp = arr[right];
                arr[right] = arr[left];
                arr[left] = temp;
            }
            else
            {
                // este es el valor que devuelvo como proxima posicion de
                // la particion en el siguiente paso del algoritmo
                return right;
            }
        }
    }
    public void quickSort(List<RankingModel> arr, int left, int right)
    {
        int pivot;
        if (left < right)
        {
            pivot = Partition(arr, left, right);
            if (pivot > 1)
            {
                // mitad del lado izquierdo del vector
                quickSort(arr, left, pivot - 1);
            }
            if (pivot + 1 < right)
            {
                // mitad del lado derecho del vector
                quickSort(arr, pivot + 1, right);
            }
        }
    }

    public void imprimirVector(List<RankingModel> vec)
    {
        vec.Reverse();
        for (int i = 0; i < vec.Count; i++)
        {
            Debug.Log($"{vec[i].NameValue} - {vec[i].StageValue} - {vec[i].ScoreValue}");
        }
    }
}
