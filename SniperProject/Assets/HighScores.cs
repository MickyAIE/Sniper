using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class HighScores : MonoBehaviour
{
    public int[] scores = new int[10];

    string currentDirectory;

    public string scoreFileName = "highscores.txt";

    // When the game has started it will load the scores and tell the user what the current directory is
    void Start()
    {
        currentDirectory = Application.dataPath;
        Debug.Log("Our current directory is: " + currentDirectory);

        LoadScoresFromFile();
    }

    void Update()
    {

    }

    public void LoadScoresFromFile()
    {

        // finding the file the highscores are saved and is loading them in and reading them
        bool fileExists = File.Exists(currentDirectory + "\\" + scoreFileName);
        if (fileExists == false)
        {
            Debug.Log("The file" + scoreFileName + "cannot be found");
            return;
        }

        scores = new int[scores.Length];

        StreamReader FileReader = new StreamReader(currentDirectory + "\\" + scoreFileName);
        int scoreCount = 0;
        while (FileReader.Peek() != 0 && scoreCount < scores.Length)
        {
            string fileLine = FileReader.ReadLine();
            int readScore = -1;
            bool didParse = int.TryParse(fileLine, out readScore);

            if (didParse)
            {
                scores[scoreCount] = readScore;
            }
            else
            {
                Debug.Log("Invalid line in scores file at" + scoreCount + ", using defualt value", this);
                scores[scoreCount] = 0;
            }
            scoreCount++;
        }
        FileReader.Close();
        Debug.Log("High scores read from" + scoreFileName);

    }

    public void SaveScoresToFile()
    {

        // Saving the scores to the Highsocres file made earlier 
        StreamWriter fileWriter = new StreamWriter(currentDirectory + "\\" + scoreFileName);

        for (int i = 0; i < scores.Length; i++)
        {
            fileWriter.WriteLine(scores[i]);
        }
        fileWriter.Close();
        Debug.Log("High scores written to " + scoreFileName);

    }

    public void Addscore(int newScore)
    {
        // Calculatign whether the score is better than any on the list and whether or 
        // not it will be added to the high scores
        int desiredIndex = -1;
        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i] > newScore || scores[i] == 0)
            {
                desiredIndex = i;
                break;
            }
        }
        if (desiredIndex < 0)
        {
            // just a debug in the consol;e telling you the high didn't get low enough to be added to the list 
            Debug.Log("Score of " + newScore + "not low enough for the high scores list.", this);
            return;
        }
        for (int i = scores.Length - 1; i > desiredIndex; i--)
        {
            scores[i] = scores[i - 1];
        }

        scores[desiredIndex] = newScore;
        /* This is an optional line of code just to let you know the new score has been addded.
        Debug.Log("score of " + newScore + " entered into the high scores at position " + desiredIndex, this);
        */
    }
}

