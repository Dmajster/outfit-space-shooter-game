using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets
{
    public class MainMenu : MonoBehaviour
    {
        public void OnPlayPressed()
        {
            SceneManager.LoadScene(1);
        }

        public void OnQuitPressed()
        {
            Application.Quit();
        }
    }
}
