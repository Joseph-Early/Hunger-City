using UnityEngine;
using TMPro;

public class FPS_GUI : MonoBehaviour
{
    [SerializeField] public TMP_Text weaponActive, currentHealth;

    public string textToDisplay = "";

    public static FPS_GUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void LateUpdate()
    {
        currentHealth.text = textToDisplay;
        textToDisplay = "";
    }
}
