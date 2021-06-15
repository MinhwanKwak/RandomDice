using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBullet : MonoBehaviour
{
    public float BulletSpeed;
    public int BulletDamage;

    public GameObject Target;

    private Vector3 Targetpos;

    private void Update()
    {
        if (Target != null)
        {
            Vector3  Direction = (transform.position - Targetpos).normalized;
            transform.position -= Direction * Time.deltaTime * BulletSpeed;
        }
        else
        {
            Destroy(gameObject);
        }
    }
   public void TargetToEnemy(Transform targetpos)
    {
        Targetpos = targetpos.position;
        Target = targetpos.gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
  
}
