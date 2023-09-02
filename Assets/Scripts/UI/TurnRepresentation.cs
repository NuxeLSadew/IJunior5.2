using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TurnSystem))]
public class TurnRepresentation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _whoseTurnText;

    private TurnSystem _turnSystem;

    private Dictionary<TurnSystem.Team, string> _teamsNamesTranslated = new Dictionary<TurnSystem.Team, string>()
    {
        {TurnSystem.Team.Enemies, "врагов" },
        {TurnSystem.Team.Players, "игроков" },
        {TurnSystem.Team.Towers, "башен" },
    };

    private Dictionary<TurnSystem.Team, Color> _teamsNamesColors = new Dictionary<TurnSystem.Team, Color>()
    {
        {TurnSystem.Team.Enemies, new Color(210, 0, 0) },
        {TurnSystem.Team.Players, new Color(0, 210, 0) },
        {TurnSystem.Team.Towers, new Color(20, 140, 20) },
    };

    private void Start()
    {
        _turnSystem = GetComponent<TurnSystem>();
        _turnSystem.OnTurnEnded += UpdateWhoseTurnText;
        UpdateWhoseTurnText();
    }

    private void FixedUpdate()
    {
        _timerText.text = _turnSystem.TimeRemain.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture);
    }

    public void UpdateWhoseTurnText()
    {
        string whoseTurnTranslated = _teamsNamesTranslated[_turnSystem.WhoseTurn];
        Color whoseTurnColor = _teamsNamesColors[_turnSystem.WhoseTurn];

        _whoseTurnText.text = $"Ход {whoseTurnTranslated}";
        _whoseTurnText.color = whoseTurnColor;
    }
}
