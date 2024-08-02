using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GetRandomNumbers;

public class ButtonUse : MonoBehaviour
{
    [SerializeField] private TMP_Text attemptsText;
    [SerializeField] private Button[] enterNumber;
    [SerializeField] private Image[] imageNumbers;
    [SerializeField] private GameObject[] objectImageNumber;
    [SerializeField] private Sprite[] spriteForImgNum;

    private bool[] greenNumber;
    private bool[] orangeNumber;

    private int stopNumber;
    private int attemptsCount;
    private float color = 1;

    private void Start()
    {
        attemptsCount = int.Parse(attemptsText.text);
        greenNumber = new bool[inputField.Length];
        orangeNumber = new bool[inputField.Length];
    }

    public void NextNumber(int number)
    {
        if (stopNumber >= inputField.Length) return;

        SkipGreenCells();
        if (orangeNumber[stopNumber]) inputField[stopNumber].color = new Color(0, 0, 0, 1);
        if (!greenNumber[stopNumber]) inputField[stopNumber].text = number.ToString();

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
        if (!IsAllFieldsFilled()) return;

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
        for (var i = stopNumber; i < inputField.Length; i++)
        {
            if (greenNumber[stopNumber] && stopNumber < inputField.Length - 1) stopNumber++;
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
                        if (!greenNumber[stopNumber]) inputField[stopNumber].text = "";
                        break;
                    }
                case false:
                    inputField[stopNumber].text = "";
                    return;
            }
        }
    }

    private static bool IsAllFieldsFilled()
    {
        return inputField.All(t => t.text != "");
    }

    private void ResetColors()
    {
        for (var i = 0; i < inputField.Length; i++)
        {
            orangeNumber[i] = false;
            greenNumber[i] = false;
        }
    }

    private void FindGreenCells()
    {
        for (var i = 0; i < inputField.Length; i++)
        {
            if (randomInputNumbers[i] == int.Parse(inputField[i].text))
                greenNumber[i] = true;
        }
    }

    private void FindOrangeCells()
    {
        for (var i = 0; i < inputField.Length; i++)
        {
            if (greenNumber[i]) continue;

            for (var j = 0; j < inputField.Length; j++)
            {
                if (greenNumber[j]) continue;

                if (randomInputNumbers[j] == int.Parse(inputField[i].text))
                    orangeNumber[i] = true;
            }
        }
    }

    private void UpdateImages()
    {
        for (var i = 0; i < inputField.Length; i++)
        {
            objectImageNumber[i].SetActive(false);
        }

        for (var i = 0; i < inputField.Length; i++)
        {
            if (greenNumber[i])
            {
                imageNumbers[i].sprite = spriteForImgNum[0];
                objectImageNumber[i].SetActive(true);
            }
            else if (orangeNumber[i])
            {
                imageNumbers[i].sprite = spriteForImgNum[1];
                objectImageNumber[i].SetActive(true);
            }
        }

        for (var i = 0; i < inputField.Length; i++)
        {
            if (!greenNumber[i] && !orangeNumber[i]) inputField[i].text = "";
            if (orangeNumber[i]) inputField[i].color = new Color(0, 0, 0, 0.5f);
        }
    }

    private void CheckWinOrLose()
    {
        var greenCount = 0;

        for (var i = 0; i < inputField.Length; i++)
        {
            if (greenNumber[i]) greenCount++;
            if (greenCount == inputField.Length)
            {
                attemptsText.text = "Win!";
                attemptsText.color = new Color(0, 1, 0.07144809f, 1);
                return;
            }
        }

        if (attemptsCount == 0)
        {
            attemptsText.text = "Lose";
            stopNumber = inputField.Length;
        }
    }
}
