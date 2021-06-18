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


    private int NetworkIndex = 0;

    private GameObject MyDice;
    private GameObject Dice;

    private Player[] players;


    private int DiceIndex = 0;

    private int NumberingIndex = 0;
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


                PV.RPC(nameof(RandomIndex), RpcTarget.AllBuffered);

                Dice = RandomDiceInstall();

                PV.RPC(nameof(DiceInstantiate), RpcTarget.OthersBuffered, InstallIndex, DiceIndex);

                GameManager.Instance.CurrentDice.Add(Dice.gameObject.name, Dice.gameObject);
                Dice.transform.position = DicePlane[InstallIndex].InstallTr.position;
                break;
            }   
        }
    }
    [PunRPC]
    public void DiceInstantiate(int InstallIndex , int Index)
    {
            MyDice = Instantiate(GameManager.Instance.RandomDice[Index], AnotherDicePlane[InstallIndex].InstallTr.position, Quaternion.identity);

            MyDice.name = MyDice + NetworkIndex.ToString();
            ++NetworkIndex;

            GameManager.Instance.NetworkDice.Add(MyDice.name, MyDice);  
    }
    [PunRPC]
    public void RandomIndex()
    {
        DiceIndex = Random.Range(0, 4);
    }



    public GameObject RandomDiceInstall()
    {
        GameObject InstantiateDice = Instantiate(GameManager.Instance.RandomDice[DiceIndex]);
        InstantiateDice.name = InstantiateDice.ToString() + NumberingIndex.ToString();
        NumberingIndex++;
        return InstantiateDice;
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
       if(stream.IsWriting)
       {
            stream.SendNext(DiceIndex);
       }
       else
       {
            this.DiceIndex = (int)stream.ReceiveNext();
       }
    }
}
