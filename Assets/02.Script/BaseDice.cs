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

    public int DiceLevel = 1;

    public Collider2D targets;

    public SpriteRenderer SR;

    public Sprite[] LevelSR;

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
        
            //Debug.DrawRay(transform.position, targets.transform.position, Color.blue);
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

    public bool DrawUpgrade(int index)
    {
        switch(index)
        {
            case 2:
                SR.sprite = LevelSR[0];
                return true;
            case 3:
                SR.sprite = LevelSR[1];
                return true;
            case 4:
                SR.sprite = LevelSR[2];
                return true;
            case 5:
                SR.sprite = LevelSR[3];
                return true;
            case 6:
                SR.sprite = LevelSR[4];
                return true;
            case 7:
                SR.sprite = LevelSR[5];
                return true;
            default:
                return false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Distance);
    }

   
}
