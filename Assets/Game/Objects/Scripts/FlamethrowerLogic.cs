using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerLogic : MonoBehaviour
{
    const float ANIMATION_LEFT_LIMIT = -70;
    const float ANIMATION_RIGHT_LIMIT = 70;
    const float WEAOPON_DAMAGE = 0.7f;

    public GameObject weaponHead;
    public GameObject fireEmitter;
    public bool inAtackRange;

    public List<GameObject> enemies = new List<GameObject>();
    private WeaponModel model;

    private float bulletRespownTimer;
    private float weaponHeadAnimationTimer;
    private float weaponHeadAnimationSpeed;
    private bool weaponHeadAnimationTurn;

    void Start()
    {
        model = this.GetComponent<WeaponModel>();
        weaponHeadAnimationSpeed = .75f;
        fireEmitter.SetActive(false);
    }

    void Update()
    {
        CheckEnemyList();

        if (inAtackRange && model.HasBullets())
        {
            fireEmitter.SetActive(true);
            AtackAnimation();
            SpawnBullet();
        }
        else
        {
            fireEmitter.SetActive(false);
        }

        if (enemies.Count != 0)
        {
            //var damage = 0.1f * enemies.Count;
            var damage = 0;
            foreach (var enemy in enemies)
            {
                if (enemy)
                {
                    damage += (int)enemy.GetComponent<EnemyModel>().Damage;
                    enemy.GetComponent<EnemyModel>().TakeDamage(WEAOPON_DAMAGE);
                }
            }
            model.TakeDamage(damage);
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

    private void SpawnBullet()
    {
        if (bulletRespownTimer >= 1f)
        {
            model.RemoveBullet(1);
        }
        else
        {
            bulletRespownTimer += Time.deltaTime;
        }
    }
}
