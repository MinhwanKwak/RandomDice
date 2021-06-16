using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public SpawnManager spawnManager;

    public int SP;
    public int DiceBuySP;

    public GameObject[] RandomDice;


    public Dictionary<string, GameObject> CurrentEnemy;
    public Dictionary<string, GameObject> CurrentDice;

 

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;  
        }

        CurrentEnemy = new Dictionary<string, GameObject>();
        CurrentDice = new Dictionary<string, GameObject>();
    }



    public GameObject RandomDiceInstall()
    {
         int DiceIndex = Random.Range(0, 4);

       
        GameObject InstantiateDice = Instantiate(RandomDice[DiceIndex]);

        return InstantiateDice;
    }
    

}
