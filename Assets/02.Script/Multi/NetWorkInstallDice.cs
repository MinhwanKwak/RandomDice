using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class NetWorkInstallDice : MonoBehaviourPunCallbacks , IPunObservable
{
    public PhotonView PV;

    [SerializeField]
    InstallInfo[] DicePlane;

    [SerializeField]
    InstallInfo[] AnotherDicePlane;

    private int Index = 0;

    private Player[] players;
    public void Install()
    {
        if (GameManager.Instance.SP >= GameManager.Instance.DiceBuySP)
        {
            GameManager.Instance.SP -= GameManager.Instance.DiceBuySP; //sp Â÷°¨ 

            UIManager.Instance.DrawText();
            while (true)
            {
                int InstallIndex = Random.Range(0, 15);

                if (DicePlane[InstallIndex].IsInstall)
                {
                    continue;
                }
                DicePlane[InstallIndex].IsInstall = true;

                GameObject Dice = GameManager.Instance.RandomDiceInstall();
                //PV.RPC("AnotherInstantiateDice", RpcTarget.AllBuffered, Dice.name, InstallIndex);

                string Clonedelete = Dice.name.Replace("(Clone)", "");

                GameObject MyDice =   PhotonNetwork.Instantiate(Clonedelete, AnotherDicePlane[InstallIndex].InstallTr.position, Quaternion.identity, 0);
                Destroy(MyDice);

                Dice.gameObject.name = Dice.gameObject.name + Index.ToString();
                GameManager.Instance.CurrentDice.Add(Dice.gameObject.name, Dice.gameObject);
                Index++;
                Dice.transform.position = DicePlane[InstallIndex].InstallTr.position;
                break;
            }   
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
