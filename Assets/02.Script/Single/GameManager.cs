using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;

    public SpawnManager spawnManager;
    public NetworkSpawnManager networkSpawnManager;

    public int SP;
    public int DiceBuySP;

    public GameObject[] RandomDice;


    public Dictionary<string, GameObject> CurrentEnemy;
    public Dictionary<string, GameObject> CurrentDice;
    public Dictionary<string, GameObject> NetworkDice;

    public PhotonView PV;

    [HideInInspector]
    public int DiceIndex;

    public int Hp = 3;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;  
        }

        CurrentEnemy = new Dictionary<string, GameObject>();
        CurrentDice = new Dictionary<string, GameObject>();
        NetworkDice = new Dictionary<string, GameObject>();
        PV = gameObject.GetComponent<PhotonView>();
    }



    public GameObject RandomDiceInstall()
    {
        DiceIndex = Random.Range(0, 4);
        GameObject InstantiateDice = Instantiate(RandomDice[DiceIndex]);
        return InstantiateDice;
    }
}
