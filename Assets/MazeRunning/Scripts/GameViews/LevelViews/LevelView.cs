using MazeRunning.SharedStructures.Data;
using MazeRunning.SharedStructures.Signals;
using MazeRunning.Utils.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace MazeRunning.GameViews.LevelViews
{
    public class LevelView : MonoBehaviour
    {
        //Mapping from the player type to its associated score view
        [SerializeField] private NoKeySerializableDictionary<PlayerType, ScoreView> scoreViews;
        [SerializeField] private TMP_Text currentLevelTxt;
        [SerializeField] private EndLevelView endLevelView;
        [SerializeField] private WaitForGeneratingMazeView waitForGeneratingMazeView;
        [Inject]
        public void Construct(SignalBus signalBus)
        {
            scoreViews.Init();
            signalBus.Subscribe<StartLevelSignal>(ShowLevelInfo);
            
            //Show the endLevelView after receiving the endLevel signal
            signalBus.Subscribe<EndLevelSignal>(e =>
            {
                endLevelView.gameObject.SetActive(true);
                endLevelView.ShowEndLevel(e);
            });
        }
        private void ShowLevelInfo(StartLevelSignal info)
        {
            foreach (var (player, score) in info.Scores)
                if (scoreViews.TryGetValue(player,  out var view))
                    view.ShowScore(score);
            currentLevelTxt.SetText(info.CurrentLevel.ToString());
            waitForGeneratingMazeView.gameObject.SetActive(true);
        }
    }
}