using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneLoader", menuName = "Scene Helpers/SceneLoader")]
public class SceneLoader : ScriptableObject
{
  public class SceneNames
  {
    public const string MAIN_MENU = "MainMenu";
    public const string CORE = "Core";
    public const string GAMEPLAY = "Gameplay";
    public const string UI = "UI";
    public const string SELECT_LEVEL_MENU = "SelectLevelMenu";
  }

  [SerializeField] private LevelsSelector _levelSelector;

  public void LoadNextLevel()
  {
    SceneManager.UnloadSceneAsync(SceneNames.GAMEPLAY);
    SceneManager.LoadSceneAsync(SceneNames.GAMEPLAY, LoadSceneMode.Additive);
  }

  public void LoadLevelWithIndex(int index)
  {
    _levelSelector.ChangeCurrenLevelIndexTo(index);
    LoadGameplay();
  }

  public void LoadGameplay()
  {
    SceneManager.LoadSceneAsync(SceneNames.CORE);
    SceneManager.LoadSceneAsync(SceneNames.GAMEPLAY, LoadSceneMode.Additive);
    SceneManager.LoadSceneAsync(SceneNames.UI, LoadSceneMode.Additive);
  }

  public void LoadMainMenu()
  {
    SceneManager.LoadSceneAsync(SceneNames.MAIN_MENU, LoadSceneMode.Single);
  }

  public void LoadSelectLevelMenu()
  {
    SceneManager.LoadSceneAsync(SceneNames.SELECT_LEVEL_MENU, LoadSceneMode.Single);
  }

  public void QuitGame()
  {
    Application.Quit();
  }
}
