using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreDisplay : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    GameSession gamesession;

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gamesession = FindObjectOfType<GameSession>();
    }
    private void Update()
    {
        scoreText.text = gamesession.GetScore().ToString();
    }
}
