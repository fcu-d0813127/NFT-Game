using UnityEngine.SceneManagement;

public class LoadingSceneController {
  public static void LoadScene() {
    SceneManager.LoadScene("Loading", LoadSceneMode.Additive);
  }

  public static void UnLoadScene() {
    SceneManager.UnloadSceneAsync("Loading");
  }
}
