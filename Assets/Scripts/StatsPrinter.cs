using LootLocker.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsPrinter : MonoBehaviour
{
    [SerializeField] TMP_Text childDeadText;
    [SerializeField] TMP_Text womenDeadText;
    [SerializeField] TMP_Text menDeadText;

    [SerializeField] TMP_Text maxHapText;
    [SerializeField] TMP_Text minHapText;
    [SerializeField] TMP_Text yearsActive;
    [SerializeField] TMP_Text moneyMadeText;

    [SerializeField] TMP_Text finalScoreText;
    [SerializeField] TMP_Text finalScoreMessageText;

    [SerializeField] TMP_Text GameOverReasonText;

    [SerializeField] TMP_Text PSText;

    public static int finalScore = 0;

    bool scoreSubmitted = false;

    string leaderboardKey = "HistoricGameJam";

    public TextMeshProUGUI playersNames;
    public TextMeshProUGUI playersScore;

    // Start is called before the first frame update
    void Start()
    {
        if (MoneyManager.gameLostReason == "bankrupt")
        {
            GameOverReasonText.text = "You went bankrupt and disgraced your family of rich industrialists";
        }
        else {
            GameOverReasonText.text = "You died peacefully surrounded by family and money";
        }
        childDeadText.text = "Children dead: " + StatsManager.deadChildren.ToString();
        womenDeadText.text = "Women dead: " + StatsManager.deadWomen.ToString();
        menDeadText.text = "Men dead: " + StatsManager.deadMen.ToString();
        maxHapText.text = "Highest workers happiness achieved: " + MoneyManager.maxHappinessReached.ToString();
        minHapText.text = "Lowest workers happiness achieved: " + MoneyManager.minHappinessReached.ToString();
        moneyMadeText.text = "Money made: £" + MoneyManager.totalGameProfit.ToString();
        if ((TimeManager.currentYear - 1830) <= 1)
        {
            yearsActive.text = "You were in the job for " + (TimeManager.currentYear - 1830).ToString() + " year";
        } else
        {
            yearsActive.text = "You were in the job for " + (TimeManager.currentYear - 1830).ToString() + " years";
        }

        calculateScore();

        if (MoneyManager.gameLostReason != "bankrupt") // only do this if the player didn't go bankrupt
        {
            finalScoreText.text = "Final Score: " + finalScore.ToString();
            if (finalScore <= -280)
            {
                finalScoreMessageText.text = "Rank: True capitalist";
            }
            else if (finalScore <= -125)
            {
                finalScoreMessageText.text = "Rank: Capitalist in training";
            }
            else if (finalScore <= -65)
            {
                finalScoreMessageText.text = "Rank: Benevolent magnate";
            }
            else if (finalScore <= 0)
            {
                finalScoreMessageText.text = "Rank: Secret proletarian";
            }
            else
            {
                finalScoreMessageText.text = "Rank: Actual anachronism";
            }

            MoneyManager.gameLostReason = "";
        }
        else
        {
            // if player went bankrupt
            finalScoreText.text = "Try not going bankrupt to reach the true ending!";
            finalScoreMessageText.text = "";
            MoneyManager.gameLostReason = "";
        }

        if (TimeManager.needToSubmitScore) // only do this if first time playing
        {
            StartCoroutine(SubmitScoreRoutine(finalScore)); // send then fetch
        }
        else
        {
            if (!MySceneManager.firstTimePlaying)
            {
                PSText.text = "P.S. you can only get into the leaderboard the first time you complete the game!";
            }
            StartCoroutine(FetchTopHighscoresRoutine()); // fetch only
            scoreSubmitted = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (scoreSubmitted)
        {
            // toggle show leadboard
            scoreSubmitted = false;
        }
    }

    void calculateScore()
    {
        finalScore = (int)Math.Round((MoneyManager.maxHappinessReached + MoneyManager.minHappinessReached) - (StatsManager.deadChildren * 5 + StatsManager.deadWomen * 3 + StatsManager.deadMen * 2.5));
    }


    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        string playerName = PlayerPrefs.GetString("name");
        print("saving entry in leaderboard with name " + playerName);
        LootLockerSDKManager.SubmitScore(playerName, scoreToUpload, leaderboardKey, playerName, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Score uploaded succesfully");
                done = true;
            }
            else
            {
                Debug.Log("failed " + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
        scoreSubmitted = true;
        print("now set scoreSubmitted to true and attempint to start fetching routine");
        StartCoroutine(FetchTopHighscoresRoutine());
    }

    public IEnumerator FetchTopHighscoresRoutine()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreList(leaderboardKey, 19, 0, (response) => {
            if (response.success)
            {
                Debug.Log("Fetched leaderboard correctly");
                string tempPlayerNames = "Name:\n";
                string tempPlayerScore = "Happiness Score:\n";

                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    tempPlayerNames += members[i].rank + ". ";
                    if (members[i].metadata != "")
                    {
                        tempPlayerNames += members[i].metadata;
                    }
                    else
                    {
                        tempPlayerNames += members[i].player.id;
                    }
                    tempPlayerScore += members[i].score + "\n";
                    tempPlayerNames += "\n";
                }
                done = true;
                playersNames.text = tempPlayerNames;
                playersScore.text = tempPlayerScore;
            }
            else
            {
                Debug.Log("Unable to fetch leadboard");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

}
