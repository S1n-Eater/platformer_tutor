using UnityEngine;
using UnityEngine.SceneManagement;


namespace Scripts.Components
{
    public class ReloadLevelComponent : MonoBehaviour
    {
        public void Reload()
        {
            PlayerPrefs.DeleteAll();
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);

        }
    }
}