using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * .1f);
    }

    private void OnTriggerEnter(Collider collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Wall":
                Destroy(this.gameObject);
                break;
            case "Enemy":
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
                break;
        }
    }
}
