using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Levels Selector", menuName = "Levels/Level Selector")]
public class LevelsSelector : ScriptableObject
{
  [Header("Levels List")]
  [SerializeField] private List<LevelData> _levels = new List<LevelData>();

  [SerializeField] private int _currentLevelIndex = 0;

  public int LevelsCount { get => _levels.Count; }
  public int HighestWonLevelIndex { get; private set; } = 0;


  private void OnEnable()
  {
    LevelManager.LevelWinEvent += OnLevelWin;
  }

  private void OnLevelWin()
  {
    NextLevelIndex();
    HighestWonLevelIndex = _currentLevelIndex > HighestWonLevelIndex ? _currentLevelIndex : HighestWonLevelIndex;
  }

  public LevelData GetCurrentLevelData()
  {
    return _levels[_currentLevelIndex];
  }

  public void ChangeCurrenLevelIndexTo(int index)
  {
    _currentLevelIndex = Mathf.Clamp(index, 0, _levels.Count - 1);
  }

  private void NextLevelIndex()
  {
    _currentLevelIndex++;

    if (_currentLevelIndex < _levels.Count) { return; }

    _currentLevelIndex = 0;
  }
}
