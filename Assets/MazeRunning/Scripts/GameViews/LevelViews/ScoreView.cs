using MazeRunning.SharedStructures.Data;
using MazeRunning.Utils.Collections;
using TMPro;
using UnityEngine;

namespace MazeRunning.GameViews.LevelViews
{
    public class ScoreView : MonoBehaviour, IIdentityElement<PlayerType>
    {
        //[SerializeField] private TMP_Text playerTxt;
        [SerializeField] private TMP_Text scoreTxt;
        [field: SerializeField] public PlayerType Id { get; private set; }

        public void ShowScore(int score)
        {
            scoreTxt.SetText(score.ToString());
        }
    }
}