using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stage
{
  public  GameObject[] SpawnEnemy;
  public  float SpawnTimeLimitStart; // ������ ����ϰ� ���� �����Ű���� 
  public  float SpawnTImeLimitEnd;
}

public class SpawnManager : MonoBehaviour
{
    public GameObject[] RaliPoint;
    public Transform SpawnLocation;
    public static SpawnManager Instancee;
    public float NextStageTime;
    [SerializeField]
    public Stage[] Stages;

    [HideInInspector]
    public int CurrentStage;


    private int EnemySpawnIdx;

    private void Start()
    {
        StartCoroutine(StartSpawn());
    }

    //�� ���� 
    public IEnumerator StartSpawn()
    {
        while(CurrentStage != Stages.Length)
        {
            for (int i = 0; i < Stages[CurrentStage].SpawnEnemy.Length; ++i)
            {
                GameObject Enemy = Instantiate(Stages[CurrentStage].SpawnEnemy[i]);
                Enemy.name = Enemy.name + EnemySpawnIdx.ToString();
                GameManager.Instance.CurrentEnemy.Add(Enemy.name, Enemy);
                EnemySpawnIdx++;
                Enemy.transform.position = SpawnLocation.position;
                float RandomRange = Random.Range(Stages[CurrentStage].SpawnTimeLimitStart, Stages[CurrentStage].SpawnTImeLimitEnd);
                yield return new WaitForSeconds(RandomRange);
            }

            yield return new WaitForSeconds(NextStageTime);
            ++CurrentStage;
            UIManager.Instance.WaveText.text = (CurrentStage + 1).ToString();
        }
    }


    private void Awake()
    {
        if(Instancee == null)
        {
            Instancee = this;
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
