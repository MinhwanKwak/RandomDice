using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkMouseInput : MonoBehaviourPunCallbacks
{
    RaycastHit hit;
    Vector2 pos;
    bool IsSelectDice = false;
    public Transform CurrentInputTarget;

    PhotonView PV;

    private void Start()
    {
        PV = gameObject.GetComponent<PhotonView>();
    }

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
        else if (Input.GetMouseButtonUp(0))
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
            if (CurrentInputTarget != null)
            {
                if (hit.collider != null && hit.collider.name != CurrentInputTarget.name) // 드래그했는데 또 자신을 드래그하지 않고 그냥한경우
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
                else//드래그 했는데 아무것도 없는경우
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


    public void MixDice(Transform UpgradeTarget, Transform DestroyDice)
    {

        BaseDice UpgradeDice = UpgradeTarget.GetComponent<BaseDice>();
        BaseDice destroydice = DestroyDice.GetComponent<BaseDice>();

        if (UpgradeDice.DiceLevel == destroydice.DiceLevel) //두개의 레벨이 같다면
        {
            UpgradeDice.DiceLevel++;
            bool IsUpgrade = UpgradeDice.DrawUpgrade(UpgradeDice.DiceLevel); //현재 내꺼 업그레이드 
            PV.RPC(nameof(NetworkDiceUpgrade), RpcTarget.OthersBuffered, UpgradeDice.name , UpgradeDice.DiceLevel);
            //다른애꺼 업그레이드 
            if (IsUpgrade)
            {
                GameManager.Instance.CurrentDice.Remove(destroydice.name);
                CurrentInputTarget = null;
                Destroy(destroydice.gameObject);
                PV.RPC(nameof(NetworkDiceDelete), RpcTarget.OthersBuffered, destroydice.name);
            }
        }
        else
        {
            DisConnect();
        }
    }
    [PunRPC]
    public void NetworkDiceUpgrade(string name , int DiceLevel)
    {
        GameObject OBJ;
        GameManager.Instance.NetworkDice.TryGetValue(name, out OBJ);
        BaseDice Dice =  OBJ.GetComponent<BaseDice>();
        if(Dice != null)
        {
            Dice.DrawUpgrade(DiceLevel);
        }

    }

    [PunRPC]
    public void NetworkDiceDelete(string name)
    {   
        GameObject OBJ;
        GameManager.Instance.NetworkDice.TryGetValue(name, out OBJ);
        GameManager.Instance.NetworkDice.Remove(name);
        Destroy(OBJ);
    }
}
