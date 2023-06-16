using UnityEngine;

namespace MazeRunning.Gameplay.Camera
{
    public class BotCameraController : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera botCamera;
        public void Prior(bool prior)
        {
            botCamera.depth = prior ? 2 : 0;
        }
    }
}