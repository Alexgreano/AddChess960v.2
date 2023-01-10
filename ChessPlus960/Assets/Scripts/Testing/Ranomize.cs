using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ranomize : MonoBehaviour
{
    private char[] pieces = new char[8];


    public void click()
    {
        for(int i = 0; i < pieces.Length; i++)
        {
            pieces[i] = '-';
        }

        RandomizeList();
        PrintList();
    }

    private void PrintList()
    {
        string toPrint = "";
        foreach(char c in pieces)
        {
            toPrint += c;
        }
        Debug.Log(toPrint);
    }

    private void RandomizeList()
    {

        int randNum1;
        int randNum2;
        bool spaceForKing = false;

        ArrayList slots = new ArrayList();

        for (int i = 0; i < 8; i++)
        {
            slots.Add(i);
        }

        //places the first rook
        randNum1 = getRandomNum(0, slots.Count - 1);
        randNum1 = (int)slots[randNum1];
        pieces[randNum1] = 'r';
        slots.Remove(randNum1);

        //places the second rook with at least 1 space for the king in between
        do
        {
            randNum2 = getRandomNum(0, slots.Count - 1);
            randNum2 = (int)slots[randNum2];
            spaceForKing = findAvailableSpace(randNum2);

        } while (pieces[randNum2].Equals('-') == false|| spaceForKing == false);

        pieces[randNum2] = 'r';
        slots.Remove(randNum2);

        //Adds the king in between the two rooks
        if(Mathf.Abs(randNum1 - randNum2) == 2)
        {
            if(randNum1 < randNum2)
            {
                pieces[randNum1 + 1] = 'k';
                slots.Remove(randNum1 + 1);
            }
            else
            {
                pieces[randNum2 + 1] = 'k';
                slots.Remove(randNum2 + 1);
            }
        }
        else
        {
            int randNum3;

            if (randNum1 < randNum2)
            {
                randNum3 = getRandomNum(randNum1 + 1, randNum2 - 1);
            }
            else
            {
                randNum3 = getRandomNum(randNum2 + 1, randNum1 - 1);
            }

            pieces[randNum3] = 'k';
            slots.Remove(randNum3);
        }

        //places the even bishop
        do
        {
            randNum2 = getRandomNum(0, slots.Count - 1);
            randNum2 = (int)slots[randNum2];

        }
        while (randNum2 % 2 == 1 || pieces[randNum2] != '-');

        pieces[randNum2] = 'b';
        slots.Remove(randNum2);

        //adds the odd Bishop
        do
        {
            randNum1 = getRandomNum(0, slots.Count - 1);
            randNum1 = (int)slots[randNum1];
        }
        while (randNum1 % 2 == 0 || pieces[randNum1] != '-');

        pieces[randNum1] = 'b';
        slots.Remove(randNum1);

        //place the final pieces on the board
        List<char> finalPieces = new List<char>();
        char piece = 'o';
        finalPieces.Add('q');
        finalPieces.Add('h');
        finalPieces.Add('h');

        for(int i = 0; i < 2; i++)
        {
            if(slots.Count == 2)
            {
                randNum1 = getRandomNum(0, 1);
                randNum2 = getRandomNum(0, 1);
            }
            else
            {
                randNum1 = getRandomNum(0, 2);
                randNum2 = getRandomNum(0, 2);
            }

            piece = finalPieces[randNum1];
            int temp = (int)slots[randNum2];
            pieces[temp] = piece;
            finalPieces.RemoveAt(randNum1);
            slots.RemoveAt(randNum2);
        }

        pieces[(int)slots[0]] = finalPieces[0];



    }

    private int getRandomNum(int min, int max)
    {
        return Random.Range(min, max);
    }

    private bool findAvailableSpace(int randomNum)
    {
        bool result = false;
        int firstNum = -1;

        for(int i = 0; i < pieces.Length; i++)
        {
            if(pieces[i] == 'r')
            {
                firstNum = i;
            }
        }

        if(firstNum < randomNum)
        {
            firstNum++;
            if(firstNum != randomNum)
            {
                for (int i = firstNum; i < randomNum; i++)
                {
                    if (pieces[i] == '-')
                    {
                        result = true;
                        break;
                    }
                }
            }
        }
        else
        {
            randomNum++;
            if(randomNum != firstNum)
            {
                for (int i = randomNum; i < firstNum; i++)
                {
                    if (pieces[i] == '-')
                    {
                        result = true;
                        break;
                    }
                }
            }
        }

        return result;
    }

}
