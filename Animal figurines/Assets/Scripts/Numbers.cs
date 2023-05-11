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

    int b = 0;
    int a = 0;
    System.Random random = new System.Random((int)DateTime.Now.Ticks);
    int[] randomNumbers;

    private void Start()
    {
        green = new bool[numbers.Length];
        orange = new bool[numbers.Length];
        for (int i = 0; i < numbers.Length; i++) ObjectImageNumber[i].SetActive(false);
        randomizer();

        for (int i = 0; i < numbers.Length; i++)
            Debug.Log(randomNumbers[i]);


    }

    public void buttonNumber(int i)
    {
        if (a == numbers.Length) return;
        for (int g = a; g != numbers.Length; g++)
            if (green[a] == true && a < numbers.Length - 1) { a++; b = a; } // Проверка клетки на true, если true, то пропускать до тех пор, пока не будет клетка false
        if (b != numbers.Length)
        {
            if (orange[a] == true) numbers[a].color = new Color(0, 0, 0, 1);
            if (green[a] != true)
                numbers[a].text = Convert.ToString(i);

            b++;
            Debug.Log(a);
        }
        if (green[a] == false && a != numbers.Length)
            if (a == numbers.Length - 1)
            {
                if (green[a - 1] == false)
                    a++;
            }
            else a++;
        if (green[numbers.Length - 1] == true && numbers.Length - 1 == a)
            for (; green[a] == true; a--) b--;


        Debug.Log(a);
        Debug.Log(b);
    }

    public void backNumber()
    {
        if (a == 0) return;
        a--; b--;
        for (int g = a; g != -1; g--)
            if (green[a] == true && a != 0) { a--; b = a; if (green[a] == false) numbers[a].text = ""; }
            else if (green[a] == false)
            {
                numbers[a].text = "";
                Debug.Log(a);
                Debug.Log(b);
                return;
            }
    }

    public void enterNumbers()
    {
        for (int i = 0; i != numbers.Length; i++) // Проверка на заполненность масива
        {
            if (numbers[i].text == "") return;

                }
        
            for (int i = 0; i != numbers.Length; i++) { orange[i] = false; green[i] = false; }
            for (int i = 0; i != numbers.Length; i++)
            {
                bool TrueOrFalse = false;
                if (randomNumbers[i] == Int32.Parse(numbers[i].text)) green[i] = true;
                else if (green[i] == false)
                {
                    for (int a = 0; a < numbers.Length; a++)
                        if (randomNumbers[a] == Int32.Parse(numbers[i].text))
                            for (int c = 0; c < numbers.Length; c++)
                                if (numbers[i] == numbers[c])
                                    TrueOrFalse = true;
                    if (TrueOrFalse == true)
                        orange[i] = true;
                }
            }
            changeImage();
            for (int i = 0; i != numbers.Length; i++)
            {
                if (green[i] == false && orange[i] == false)
                    numbers[i].text = "";
                if (green[i] == true) numbers[i].color = new Color(0, 0, 0, 1);
                if (orange[i] == true) numbers[i].color = new Color(0, 0, 0, 0.5f);
            }
            b = 0;
            a = 0;
        }
    

    private void changeImage()
    {
        for (int i = 0; i != numbers.Length; i++) ObjectImageNumber[i].SetActive(false);
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
    }

    private void randomizer()
    {
        randomNumbers = new int[numbers.Length];
        for (int i = 0; i != numbers.Length; i++)
            randomNumbers[i] = random.Next(0, 10);
    }

}
