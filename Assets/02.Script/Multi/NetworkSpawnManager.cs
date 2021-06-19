using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkSpawnManager : MonoBehaviour
{
    public GameObject[] RaliPoint;
    public Transform SpawnLocation;
    public static NetworkSpawnManager Instancee;
    public float NextStageTime;
    [SerializeField]
    public Stage[] Stages;

    [HideInInspector]
    public int CurrentStage;


    private int EnemySpawnIdx;

    PhotonView PV;

    float RandomRange;

    private void Start()
    {
        PV = gameObject.GetComponent<PhotonView>();
        StartCoroutine(StartSpawn());
    }

    //Àû ½ºÆù 
    public IEnumerator StartSpawn()
    {
        while (CurrentStage != Stages.Length)
        {
            for (int i = 0; i < Stages[CurrentStage].SpawnEnemy.Length; ++i)
            {
                GameObject Enemy = Instantiate(Stages[CurrentStage].SpawnEnemy[i]);
                Enemy.name = Enemy.name + EnemySpawnIdx.ToString();
                GameManager.Instance.CurrentEnemy.Add(Enemy.name, Enemy);
                EnemySpawnIdx++;
                Enemy.transform.position = SpawnLocation.position;
                PV.RPC(nameof(RandomUnitySpawn), RpcTarget.AllBuffered, CurrentStage);
                yield return new WaitForSeconds(RandomRange);
            }
            yield return new WaitForSeconds(NextStageTime);
            ++CurrentStage;
            UIManager.Instance.WaveText.text = (CurrentStage + 1).ToString();
        }
    }
    private void Awake()
    {
        if (Instancee == null)
        {
            Instancee = this;
        }
    }

   [PunRPC]
   public void RandomUnitySpawn(int range)
   {
        RandomRange = Random.Range(Stages[range].SpawnTimeLimitStart, Stages[range].SpawnTImeLimitEnd);
   }
}
