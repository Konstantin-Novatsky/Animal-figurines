using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Numbers : MonoBehaviour
{
    [SerializeField] TMP_Text[] numbers;
    [SerializeField] Button[] enterNumber;

    int a = 0;
    System.Random random = new System.Random();
    int[] randomNumbers;

    private void Start()
    {
        randomizer();
    }

    public void buttonEnterNumber(int i)
    {
        if (a < numbers.Length)
        {
            numbers[a].text = Convert.ToString(i);
            a++;
        }
    }

    public void backNumber()
    {
        if (a > 0)
        {
            a--;
            numbers[a].text = Convert.ToString("");
        }
    }

    public void enterNumbers()
    {
        
    }

    private void randomizer()
    {
        randomNumbers = new int[numbers.Length];
        for (int i = 0; i < numbers.Length; i++)
            randomNumbers[i] = random.Next(0, 10);

    }

    private void comparisons()
    {

        for (int i = 0; i < numbers.Length * 2; i++)
        {
            bool[] green = new bool[numbers.Length];
            bool[] orange = new bool[numbers.Length];
            bool[] red = new bool[numbers.Length];
            if (randomNumbers[i] == Int32.Parse(numbers[i].text)) green[i] = true;


        }


    }

}
