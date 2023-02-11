using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Globals = HungerCity.Systems.Globals;

namespace HungerCity.UI
{

    public class UpdateTimeRemaining : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI TMP;

        // Show time remaining
        void Update() => TMP.text = $"Survive: {Mathf.Round(HungerCity.Systems.GameState.Instance.timeRemaining)}s";
    }

}