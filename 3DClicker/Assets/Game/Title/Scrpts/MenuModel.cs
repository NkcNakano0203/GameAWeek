using UnityEngine;
using UnityEngine.SceneManagement;

namespace TitleUI
{
    public class MenuModel : MonoBehaviour
    {
        const string GameScene = "GameScene";

        public void GameStart()
        {
            SceneManager.LoadScene(GameScene);
        }

        public void GameQuit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}