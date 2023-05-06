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
        if (a != numbers.Length - 1)
        {
            if (green[a] == true) a += 2;
            else a++;
        }
        Debug.Log(b);
        Debug.Log(a);
    }

    public void backNumber()
    {
        if (b != -1)
        {
            numbers[a].text = ""; b--;
        }
        if (green[a] == true && a != 1) a -= 2;
        else if (a != 0) a--;
        Debug.Log(b);
        Debug.Log(a);
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
