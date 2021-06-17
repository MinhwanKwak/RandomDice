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

    private int Index = 0;
    public void Install()
    {
        if(GameManager.Instance.SP >= GameManager.Instance.DiceBuySP)
        {
            GameManager.Instance.SP -= GameManager.Instance.DiceBuySP; //sp Â÷°¨ 

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
                Dice.gameObject.name = Dice.gameObject.name + Index.ToString();
                GameManager.Instance.CurrentDice.Add(Dice.gameObject.name, Dice.gameObject);
                Index++;
                Dice.transform.position = DicePlane[InstallIndex].InstallTr.position;
                break;
            }
        }
    }
}
