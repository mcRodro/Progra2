﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretLogic : MonoBehaviour
{
    const float ANIMATION_LEFT_LIMIT = -70;
    const float ANIMATION_RIGHT_LIMIT = 70;

    public GameObject weaponHead;
    public GameObject Bullet;
    public bool inAtackRange;

    private WeaponModel model;
    private Transform bulletsHolder;

    private float bulletRespownTimer;
    private float weaponHeadAnimationTimer;
    private float weaponHeadAnimationSpeed;
    private bool weaponHeadAnimationTurn;


    // Start is called before the first frame update
    void Start()
    {
        model = this.GetComponent<WeaponModel>();
        weaponHeadAnimationSpeed = .75f;
        bulletsHolder = GameObject.FindGameObjectWithTag("BulletGroup").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.IsGameOver && !GameManager.instance.GamePause)
        {
            CheckEnemyList();

            if (inAtackRange && model.HasBullets())
            {
                AtackAnimation();
                SpawnBullet();
            }

            if (enemies.Count != 0)
            {
                //var damage = 0.1f * enemies.Count;
                var damage = 0;
                foreach (var enemy in enemies)
                {
                    damage += (int)enemy.GetComponent<EnemyModel>().Damage;
                }
                model.TakeDamage(damage);
            }
        }
    }

    private void AtackAnimation() {
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
        if (bulletRespownTimer >= .1f)
        {
            bulletRespownTimer = 0;
            var bullet = Instantiate(Bullet, bulletsHolder);
            bullet.transform.position = weaponHead.transform.position + new Vector3(0, .75f, 0);
            bullet.transform.eulerAngles = weaponHead.transform.eulerAngles;
            model.RemoveBullet(1);
        }
        else
        {
            bulletRespownTimer += Time.deltaTime;
        }
    }

    public List<GameObject> enemies = new List<GameObject>();

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("Enemy collision enter");
            enemies.Add(collision.gameObject);
            inAtackRange = true;
            //take damage - promedio de enemies
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
