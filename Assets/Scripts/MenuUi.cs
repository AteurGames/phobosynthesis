using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUi : MonoBehaviour {
    public void Begin() {
        SceneManager.LoadSceneAsync("Level1");
    }
    public void Quit() {
        Application.Quit();
    }
}
 