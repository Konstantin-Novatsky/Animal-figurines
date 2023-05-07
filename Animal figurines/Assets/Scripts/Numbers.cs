using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Numbers : MonoBehaviour
{
    [SerializeField] TMP_Text[] numbers;
    [SerializeField] Button[] enterNumber;
    [SerializeField] Image[] imageNumbers;
    [SerializeField] GameObject[] ObjectImageNumber;
    [SerializeField] Sprite[] spriteForImgNum;


    bool[] green;
    bool[] orange;
    bool[] red;

    int b = 0;
    int a = 0;
    System.Random random = new System.Random();
    int[] randomNumbers;

    private void Start()
    {
        green = new bool[numbers.Length];
        orange = new bool[numbers.Length];
        red = new bool[numbers.Length];
        for (int i = 0; i < numbers.Length; i++) ObjectImageNumber[i].SetActive(false);
        randomizer();

        //green[0] = true;
        //imageNumbers[0].sprite = spriteForImgNum[0];
        //ObjectImageNumber[0].SetActive(true);
        //green[1] = true;
        //imageNumbers[1].sprite = spriteForImgNum[0];
        //ObjectImageNumber[1].SetActive(true);
        //green[2] = true;
        //imageNumbers[2].sprite = spriteForImgNum[0];
        //ObjectImageNumber[2].SetActive(true);
        //green[3] = true;
        //imageNumbers[3].sprite = spriteForImgNum[0];
        //ObjectImageNumber[3].SetActive(true);
    }

    public void buttonNumber(int i)
    {
        for (int g = 0; g < numbers.Length; g++)
            if (green[a] == true && a < numbers.Length - 1) { a++; b++; }
        if (b != numbers.Length)
        {
            if (orange[a] == true) numbers[a].color = new Color(0, 0, 0, 1);
            if (numbers[a].text == "") numbers[a].text = Convert.ToString(i);
            b++;
        }
        if (green[a] == false && a < numbers.Length - 1)
            if (a == numbers.Length - 1)
            {
                if (green[a - 1] == false)
                    a++;
            }
            else a++;
        if (green[numbers.Length - 1] == true)
            for (; green[a] == true; a--) b--;


        Debug.Log(a);
        Debug.Log(b);
    }

    public void backNumber()
    {
        if (b > numbers.Length - 1) // Конец
        {
            numbers[a].text = "";
            if (green[a] == false && a != 0 && a == b) a--;
            else if (a != 0 && a == b) a -= 2;
            b--;
        }
        else if (b > 0 && b <= numbers.Length - 1) // Начало
        {
            if (green[a] == false && a != 0) a--;
            else if (a != 0) a -= 2;
            numbers[a].text = ""; b--;
        }
        Debug.Log(a);
        Debug.Log(b);
    }

    public void enterNumbers()
    {
        bool TrueOrFalse = false;
        if (b == numbers.Length)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (randomNumbers[i] == Int32.Parse(numbers[i].text)) green[i] = true;
                else if (green[i] == false)
                {
                    for (int a = 0; a < numbers.Length; a++)
                        if (randomNumbers[a] == Int32.Parse(numbers[i].text))
                            for (int c = 0; c < numbers.Length; c++)
                                if (numbers[i] == numbers[c])
                                    TrueOrFalse = true;
                    if (TrueOrFalse == false)
                        orange[i] = true;
                }
                else red[i] = true;
            }
            changeImage();
            for (int i = 0; i < numbers.Length; i++)
            {
                if (green[i] == false && orange[i] == false)
                    numbers[i].text = "";
                if (green[i] == true) numbers[i].color = new Color(0, 0, 0, 1);
                if (orange[i] == true) numbers[i].color = new Color(0, 0, 0, 0.5f);
            }
            b = 0;
            a = 0;
        }
    }

    private void changeImage()
    {
        for (int i = 0; i < numbers.Length; i++)
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
    }

    private void randomizer()
    {
        randomNumbers = new int[numbers.Length];
        for (int i = 0; i < numbers.Length; i++)
            randomNumbers[i] = random.Next(0, 10);
    }

}
