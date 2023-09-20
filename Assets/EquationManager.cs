using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquationManager : MonoBehaviour
{
    public TextMeshProUGUI equationText;
    public int answer;

    public void GenerateEquation()
    {
        int num1 = Random.Range(0, 5);
        int num2 = Random.Range(0, 5);
        answer = num1 + num2; // for addition
        equationText.text = num1 + " + " + num2 + " = ?";
    }

    void Start()
    {
        GenerateEquation();
    }
    
    public bool CheckAnswer(int value)
    {
        return answer == value;
    }
}
