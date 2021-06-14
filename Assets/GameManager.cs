using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int SP;
    public int DiceBuySP;

    public GameObject[] RandomDice;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;  
        }
    }


    public GameObject RandomDiceInstall()
    {
         int DiceIndex = Random.Range(0, 4);


        GameObject InstantiateDice = Instantiate(RandomDice[DiceIndex]);

        return InstantiateDice;
    }
    

}
