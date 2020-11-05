using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunLogic : MonoBehaviour
{
    const float ANIMATION_LEFT_LIMIT = -50;
    const float ANIMATION_RIGHT_LIMIT = 50;

    public GameObject weaponHead;
    public GameObject Bullet;
    public bool inAtackRange;

    private WeaponModel model;
    private Transform bulletsHolder;
    public List<GameObject> enemies = new List<GameObject>();
    public List<Transform> BulletSpownReference;

    private float bulletRespownTimer;
    private float weaponHeadAnimationTimer;
    private float weaponHeadAnimationSpeed;
    private bool weaponHeadAnimationTurn;
    
    void Start()
    {
        model = this.GetComponent<WeaponModel>();
        weaponHeadAnimationSpeed = .25f;
        bulletsHolder = GameObject.FindGameObjectWithTag("BulletGroup").transform;
    }

    void Update()
    {
        if (inAtackRange && model.HasBullets())
        {
            AtackAnimation();
            SpawnBullet();
            CheckEnemyList();
        }

        if (enemies.Count != 0)
        {
            var damage = 0.1f * enemies.Count;
            model.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("Enemy collision enter");
            enemies.Add(collision.gameObject);
            inAtackRange = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("Enemy collision exit");
            enemies.Remove(collision.gameObject);
            if (enemies.Count == 0)
            {
                inAtackRange = false;
            }
        }
    }

    private void AtackAnimation()
    {
        if (weaponHeadAnimationTurn && weaponHeadAnimationTimer >= 1)
        {
            weaponHeadAnimationTurn = !weaponHeadAnimationTurn;
        }
        else if (!weaponHeadAnimationTurn && weaponHeadAnimationTimer <= 0)
        {
            weaponHeadAnimationTurn = !weaponHeadAnimationTurn;
        }
        else
        {
            weaponHeadAnimationTimer = weaponHeadAnimationTurn ? weaponHeadAnimationTimer + Time.deltaTime * weaponHeadAnimationSpeed : weaponHeadAnimationTimer - Time.deltaTime * weaponHeadAnimationSpeed;
            weaponHead.transform.localEulerAngles = new Vector3(0, Mathf.SmoothStep(ANIMATION_RIGHT_LIMIT, ANIMATION_LEFT_LIMIT, weaponHeadAnimationTimer), 0);
        }
    }

    private void SpawnBullet()
    {
        if (bulletRespownTimer >= .5f)
        {
            bulletRespownTimer = 0;
            foreach (var item in BulletSpownReference)
            {
                var bullet = Instantiate(Bullet, bulletsHolder);
                bullet.transform.position = item.position + new Vector3(0, 0, 0);
                bullet.transform.eulerAngles = item.eulerAngles;
                model.RemoveBullet(1);
            }            
        }
        else
        {
            bulletRespownTimer += Time.deltaTime;
        }
    }

    private void CheckEnemyList()
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }

        if (enemies.Count == 0)
        {
            inAtackRange = false;
        }
    }
}
