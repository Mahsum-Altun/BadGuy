using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text spiritScore;
    public int score;
    public Animator animator;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        score = PlayerPrefs.GetInt("score", score);
    }
    private void Update()
    {
        spiritScore.text = score.ToString();
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death") == true)
        {
            score = 0;
            spiritScore.text = score.ToString();
            PlayerPrefs.SetInt("score", score);
        }
    }

    public void AddPoint()
    {
        score += 1;
        spiritScore.text = score.ToString();
        PlayerPrefs.SetInt("score", score);
    }
    public void InterestPoint()
    {
        score -= 1;
        spiritScore.text = score.ToString();
        PlayerPrefs.SetInt("score", score);
    }
    public void Relog()
    {
        score = 0;
        spiritScore.text = score.ToString();
        PlayerPrefs.SetInt("score", score);
    }
}
