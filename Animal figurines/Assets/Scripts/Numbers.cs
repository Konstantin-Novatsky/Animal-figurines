using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static GetRandomNumbers;

public class Numbers : MonoBehaviour
{
    [SerializeField] private TMP_Text attemptsText;
    [SerializeField] private Button[] enterNumber;
    [SerializeField] private Image[] imageNumbers;
    [SerializeField] private GameObject[] ObjectImageNumber;
    [SerializeField] private Sprite[] spriteForImgNum;

    private bool[] greenNumber;
    private bool[] orangeNumber;

    private int StopNumber;
    private int attemptsCount;

    private float color = 1;
    private void Start()
    {
        attemptsCount = int.Parse(attemptsText.text);
        greenNumber = new bool[inputField.Length];
        orangeNumber = new bool[inputField.Length];
    }

    public void buttonNumber(int number)
    {
        if (StopNumber == inputField.Length) return;

        for (var i = StopNumber; i != inputField.Length; i++) if (greenNumber[StopNumber] && StopNumber < inputField.Length - 1) StopNumber++;  // Пропуск через клетки green

        if (orangeNumber[StopNumber]) inputField[StopNumber].color = new Color(0, 0, 0, 1); // При введении числа на orange возвращаем исходную прозрачность
        if (greenNumber[StopNumber] == false) inputField[StopNumber].text = Convert.ToString(number); // Введение числа

        StopNumber++;
    }

    public void backNumber()
    {
        if (StopNumber == 0) return;

        StopNumber--;

        for (var i = StopNumber; i != -1; i--)
            if (greenNumber[StopNumber] && StopNumber != 0) // Пропуск клетки green
            {
                StopNumber--; if (greenNumber[StopNumber] == false) inputField[StopNumber].text = "";
            }
            else if (greenNumber[StopNumber] == false)
            {
                inputField[StopNumber].text = "";
                return;
            }
    }

    public void enterNumbers()
    {
        for (var i = 0; i != inputField.Length; i++) if (inputField[i].text == "") return; // Проверка на заполненность масива

        for (var i = 0; i != inputField.Length; i++) // Обнуление значений
        {
            orangeNumber[i] = false;
            greenNumber[i] = false;
        }

        for (var i = 0; i != inputField.Length; i++) // Нахождение клеток green
        {
            if (randomInputNumbers[i] == int.Parse(inputField[i].text)) greenNumber[i] = true;
        }

        for (var i = 0; i != inputField.Length; i++) // Поиск клеток orange
        {
            for (var a = 0; a != inputField.Length; a++)
            {
                if (greenNumber[a]) continue; // Пропуск клеток green
                if (randomInputNumbers[a] == int.Parse(inputField[i].text)) orangeNumber[i] = true;
            }
        }

        changeImage();

        attemptsCount--;
        attemptsText.text = attemptsCount.ToString();

        color -= 0.2f;

        attemptsText.color = new Color(1, color, color, 1);

        StopNumber = 0;

        var b = 0;
        for (var i = 0; i != inputField.Length; i++) // Объявление победы
        {

            if (greenNumber[i]) b++;
            if (b == 4)
            {
                attemptsText.text = "Win!";
                attemptsText.color = new Color(0, 1, 0.07144809f, 1);
                return;
            }

        }
        if (attemptsCount == 0)
        {
            attemptsText.text = "Lose"; // Объявление поражения
            StopNumber = 4;
        }

    }


    private void changeImage()
    {
        for (var i = 0; i != inputField.Length; i++) ObjectImageNumber[i].SetActive(false); // Обнуление всех изображений

        for (var i = 0; i != inputField.Length; i++)
        {
            if (greenNumber[i])
            {
                imageNumbers[i].sprite = spriteForImgNum[0];
                ObjectImageNumber[i].SetActive(true);
            }
            else if (orangeNumber[i])
            {
                imageNumbers[i].sprite = spriteForImgNum[1];
                ObjectImageNumber[i].SetActive(true);
            }
        }

        for (var i = 0; i != inputField.Length; i++) // Присваивание прозрачности
        {
            if (greenNumber[i] == false && orangeNumber[i] == false) inputField[i].text = "";
            if (orangeNumber[i]) inputField[i].color = new Color(0, 0, 0, 0.5f);
        }

    }
}
