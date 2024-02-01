using UnityEngine;
using UnityEngine.UI;
using YG;

public class LeaderBoardManager : MonoBehaviour
{
    public static LeaderBoardManager Instance;

    [SerializeField]
    private LeaderboardYG leaderboardYG;

    public void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void NewScore(int score)
    {
        // ����������� ����� ���������� ������ �������
        YandexGame.NewLeaderboardScores(leaderboardYG.nameLB, score);

        // ����� ���������� ������ ������� ���������� � ���������� LeaderboardYG
        // leaderboardYG.NewScore(int.Parse(scoreLbInputField.text));
    }
}