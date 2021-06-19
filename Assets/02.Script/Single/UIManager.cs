using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text WaveText;
    public Text CurrentspText;
    public Text DicePrice;
    public static UIManager Instance;

    public GameObject[] Hps;

    public GameObject Lose;
    public GameObject Win;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        DrawText();
    }

    public void DrawText()
    {
        CurrentspText.text = GameManager.Instance.SP.ToString();
        DicePrice.text = GameManager.Instance.DiceBuySP.ToString();
    }

    public void HPReduction()
    {
        --GameManager.Instance.Hp;
        Hps[GameManager.Instance.Hp].SetActive(false);
        if (GameManager.Instance.Hp <= 0)
        {
            EndGame();
        }
    }


    public void EndGame()
    {
        Lose.gameObject.SetActive(true);
    }
}
