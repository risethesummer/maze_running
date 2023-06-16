using MazeRunning.Gameplay.Managers;
using MazeRunning.SharedStructures.Data;
using MazeRunning.SharedStructures.Signals;
using MazeRunning.Utils.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MazeRunning.GameViews.LevelViews
{
    public class EndLevelView : MonoBehaviour
    {
        private LevelManager _levelManager;
        [SerializeField] private TMP_Text endLevelTxt;
        [SerializeField] private Button okBtn;

        [SerializeField] private SerializableDictionary<PlayerType, string> winTxt;
        private void Awake()
        {
            //After click the OK btn => try to start the new level and hide the panel off 
            okBtn.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
                _levelManager.ApplyNextLevel();
            });
            winTxt.Init();
        }
        [Inject]
        public void Construct(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }
        public void ShowEndLevel(EndLevelSignal signal)
        {
            if (winTxt.TryGetValue(signal.Winner, out var txt))
                endLevelTxt.SetText(txt);
        }
    }
}