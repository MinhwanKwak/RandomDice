using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour
{


    public IEnumerator Bossappear()
    {



        yield return new WaitForSeconds(2f);
    }


}
