using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyExplotionObject", .75f);
    }

    // Update is called once per frame
    private void DestroyExplotionObject()
    {
        Destroy(this.gameObject);
    }
}
