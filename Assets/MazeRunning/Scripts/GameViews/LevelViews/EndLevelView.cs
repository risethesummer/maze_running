using MazeRunning.Gameplay.Managers;
using MazeRunning.SharedStructures.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MazeRunning.GameViews.LevelViews
{
    public class EndLevelView : MonoBehaviour
    {
        private LevelManager _levelManager;
        [SerializeField] private Button okBtn;
        private void Awake()
        {
            //After click the OK btn => try to start the new level and hide the panel off 
            okBtn.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
                _levelManager.ApplyNextLevel();
            });
        }
        [Inject]
        public void Construct(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }
        public void ShowEndLevel(EndLevelSignal signal)
        {
            
        }
    }
}