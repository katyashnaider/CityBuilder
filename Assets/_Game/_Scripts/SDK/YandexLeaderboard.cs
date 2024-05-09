using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

namespace CityBuilder.SDK
{
    public class YandexLeaderboard : MonoBehaviour
    {
        [SerializeField] private Button _buttonLeaderboard;
        
        private void Awake()
        {
            _buttonLeaderboard.onClick.AddListener(OnOpenedLeaderboard);
        }

        private void OnDisable()
        {
            _buttonLeaderboard.onClick.RemoveListener(OnOpenedLeaderboard);
        }

        private void OnOpenedLeaderboard()
        {
            PlayerAccount.Authorize();

            if (PlayerAccount.IsAuthorized)
            {
                PlayerAccount.RequestPersonalProfileDataPermission();
                
                _leaderboardView.gameObject.SetActive(!_leaderboardView.gameObject.activeSelf);
                SetPlayerScore(10);
                Fill();
            }

            if (PlayerAccount.IsAuthorized == false)
            {
                return;
            }
        }

        private const string LeaderboardName = "Leaderboard";
        private const string AnonymousName = "Anonymoys";
        private readonly List<LeaderboardPlayer> _leaderboardPlayers = new();

        [SerializeField] private LeaderboardView _leaderboardView;

        public void SetPlayerScore(int score)
        {
            if (PlayerAccount.IsAuthorized == false)
                return;

            Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
            {
                if (result.score < score)
                    Leaderboard.SetScore(LeaderboardName, score);
            });
        }

        public void Fill()
        {
            if (PlayerAccount.IsAuthorized == false)
                return;

            _leaderboardPlayers.Clear();

            Leaderboard.GetEntries(LeaderboardName, (result) =>
            {
                foreach (var entry in result.entries)
                {
                    int rank = entry.rank;
                    int score = entry.score;
                    string name = entry.player.publicName;
                    
                    if (string.IsNullOrEmpty(name))
                        name = AnonymousName;
                    
                    _leaderboardPlayers.Add(item: new LeaderboardPlayer(rank, name, score));
                }

                _leaderboardView.ConstructLeaderboard(_leaderboardPlayers);
            });
        }
    }
}