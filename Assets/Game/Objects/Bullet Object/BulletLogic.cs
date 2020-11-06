using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    const int BULLET_DAMAGE = 25;

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
                //Destroy(collision.gameObject);
                collision.gameObject.GetComponent<EnemyModel>().TakeDamage(BULLET_DAMAGE);
                Destroy(this.gameObject);
                break;
        }
    }
}
