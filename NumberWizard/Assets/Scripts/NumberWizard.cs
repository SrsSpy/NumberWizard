using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberWizard : MonoBehaviour
{
    [SerializeField]
    private int initialLowerEnd = 0;
    [SerializeField]
    private int initialHigherEnd = 1000;

    private int currentLowerEnd;
    private int currentHigherEnd;
    private int currentGuess;
    private int targetNumber;
    private bool inputUp;
    private bool inputDown;
    private bool inputEnter;
    private bool ended = false;
    private int guessCounter = 0;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        UpdateInput();
        if (inputDown || inputUp || inputEnter)
        {
            if (ended) Initialize();
            else
            {
                ReactToInput();
                if (!ended) Guess();
            }
        }
    }

    private void Initialize()
    {
        guessCounter = 0;
        ended = false;
        currentHigherEnd = initialHigherEnd+1;
        currentLowerEnd = initialLowerEnd;
        currentGuess = int.MinValue;
        Guess();
    }

    private void Guess(bool debug = false)
    {
        currentGuess = CalculateNextNumber();

        if (currentHigherEnd - currentLowerEnd <= 2) // fix 501 later
        {
            PrintEarlySuccess();
            GameOver();
            return;
        }

        guessCounter++;
        PrintQuestion();

        if (debug) Debug.Log(currentGuess + " [" + currentLowerEnd + "," + currentHigherEnd + "] ");
    }

    private int CalculateNextNumber()
    {
        int newGuess = (currentHigherEnd + currentLowerEnd) / 2;
        return newGuess;
    }

    private void ReactToInput()
    {
        if (inputEnter)
        {
            PrintSuccess();
            GameOver();
        }
        else if (inputDown) currentHigherEnd = currentGuess;
        else if (inputUp) currentLowerEnd = currentGuess;
    }

    private void UpdateInput()
    {
        inputEnter = Input.GetKeyDown(KeyCode.Return);
        inputUp = Input.GetKeyDown(KeyCode.UpArrow);
        inputDown = Input.GetKeyDown(KeyCode.DownArrow);
    }

    private void GameOver()
    {
        ended = true;
        Debug.Log("Wcisnij dowolny przycisk aby zacząc od nowa.");
    }

    private void PrintQuestion()
    {
        Debug.Log("Czy " + currentGuess + " to twoja liczba? " + "Wciśnij Enter jeżeli tak. " + "Strzałka w górę jeśli jest większa. " + "Strzałka w dół jeśli jest mniejsza. ");
    }

    private void PrintSuccess()
    {
        Debug.Log(currentGuess + " to piękna liczba! Wystarczyło " + guessCounter + " pytań!");
    }

    private void PrintEarlySuccess()
    {
        Debug.Log("Nie już ma innych mozliwości! Twoją liczbą musi być " + currentGuess + ". Zadane zostało " + guessCounter + " pytań!");
    }
}
