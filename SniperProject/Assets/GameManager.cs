using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Referencing the HighScores
    public HighScores m_HighScores;

    // Referencing all the overlays and text within the game
    public Text m_MessageText;
    public Text m_TimerText;
    public Text fpsText;
    public Text m_HighScoresText;

    
    public GameObject m_HighScorePanel;
    public Button m_NewGameButton;
    public Button m_HighScoresButton;

    private int fpsCount = 0;
    private float fpsTimer = 0;

    public GameObject[] m_Characters;
    private float m_gameTime = 0;
    public float GameTime { get { return m_gameTime; } }

    public enum GameState
    {
        Start,
        Playing,
        GameOver
    };
    private GameState m_GameState;
    public GameState State { get { return m_GameState; } }

    private void Awake()
    {
        Application.targetFrameRate = 300;
        m_GameState = GameState.Start;
    }

    private bool OneTankLeft()
    {
        int numTanksLeft = 0;
        for (int i = 0; i < m_Characters.Length; i++)
        {
            if (m_Characters[i].activeSelf == true)
            {
                numTanksLeft++;
            }
        }
        return numTanksLeft <= 1;
    }

    private bool IsPlayerDead()
    {
        for (int i = 0; i < m_Characters.Length; i++)
        {
            if (m_Characters[i].activeSelf == false)
            {
                if (m_Characters[i].tag == "player")
                    return true;
            }
        }
        return false;
    }


    private void Start()
    {
        for (int i = 0; i < m_Characters.Length; i++)
        {
            m_Characters[i].SetActive(false);
        }
        m_TimerText.gameObject.SetActive(false);
        m_MessageText.text = "PRESS 'ENTER' TO BEGIN THE MISSION";

        m_HighScorePanel.gameObject.SetActive(false);
        m_NewGameButton.gameObject.SetActive(false);
        m_HighScoresButton.gameObject.SetActive(false);
    }

    public void OnNewGame()
    {
        m_NewGameButton.gameObject.SetActive(false);
        m_HighScoresButton.gameObject.SetActive(false);
        m_HighScorePanel.SetActive(false);

        m_gameTime = 0;
        m_GameState = GameState.Playing;
        m_TimerText.gameObject.SetActive(true);
        m_MessageText.text = "";

        for (int i = 0; i < m_Characters.Length; i++)
        {
            m_Characters[i].SetActive(true);
        }
    }

    public void OnHighScores()
    {
        m_MessageText.text = "";

        m_HighScoresButton.gameObject.SetActive(false);
        m_HighScorePanel.SetActive(true);

        string text = "";
        for (int i = 0; i < m_HighScores.scores.Length; i++)
        {
            int seconds = m_HighScores.scores[i];
            text += string.Format("{0:D2}:{1:D1}\n",
                            (seconds / 60), (seconds % 60));
        }
        m_HighScoresText.text = text;
    }


    void Update()
    {
        // Frame counter code, every update per frame the the total frames shown will be displayed
        fpsCount++;
        fpsTimer += Time.deltaTime;
        if (fpsTimer >= 1.0f)
        {
            fpsText.text = "FPS: " + fpsCount;
            fpsCount = 0;
            fpsTimer -= 1;
        }


        switch (m_GameState)
        {
            case GameState.Start:
                if (Input.GetKeyUp(KeyCode.Return) == true)
                {
                    m_TimerText.gameObject.SetActive(true);
                    m_MessageText.text = "";
                    m_GameState = GameState.Playing;
                    for (int i = 0; i < m_Characters.Length; i++)
                    {
                        m_Characters[i].SetActive(true);
                    }

                }
                break;
            case GameState.Playing:
                bool isGameOver = false;

                m_gameTime += Time.deltaTime;
                int seconds = Mathf.RoundToInt(m_gameTime);
                m_TimerText.text = string.Format("{0:D2}:{1:D2}",
                            (seconds / 60), (seconds % 60));


                if (OneTankLeft() == true)
                {
                    isGameOver = true;
                }
                else if (IsPlayerDead() == true)
                {
                    isGameOver = true;
                }
                if (isGameOver == true)
                {
                    m_GameState = GameState.GameOver;
                    m_TimerText.gameObject.SetActive(false);

                    m_NewGameButton.gameObject.SetActive(true);
                    m_HighScoresButton.gameObject.SetActive(true);

                    if (IsPlayerDead() == true)
                    {
                        m_MessageText.text = "MISSION FAILED, YOU'LL GET 'EM NEXT TIME";
                    }
                    else
                    {
                        m_MessageText.text = "YOU HAVE ELIMINATED THE TAGERT!";
                        // The last section of code is going to save the highscore
                        m_HighScores.Addscore(Mathf.RoundToInt(m_gameTime));
                        m_HighScores.SaveScoresToFile();
                    }
                }
                break;
            case GameState.GameOver:
                if (Input.GetKeyUp(KeyCode.Return) == true)
                {
                    m_gameTime = 0;
                    m_GameState = GameState.Playing;
                    m_MessageText.text = "";
                    m_TimerText.gameObject.SetActive(true);
                    

                    for (int i = 0; i < m_Characters.Length; i++)
                    {
                        m_Characters[i].SetActive(true);
                    }
                }
                break;
        }

    }
}
