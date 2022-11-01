using UnityEngine;
using TMPro;

public class FPS_GUI : MonoBehaviour
{
    [SerializeField] public TMP_Text weaponActive;

    public static FPS_GUI Instance;

    private void Awake() {
        Instance = this;
    }
}
