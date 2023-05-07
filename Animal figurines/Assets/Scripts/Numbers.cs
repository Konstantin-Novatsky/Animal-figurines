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
    }

    public void buttonNumber(int i)
    {
        if (b != numbers.Length)
        {
            numbers[a].text = Convert.ToString(i); b++;
        }
            if (green[a] == false && a != numbers.Length - 1) a ++;
            else if (a < numbers.Length - 2) a+=2;
        //green[3] = true;
        Debug.Log(a);
        Debug.Log(b);
    }

    public void backNumber()
    {
        if (b > numbers.Length - 1) // конец
        {
            numbers[a].text = "";
            if (green[a] == false && a != 0 && a == b) a--;
            else if (a != 0 && a == b) a -= 2;
            b--;
        }
        else if (b > 0 && b <= numbers.Length - 1) // начало
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
        for (int i = 0; i < numbers.Length; i++)
        {
            if (randomNumbers[i] == Int32.Parse(numbers[i].text)) green[i] = true;
            else if (green[i] == false)
            {
                for (int a = 0; a < numbers.Length; a++)
                    if (randomNumbers[a] == Int32.Parse(numbers[i].text)) orange[i] = true;
            }
            else red[i] = true;
        }
        changeImage();
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
        for (int i = 0; i < numbers.Length; i++)
        {
            if (green[i] == false && orange[i] == false)
                numbers[i].text = "";
        }
        a = 0;
    }

    private void randomizer()
    {
        randomNumbers = new int[numbers.Length];
        for (int i = 0; i < numbers.Length; i++)
            randomNumbers[i] = random.Next(0, 10);
    }

}
