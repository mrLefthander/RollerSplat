using UnityEngine;
using DG.Tweening;

public class LevelWinUI : MonoBehaviour
{
  [SerializeField] private GameObject _levelWinMenuContainer;
  [SerializeField] private CanvasGroup _canvasGroup;

  private void Start()
  {
    Hide();
    LevelManager.LevelWinEvent += Show;
  }

  public void Hide()
  {
    _levelWinMenuContainer.SetActive(false);
    _canvasGroup.alpha = 0f;
  }

  private void OnDestroy()
  {
    LevelManager.LevelWinEvent -= Show;
  }

  private void Show()
  {
    _levelWinMenuContainer.SetActive(true);
    _canvasGroup.DOFade(1f, 0f).SetDelay(1f);
  }
}
