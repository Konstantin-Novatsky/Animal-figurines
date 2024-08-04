using JetBrains.Annotations;
using System;
using TMPro;
using UnityEngine;

public class GetRandomNumbers : MonoBehaviour
{
    public static int[] randomInputNumbers;

    [SerializeField][NotNull] private GameObject FolderInputField;

    private int inputFieldCount;

    private System.Random random = new();

    private bool[] isRepeatNumber;

    private bool isEndCycle;

    private void Start()
    {
        inputFieldCount = FolderInputField.transform.childCount;

        randomInputNumbers = new int[inputFieldCount];
        isRepeatNumber = new bool[inputFieldCount];

        Randomizer();

        for (var i = 0; i < inputFieldCount; i++) Debug.Log(randomInputNumbers[i]);
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
        for (var i = 0; i < inputFieldCount; i++)
        {
            randomInputNumbers[i] = random.Next(0, 10);
        }
    }

    private void UpdateRepeatedNumbers()
    {
        for (var i = 0; i < inputFieldCount; i++)
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
        for (var a = 0; a < inputFieldCount; a++)
        {
            for (var b = a + 1; b < inputFieldCount; b++)
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
