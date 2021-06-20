using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkEnemy : MonoBehaviour
{
    public int Hp;
    public float Speed = 0f;
    int RaliPointNumber = 0;


    public Text UIHp;

    [HideInInspector]
    public bool IsBossMove = false;


    public bool IsLeftSpawn = false;

    void Start()
    {
        DrawHp();
    }
    private void Update()
    {
        if (!IsBossMove)
        {
            Vector3 direction = (transform.position - NetworkSpawnManager.Instancee.RaliPoint[RaliPointNumber].transform.position).normalized;
            transform.position -= direction * Time.deltaTime * Speed;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RaliPointL") && IsLeftSpawn)
        {
            RaliPointNumber = 2;
        }
        else if (collision.CompareTag("RaliPointR") && !IsLeftSpawn)
        {
            RaliPointNumber = 2;
        }
        else if (collision.CompareTag("RaliPoint"))
        {
            RaliPointNumber++;
        }

        if (collision.CompareTag("EndPoint"))
        {
            Destroy(gameObject);
            UIManager.Instance.HPReduction();
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
            GameManager.Instance.SP += 25;
            UIManager.Instance.DrawText();
            Destroy(gameObject);
        }
    }

    public void DrawHp()
    {
        UIHp.text = Hp.ToString();
    }
}
