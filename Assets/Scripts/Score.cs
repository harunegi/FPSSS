using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// UI���g���ꍇ�ɂ͖Y�ꂸ�ɒǉ����邱�ƁI
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int score = 0;
    private Text scoreLabel;

    void Start()
    {
        scoreLabel = GameObject.Find("ScoreLabel").GetComponent<Text>();
        scoreLabel.text = "SCORE�F" + score;
    }

    // �X�R�A�𑝉������郁�\�b�h
    // �O������A�N�Z�X���邽��public�Œ�`����
    public void AddScore(int amount)
    {
        score += amount;
        scoreLabel.text = "SCORE�F" + score;
    }
}