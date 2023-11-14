using UnityEngine;
using TMPro;

public class DeathScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScoreText;
    private void OnEnable()
    {
        GameManager gm = GameManager.Instance;
        finalScoreText.text = $"GAMEOVER\r\n\r\nHIGHSCORE: {gm.GetHighScore}\r\n\r\nCURRENT SCORE: {gm.GetCurrentScore}";
    }
}


