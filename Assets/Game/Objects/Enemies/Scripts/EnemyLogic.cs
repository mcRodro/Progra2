using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    const float NORMAL_MOVEMENT_SPEED = 0.5f;
    const float SLOW_MOVEMENT_SPEED = 0.05f;

    public string[] camino;

    private Transform mainBase;
    private float speed;
    private int currentNode;
    private Transform node;

    private float speedTimer;
    private bool inNormalSpeed;

    void Start()
    {
        this.mainBase = GameObject.FindGameObjectWithTag("MainBase").transform;
        speed = NORMAL_MOVEMENT_SPEED;

        currentNode = 1;
        node = EnemyManager.instance.nodosGrafo[int.Parse(camino[currentNode])-1].transform;
        this.transform.LookAt(node);

        speedTimer = 2;
        inNormalSpeed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainBase && Vector3.Distance(this.transform.position, node.position) > 0.1f)
        {
            this.transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        else
        {
            if (currentNode + 1 < camino.Length)
            {
                currentNode++;
            }

            node = EnemyManager.instance.nodosGrafo[int.Parse(camino[currentNode])-1].transform;
            this.transform.LookAt(node);
        }

        ActionChangeSpeed();
    }

    public void ChangeSpeedToNormal(bool normalSpeed)
    {
        
        //speed = normalSpeed ? NORMAL_MOVEMENT_SPEED : SLOW_MOVEMENT_SPEED;
        
        speedTimer = 0;
        inNormalSpeed = normalSpeed;
    }

    private void ActionChangeSpeed()
    {
        if (speedTimer <= 1)
        {
            speedTimer += Time.deltaTime * .4f;
            speed = 
                inNormalSpeed ?
                Mathf.SmoothStep(SLOW_MOVEMENT_SPEED, NORMAL_MOVEMENT_SPEED, speedTimer):
                Mathf.SmoothStep(NORMAL_MOVEMENT_SPEED, SLOW_MOVEMENT_SPEED, speedTimer);
        }
    }
}
