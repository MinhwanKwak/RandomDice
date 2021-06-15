using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    RaycastHit hit;
    Vector2 pos;
    bool IsSelectDice = false;
    public Transform CurrentInputTarget;
    private void Update()
    {
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
            if (hit.collider != null)
            {
                IsSelectDice = true;
                CurrentInputTarget = hit.transform;
                Debug.Log(hit.transform.name);
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
            if (CurrentInputTarget != null)
            {
                if (hit.collider != null && hit.collider.name != CurrentInputTarget.name) // �巡���ߴµ� �� �ڽ��� �巡������ �ʰ� �׳��� ��� 
                {
                    if (hit.collider.tag == CurrentInputTarget.tag)
                    {
                        Debug.Log(hit.transform.name);
                        MixDice(hit.transform, CurrentInputTarget);
                        IsSelectDice = false;
                    }
                    else
                    {
                        DisConnect();
                    }
                }
                else//�巡�� �ߴµ� �ƹ��͵� ���°��
                {
                    DisConnect();
                }
            }
        }
        SelectDice();
    }


    public void SelectDice()
    {
        if (CurrentInputTarget != null)
        {
            CurrentInputTarget.GetComponent<SpriteRenderer>().enabled = false;
            CurrentInputTarget.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            CurrentInputTarget.GetChild(0).position = new Vector2(pos.x, pos.y);
        }
    }

    public void DisConnect()
    {
        if (CurrentInputTarget != null)
        {
            CurrentInputTarget.GetComponent<SpriteRenderer>().enabled = true;
            CurrentInputTarget.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            IsSelectDice = false;
            CurrentInputTarget = null;
        }
    }
  

    public void  MixDice(Transform UpgradeTarget ,Transform DestroyDice)
    {

        BaseDice UpgradeDice = UpgradeTarget.GetComponent<BaseDice>();
        BaseDice destroydice = DestroyDice.GetComponent<BaseDice>();

        if (UpgradeDice.DiceLevel == destroydice.DiceLevel) //�ΰ��� ������ ���ٸ�
        {
            UpgradeDice.DiceLevel++;
            bool IsUpgrade =  UpgradeDice.DrawUpgrade(UpgradeDice.DiceLevel);
            if (IsUpgrade)
            {
                GameManager.Instance.CurrentDice.Remove(UpgradeDice.name);
                CurrentInputTarget = null;
                Destroy(destroydice.gameObject);
            }
        }
        else
        {
            DisConnect();
        }


    }
}
