using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GetRandomNumbers;

public class ButtonUse : MonoBehaviour
{
    [SerializeField][NotNull] private TMP_Text[] inputFieldText;
    [SerializeField][NotNull] private TMP_Text attemptsText;
    [SerializeField][NotNull] private Button[] numbersButton;
    [SerializeField][NotNull] private Image[] numbersStatusImage;
    [SerializeField][NotNull] private GameObject[] numbersStatusObject;
    [SerializeField][NotNull] private Sprite spriteGreenStatus;
    [SerializeField][NotNull] private Sprite spriteOrangeStatus;

    [NotNull] private bool[] greenNumber;
    [NotNull] private bool[] orangeNumber;

    private int stopNumber;
    private int attemptsCount;
    private float color = 1;

    private void Start()
    {
        attemptsCount = int.Parse(attemptsText.text);
        greenNumber = new bool[inputFieldText.Length];
        orangeNumber = new bool[inputFieldText.Length];
    }

    public void NextNumber(int number)
    {
        if (stopNumber >= inputFieldText.Length) return;

        SkipGreenCells();
        if (orangeNumber[stopNumber]) inputFieldText[stopNumber].color = new Color(0, 0, 0, 1);
        if (!greenNumber[stopNumber]) inputFieldText[stopNumber].text = number.ToString();

        stopNumber++;
    }

    public void BackNumber()
    {
        if (stopNumber == 0) return;

        stopNumber--;
        SkipGreenCellsBackward();
    }

    public void EnterNumbers()
    {
        if (!IsAllFieldsFilled(inputFieldText)) return;

        ResetColors();

        FindGreenCells();
        FindOrangeCells();

        UpdateImages();

        attemptsCount--;
        attemptsText.text = attemptsCount.ToString();
        color -= 0.2f;
        attemptsText.color = new Color(1, color, color, 1);
        stopNumber = 0;

        CheckWinOrLose();
    }

    private void SkipGreenCells()
    {
        for (var i = stopNumber; i < inputFieldText.Length; i++)
        {
            if (greenNumber[stopNumber] && stopNumber < inputFieldText.Length - 1) stopNumber++;
            else break;
        }
    }

    private void SkipGreenCellsBackward()
    {
        for (var i = stopNumber; i >= 0; i--)
        {
            switch (greenNumber[stopNumber])
            {
                case true when stopNumber > 0:
                    {
                        stopNumber--;
                        if (!greenNumber[stopNumber]) inputFieldText[stopNumber].text = "";
                        break;
                    }
                case false:
                    inputFieldText[stopNumber].text = "";
                    return;
            }
        }
    }

    private static bool IsAllFieldsFilled(IEnumerable<TMP_Text> fields)
    {
        return fields.All(field => !string.IsNullOrEmpty(field.text));
    }

    private void ResetColors()
    {
        for (var i = 0; i < inputFieldText.Length; i++)
        {
            orangeNumber[i] = false;
            greenNumber[i] = false;
        }
    }

    private void FindGreenCells()
    {
        for (var i = 0; i < inputFieldText.Length; i++)
        {
            if (randomInputNumbers[i] == int.Parse(inputFieldText[i].text))
                greenNumber[i] = true;
        }
    }

    private void FindOrangeCells()
    {
        for (var i = 0; i < inputFieldText.Length; i++)
        {
            if (greenNumber[i]) continue;

            for (var j = 0; j < inputFieldText.Length; j++)
            {
                if (greenNumber[j]) continue;

                if (randomInputNumbers[j] == int.Parse(inputFieldText[i].text))
                    orangeNumber[i] = true;
            }
        }
    }

    private void UpdateImages()
    {
        for (var i = 0; i < inputFieldText.Length; i++)
        {
            numbersStatusObject[i].SetActive(false);
        }

        for (var i = 0; i < inputFieldText.Length; i++)
        {
            if (greenNumber[i])
            {
                numbersStatusImage[i].sprite = spriteGreenStatus;
                numbersStatusObject[i].SetActive(true);
            }
            else if (orangeNumber[i])
            {
                numbersStatusImage[i].sprite = spriteOrangeStatus;
                numbersStatusObject[i].SetActive(true);
            }
        }

        for (var i = 0; i < inputFieldText.Length; i++)
        {
            if (!greenNumber[i] && !orangeNumber[i]) inputFieldText[i].text = "";
            if (orangeNumber[i]) inputFieldText[i].color = new Color(0, 0, 0, 0.5f);
        }
    }

    private void CheckWinOrLose()
    {
        var greenCount = 0;

        for (var i = 0; i < inputFieldText.Length; i++)
        {
            if (greenNumber[i]) greenCount++;
            if (greenCount == inputFieldText.Length)
            {
                attemptsText.text = "Win!";
                attemptsText.color = new Color(0, 1, 0.07144809f, 1);
                return;
            }
        }

        if (attemptsCount == 0)
        {
            attemptsText.text = "Lose";
            stopNumber = inputFieldText.Length;
        }
    }
}
