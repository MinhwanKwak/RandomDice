using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class BossMonster : MonoBehaviour
{
    public Transform BossSpawnSelect;

    public float MoveSpeed;

    public int Hp;
    
    public Text UIHp;

    private int RaliPointNumber;

    private bool IsMoveStart = false;    
    private void Start()
    {
        DrawHp();
        StartCoroutine(Bossappear());
    }

    private void Update()
    {
        if (IsMoveStart)
        {
            Vector3 direction = (transform.position - SpawnManager.Instancee.RaliPoint[RaliPointNumber].transform.position).normalized;
            transform.position -= direction * Time.deltaTime * MoveSpeed;
        }
    }

    public IEnumerator Bossappear()
    {
        foreach (var Dice in GameManager.Instance.CurrentDice.Values)
        {
            if (Dice != null)
            {
                Dice.GetComponent<BaseDice>().IsFire = false;
            }
        }
        foreach (var Targets in GameManager.Instance.CurrentEnemy.Values)
        {
            if (Targets != null)
            {
                Enemy target = Targets.transform.GetComponent<Enemy>();
                if (target != null)
                {
                    target.IsBossMove = true;
                }
            }
        }
        transform.DOMove(GameManager.Instance.spawnManager.RaliPoint[1].transform.position, 2f).OnComplete(()=>{
            foreach (var Targets in GameManager.Instance.CurrentEnemy.Values)
            {
              if (Targets != null)
              {
                  Enemy target = Targets.transform.GetComponent<Enemy>();
                  if (target != null)
                  {
                      target.transform.DOMove(transform.position, 2f).OnComplete(() => { 
                          Destroy(target.gameObject);
                      });
                  }
                }
            }
            FunctionTimer.Create(OnMoveBoss, 2f);
        });


       



        yield return null;
    }

    public void OnMoveBoss()
    {
        transform.DOMove(GameManager.Instance.spawnManager.SpawnLocation.transform.position, 2f).OnComplete(() =>
        {
            foreach (var Dice in GameManager.Instance.CurrentDice.Values)
            {
                if (Dice != null)
                {
                    Dice.GetComponent<BaseDice>().IsFire = true;
                }
            }
            IsMoveStart = true;
            RaliPointNumber = 0;
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RaliPoint"))
        {
            RaliPointNumber++;
        }
        else if (collision.CompareTag("EndPoint"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Bullet"))
        {
            DiceBullet bullet = collision.gameObject.GetComponent<DiceBullet>();
            Hp -= bullet.BulletDamage;
            DrawHp();
            Dead();
        }
    }

    public void Dead()
    {
        if (Hp <= 0)
        {
            GameManager.Instance.CurrentEnemy.Remove(this.name);
            Destroy(gameObject);
        }
    }

    public void DrawHp()
    {
        UIHp.text = Hp.ToString();
    }

}
