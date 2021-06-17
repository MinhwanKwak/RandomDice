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
  
}
