using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectLevelUI : MonoBehaviour
{
  [SerializeField] private LevelsSelector _levelSelector;
  [SerializeField] private SceneLoader _sceneLoader;
  [SerializeField] private GameObject _levelButtonPrefab;
  [SerializeField] private RectTransform _gridParent;

  private void Awake()
  {
    Generate();
  }

  private void Generate()
  {
    int buttonsCount = _levelSelector.LevelsCount;

    for(int i = 0; i < buttonsCount; i++)
    {
      int levelIndex = i;
      GameObject levelButtonGO = Instantiate(_levelButtonPrefab, _gridParent);
      ChangeButtonText(levelIndex, levelButtonGO);
      AddListenerToButtons(i, levelIndex, levelButtonGO);
    }
  }

  private static void ChangeButtonText(int levelIndex, GameObject levelButtonGO)
  {
    TMP_Text levelButtonText = levelButtonGO.GetComponentInChildren<TMP_Text>();
    levelButtonText.text = (levelIndex + 1).ToString();
  }

  private void AddListenerToButtons(int i, int levelIndex, GameObject levelButtonGO)
  {
    Button levelButton = levelButtonGO.GetComponent<Button>();
    levelButton.onClick.AddListener(() => _sceneLoader.LoadLevelWithIndex(levelIndex));

    LockNotOpenedLevels(i, levelButton);
  }

  private void LockNotOpenedLevels(int i, Button levelButton)
  {
    if (i <= _levelSelector.HighestWonLevelIndex) { return; }

    levelButton.interactable = false;
  }


}
