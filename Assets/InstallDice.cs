using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InstallInfo
{ 
     public bool IsInstall = false;
     public Transform InstallTr;
}


public class InstallDice : MonoBehaviour
{
    [SerializeField]
    InstallInfo[] DicePlane;



    public void Install()
    {
        if(GameManager.Instance.SP >= GameManager.Instance.DiceBuySP)
        {
            GameManager.Instance.SP -= GameManager.Instance.DiceBuySP; //sp ���� 

            UIManager.Instance.DrawText();
            while(true)
            {
                int InstallIndex = Random.Range(0, 15);
                
                if(DicePlane[InstallIndex].IsInstall)
                {
                    continue;
                }
                DicePlane[InstallIndex].IsInstall = true;
                GameObject Dice =  GameManager.Instance.RandomDiceInstall();
                Dice.transform.position = DicePlane[InstallIndex].InstallTr.position;
                break;
            }
        }

    }
}
