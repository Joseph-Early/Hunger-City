using UnityEngine;
using TMPro;

public class FPS_GUI : MonoBehaviour
{
    [SerializeField] public TMP_Text currentStats;

    public string textToDisplay = "";

    public static FPS_GUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        currentStats.text = textToDisplay;
    }
}
