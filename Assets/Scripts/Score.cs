using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// UIを使う場合には忘れずに追加すること！
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int score = 0;
    private Text scoreLabel;

    void Start()
    {
        scoreLabel = GameObject.Find("ScoreLabel").GetComponent<Text>();
        scoreLabel.text = "SCORE：" + score;
    }

    // スコアを増加させるメソッド
    // 外部からアクセスするためpublicで定義する
    public void AddScore(int amount)
    {
        score += amount;
        scoreLabel.text = "SCORE：" + score;
    }
}