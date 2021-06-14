using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDice : MonoBehaviour
{
    public float AttackSpeed;
    public float Damage;

    public float Distance;

    public float FireTime;

    public GameObject Bullet;

    public LayerMask TargetLayer;

    public Collider2D targets;

    private float CurrentRateFire;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindVisibleTargets();
    }
    public void FindVisibleTargets()
    {
        if(targets == null)
        {
            targets = Physics2D.OverlapCircle(transform.position, Distance, TargetLayer);

        }
        if (targets != null)
        {
        
            Debug.DrawRay(transform.position, targets.transform.position, Color.blue);
            Debug.Log(targets.name);
            CurrentRateFire += Time.deltaTime;
            if (CurrentRateFire >= FireTime)
            {
                CurrentRateFire = 0f;
                Attack();
            }
        }
    }

    public void Attack()
    {
        Instantiate(Bullet, transform.position , Quaternion.identity).GetComponent<DiceBullet>().TargetToEnemy(targets.transform);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Distance);
    }
}
