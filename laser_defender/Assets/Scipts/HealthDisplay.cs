using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    TextMeshProUGUI healthText;
    PlayerScript playerScript;

    private void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        playerScript = FindObjectOfType<PlayerScript>();
    }
    private void Update()
    {
        healthText.text = playerScript.GetHealth().ToString();
    }
}