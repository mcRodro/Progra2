using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    const float NORMAL_MOVEMENT_SPEED = 0.5f;
    const float SLOW_MOVEMENT_SPEED = 0.05f;

    private Transform mainBase;
    private float speed;

    void Start()
    {
        this.mainBase = GameObject.FindGameObjectWithTag("MainBase").transform;
        this.transform.LookAt(mainBase);
        speed = NORMAL_MOVEMENT_SPEED;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainBase && Vector3.Distance(this.transform.position, mainBase.position) > 1)
        {
            this.transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }

    public void ChangeSpeedToNormal(bool normalSpeed)
    {
        speed = normalSpeed ? NORMAL_MOVEMENT_SPEED : SLOW_MOVEMENT_SPEED;
    }
}
