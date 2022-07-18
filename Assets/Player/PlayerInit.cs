using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerInit : MonoBehaviour
{
    public GameObject Player;
    public int nextScene;
    
    // Start is called before the first frame update
    void Start()
    {
        //直接載入下個場景，並讓玩家不被摧毀
        // SceneManager.LoadScene(nextScene);
        DontDestroyOnLoad(Player);
    }
}
