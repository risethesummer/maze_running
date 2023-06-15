using MazeRunning.SharedStructures.Data;
using MazeRunning.Utils.Collections;
using TMPro;
using UnityEngine;

namespace MazeRunning.GameViews.LevelViews
{
    public class ScoreView : MonoBehaviour, IIdentityElement<PlayerType>
    {
        [SerializeField] private TMP_Text playerTxt;
        [SerializeField] private TMP_Text scoreTxt;
        [field: SerializeField] public PlayerType Id { get; private set; }
        private void Start()
        {
            playerTxt.SetText(Id.ToString());
        }
        public void ShowScore(int score)
        {
            scoreTxt.SetText(score.ToString());
        }
    }
}