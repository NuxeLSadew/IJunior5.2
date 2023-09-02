using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
    public const float EnemiesTurnTime = 2f;
    public const float PlayersTurnTime = 10f;
    public const float TowersTurnTime = 1f;
    private const Team PlayersTeam = Team.Players;

    [SerializeField] private Button _endPlayersTurnButton;
    [SerializeField] private UnityEvent _onTurnEnded;

    private float _timer;
    private float _turnTime;
    private Team _whoseTurn;

    public Team WhoseTurn => _whoseTurn;
    public float Timer => _timer;
    public float TimeRemain => _turnTime - _timer;
    public event UnityAction OnTurnEnded
    {
        add => _onTurnEnded.AddListener(value);
        remove => _onTurnEnded.RemoveListener(value);
    }

    private void Start()
    {
        _whoseTurn = Team.Enemies;

        ResetTimer();
        SwitchTurnTimer();

        _endPlayersTurnButton.interactable = false;
        _endPlayersTurnButton.onClick.AddListener(EndPlayersTurn);

        _onTurnEnded?.Invoke();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _turnTime)
        {
            GiveTurnToNextTeam();
        }
    }

    private void EndPlayersTurn()
    {
        _endPlayersTurnButton.interactable = false;
        GiveTurnToNextTeam();
    }

    private void GiveTurnToNextTeam()
    {
        ResetTimer();

        int teamIndex = (int)_whoseTurn;
        int teamsCount = System.Enum.GetNames(typeof(Team)).Length;

        teamIndex++;

        if (teamIndex >= teamsCount)
        {
            teamIndex = 0;
        }

        _whoseTurn = (Team)teamIndex;

        if (_whoseTurn == PlayersTeam)
        {
            MakeButtonInteractible();
        }

        SwitchTurnTimer();
        _onTurnEnded?.Invoke();
    }

    private void SwitchTurnTimer()
    {
        switch (_whoseTurn)
        {
            case Team.Enemies:
                _turnTime = EnemiesTurnTime;
                break;

            case Team.Players:
                _turnTime = PlayersTurnTime;
                break;

            case Team.Towers:
                _turnTime = TowersTurnTime;
                break;
        }
    }

    private void MakeButtonInteractible()
    {
        _endPlayersTurnButton.interactable = true;
    }

    private void ResetTimer()
    {
        _timer = 0;
    }

    public enum Team
    {
        Enemies,
        Players,
        Towers
    }
}
