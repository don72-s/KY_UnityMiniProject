using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ScoreContoller : MonoBehaviour
{

    [Header("score model")]
    [SerializeField]
    ScoreModel scoreModel;

    [Header("ui[TMP]")]
    [SerializeField]
    TextMeshProUGUI score;
    [SerializeField]
    TextMeshProUGUI bestScore;

    StringBuilder scoreSb;
    int scoreLength;

    StringBuilder bestScoreSb;
    int bestScoreLength;

    private void Start() {
        
        scoreSb = new StringBuilder("Score : ", 16);
        scoreLength = 8;

        bestScoreSb = new StringBuilder("Best Score : ", 16);
        bestScoreLength = 13;

        scoreModel.ScoreChangedEvent += UpdateScore;
        scoreModel.BestScoreChangedEvent += UpdateBestScore;

    }

    void UpdateScore(int _updatedScore) { 
    
        scoreSb.Length = scoreLength;
        scoreSb.Append(_updatedScore);
        score.SetText(scoreSb);

        if (scoreModel.BestScore < scoreModel.Score)
            scoreModel.BestScore = scoreModel.Score;

    }

    void UpdateBestScore(int _updatedBestScore) {

        bestScoreSb.Length = bestScoreLength;
        bestScoreSb.Append(_updatedBestScore);
        bestScore.SetText(bestScoreSb);

    }

    public void AddScore() {

        scoreModel.Score++;

    }

    public void ResetScore() {

        scoreModel.Score = 0;

    }

}
