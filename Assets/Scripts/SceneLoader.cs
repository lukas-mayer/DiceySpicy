using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static void LoadNextScene()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % (SceneManager.sceneCount - 1));
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
