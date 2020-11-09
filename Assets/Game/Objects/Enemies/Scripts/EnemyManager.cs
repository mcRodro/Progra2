using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    static public EnemyManager instance;

    const int FINAL_NODE = 31;

    private GrafoMA grafoEst;
    private int[] aristas_origen;
    private int[] aristas_destino;
    private int[] aristas_pesos;

    public List<GameObject> nodosGrafo; /* Gameobjects de vertices */
    public List<Vector3> SetAristas; /* Utiliza la pos x para el orgien, la pos y para el distino, y la pos z para el peso */
    public string[] camino; /* Camino encontrado por dijkstra */

    void Awake()
    {
        instance = this;

        // creo la estructura de grafos (estatica)
        grafoEst = new GrafoMA();
        // inicializo TDA
        grafoEst.InicializarGrafo();

        // vector de vértices
        int[] vertices = new int[nodosGrafo.Count];

        // agrego los vértices
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = i + 1;
            grafoEst.AgregarVertice(vertices[i]);
        }

        // vector de aristas - vertices origen
        aristas_origen = new int[SetAristas.Count];
        // vector de aristas - vertices destino
        aristas_destino = new int[SetAristas.Count];
        // vector de aristas - pesos
        aristas_pesos = new int[SetAristas.Count];
        // carga de datos de aristas
        for(int i = 0; i < SetAristas.Count; i++)
        {
            aristas_origen[i] = (int)SetAristas[i].x;
            aristas_destino[i] = (int)SetAristas[i].y;
            aristas_pesos[i] = (int)SetAristas[i].z;
        }

        // agrego las aristas
        Debug.Log("\nAgregando las aristas");
        for (int i = 0; i < aristas_pesos.Length; i++)
        {
            grafoEst.AgregarArista(aristas_origen[i], aristas_destino[i], aristas_pesos[i]);
        }

        Debug.Log("\nListado de Etiquetas de los nodos");
        //for (int i = 0; i < grafoEst.Etiqs.Length; i++)
        //{
        //    if (grafoEst.Etiqs[i] != 0)
        //    {
        //        Debug.Log("Nodo: " + grafoEst.Etiqs[i].ToString());
        //    }
        //}

        Debug.Log("\nListado de Aristas (Inicio, Fin, Peso)");
        for (int i = 0; i < grafoEst.cantNodos; i++)
        {
            for (int j = 0; j < grafoEst.cantNodos; j++)
            {
                if (grafoEst.MAdy[i, j] != 0)
                {
                    // obtengo la etiqueta del nodo origen, que está en las filas (i)
                    int nodoIni = grafoEst.Etiqs[i];
                    // obtengo la etiqueta del nodo destino, que está en las columnas (j)
                    int nodoFin = grafoEst.Etiqs[j];
                    //Debug.Log(nodoIni.ToString() + ", " + nodoFin.ToString() + ", " + grafoEst.MAdy[i, j].ToString());
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AccionCalcularCamino(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AccionCalcularCamino(17);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AccionCalcularCamino(23);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AccionCalcularCamino(10);
        }
    }

    public string[] AccionCalcularCamino(int nodoInicio)
    {
        var origen = nodoInicio;
        var destino = FINAL_NODE;

        // se llema al algoritmo dijkstra
        AlgoDijkstra.Dijkstra(grafoEst, origen);

        // obtener el camino
        var distancia = string.Empty;
        var nodos = string.Empty;

        for (int i = 0; i < grafoEst.cantNodos; ++i)
        {
            if (AlgoDijkstra.distance[i] == int.MaxValue)
            {
                distancia = "---";
            }
            else
            {
                distancia = AlgoDijkstra.distance[i].ToString();
            }

            if (grafoEst.Etiqs[i] == destino)
            {
                nodos = AlgoDijkstra.nodos[i];
                var mensaje = string.Format("Vertice: {0} --x-- Distancia: {1} --x-- Camino: {2}", grafoEst.Etiqs[i], distancia, AlgoDijkstra.nodos[i]);
                Debug.Log(mensaje);
            }
        }

        /* Se preparan los datos para la animación de recorrido del player */
        camino = nodos.Split(',');
        return camino;
    }
}
