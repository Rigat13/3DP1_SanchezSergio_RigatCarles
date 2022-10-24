using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text score;
    int maxScore;

    public void updateUI(int currentScore)
    {
        score.text = currentScore + " / " + maxScore;
    }

    public void setMaxScore(int maxScore)
    {
        this.maxScore = maxScore;
    }
}
