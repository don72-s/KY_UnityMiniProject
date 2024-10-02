using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreModel : MonoBehaviour
{
    int score;
    int bestScore;

    public event Action<int> ScoreChangedEvent;
    public event Action<int> BestScoreChangedEvent;

    public int Score {

        get { return score; }
        set { score = value; ScoreChangedEvent?.Invoke(score); }

    }

    public int BestScore {

        get { return bestScore; }
        set { bestScore = value; BestScoreChangedEvent?.Invoke(score); }

    }
}
