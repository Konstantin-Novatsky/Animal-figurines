using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetRandomNumbers : MonoBehaviour
{
    [SerializeField] public static TMP_Text[] inputField;

    private System.Random random = new();

    private int[] randomInputNumbers;

    private bool[] isRepeatNumber;
    private bool isEndCycle;

    private void Start()
    {
        randomInputNumbers = new int[inputField.Length];
        isRepeatNumber = new bool[inputField.Length];

        Randomizer();

        for (var i = 0; i < inputField.Length; i++) Debug.Log(randomInputNumbers[i]);
    }

    private void Randomizer()
    {
        GenerateRandomNumbers();

        while (!AreAllNumbersUnique())
        {
            UpdateRepeatedNumbers();
        }
    }

    private void GenerateRandomNumbers()
    {
        for (var i = 0; i < inputField.Length; i++)
        {
            randomInputNumbers[i] = random.Next(0, 10);
        }
    }

    private void UpdateRepeatedNumbers()
    {
        for (var i = 0; i < inputField.Length; i++)
        {
            if (isRepeatNumber[i])
            {
                randomInputNumbers[i] = random.Next(0, 10);
                isRepeatNumber[i] = false;
            }
        }
    }

    private bool AreAllNumbersUnique()
    {
        for (var a = 0; a < inputField.Length; a++)
        {
            for (var b = a + 1; b < inputField.Length; b++)
            {
                if (randomInputNumbers[a] == randomInputNumbers[b])
                {
                    isRepeatNumber[a] = true;
                    isRepeatNumber[b] = true;
                }
            }
        }

        return Array.TrueForAll(isRepeatNumber, value => !value);
    }
}
