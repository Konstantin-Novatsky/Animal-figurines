using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Numbers : MonoBehaviour
{
    [SerializeField] TMP_Text[] numbers;
    [SerializeField] TMP_Text attemptsText;
    [SerializeField] Button[] enterNumber;
    [SerializeField] Image[] imageNumbers;
    [SerializeField] GameObject[] ObjectImageNumber;
    [SerializeField] Sprite[] spriteForImgNum;

    bool[] green;
    bool[] orange;

    int a = 0;
    int attempts;

    float color = 1;

    System.Random random = new System.Random();

    int[] randomNumbers;

    bool[] repeat;
    bool returnYes;

    private void Start()
    {
        attempts = Int32.Parse(attemptsText.text);
        randomNumbers = new int[numbers.Length];

        repeat = new bool[numbers.Length];
        green = new bool[numbers.Length];
        orange = new bool[numbers.Length];

        randomizer();

        for (int i = 0; i < numbers.Length; i++)
            Debug.Log(randomNumbers[i]);

    }

    public void buttonNumber(int i)
    {
        if (a == numbers.Length) return;


        for (int g = a; g != numbers.Length; g++)
            if (green[a] == true && a < numbers.Length - 1) a++;  // Пропуск через клетки green

        if (orange[a] == true) numbers[a].color = new Color(0, 0, 0, 1); // При введении числа на orange возвращаем исходную прозрачность
        if (green[a] == false) numbers[a].text = Convert.ToString(i); // Введение числа

        a++;
        Debug.Log(a);
    }

    public void backNumber()
    {
        if (a == 0) return;

        a--;

        for (int g = a; g != -1; g--)
            if (green[a] == true && a != 0) // Пропуск клетки green
            {
                a--; if (green[a] == false) numbers[a].text = "";
            }
            else if (green[a] == false)
            {
                numbers[a].text = "";
                return;
            }
    }

    public void enterNumbers()
    {
        for (int i = 0; i != numbers.Length; i++) if (numbers[i].text == "") return; // Проверка на заполненность масива

        for (int i = 0; i != numbers.Length; i++) // Обнуление значений
        {
            orange[i] = false;
            green[i] = false;
        }

        for (int i = 0; i != numbers.Length; i++) // Нахождение клеток green
        {
            if (randomNumbers[i] == Int32.Parse(numbers[i].text)) green[i] = true;
        }

        for (int i = 0; i != numbers.Length; i++) // Поиск клеток orange
        {
            for (int a = 0; a != numbers.Length; a++)
            {
                if (green[a] == true) continue; // Пропуск клеток green
                if (randomNumbers[a] == Int32.Parse(numbers[i].text)) orange[i] = true;
            }
        }

        changeImage();

        attempts--;
        attemptsText.text = attempts.ToString();

        color -= 0.2f;

        attemptsText.color = new Color(1, color, color, 1);

        a = 0;

        int b = 0;
        for (int i = 0; i != numbers.Length; i++) // Объявление победы
        {

            if (green[i] == true) b++;
            if (b == 4)
            {
                attemptsText.text = "Win!";
                attemptsText.color = new Color(0, 1, 0.07144809f, 1);
                return;
            }

        }
        if (attempts == 0)
        {
            attemptsText.text = "Lose"; // Объявление поражения
            a = 4;
        }

    }


    private void changeImage()
    {
        for (int i = 0; i != numbers.Length; i++) ObjectImageNumber[i].SetActive(false); // Обнуление всех изображений

        for (int i = 0; i != numbers.Length; i++)
        {
            if (green[i] == true)
            {
                imageNumbers[i].sprite = spriteForImgNum[0];
                ObjectImageNumber[i].SetActive(true);
            }
            else if (orange[i] == true)
            {
                imageNumbers[i].sprite = spriteForImgNum[1];
                ObjectImageNumber[i].SetActive(true);
            }
        }

        for (int i = 0; i != numbers.Length; i++) // Присваивание прозрачности
        {
            if (green[i] == false && orange[i] == false) numbers[i].text = "";
            if (orange[i] == true) numbers[i].color = new Color(0, 0, 0, 0.5f);
        }

    }

    private void randomizer()
    {
        for (int i = 0; i != numbers.Length; i++)
            randomNumbers[i] = random.Next(0, 10);
        examinationOne();
    }

    private void examinationOne()
    {

        for (; ; )
        {
            examinationTwo();

            for (int i = 0; i != numbers.Length; i++)
                if (repeat[i] == true)
                {
                    randomNumbers[i] = random.Next(0, 10);
                    repeat[i] = false;
                }
            if (returnYes == true) return;
        }
    }

    private void examinationTwo()
    {
        for (int i = 0; i != numbers.Length; i++)
        {
            for (int k = i + 1; k < numbers.Length; k++)
                if (randomNumbers[i] == randomNumbers[k]) repeat[i] = true;
        }

        for (int i = 0; ;)
        {
            if (repeat[i] == false) i++;
            else break;
            if (i == 4)
            {
                returnYes = true;
                break;
            }
        }
    }
}
