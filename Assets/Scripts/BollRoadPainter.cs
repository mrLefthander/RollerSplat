using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class BollRoadPainter : MonoBehaviour
{
  [SerializeField] private LevelManager _levelManager;
  [SerializeField] private BallMovement _ballMovement;
  [SerializeField] private MeshRenderer _ballMeshRenderer;

  private int _paintedRoadTiles = 0;
  private Color _paintColor;

  private void Start()
  {
    _paintColor = _levelManager.GetPaintColor();

    _ballMeshRenderer.material.color = _paintColor;
    Paint(_levelManager.LevelStartRoadTile, 0f, 0f);

    _ballMovement.MoveEvent += OnBallMoveStartHandler;
  }

  private void OnDestroy()
  {
    _ballMovement.MoveEvent -= OnBallMoveStartHandler;
  }

  private void OnBallMoveStartHandler(List<RoadTile> roadTiles, float totalDuration)
  {
    float stepDuration = totalDuration / roadTiles.Count;
    for (int i = 0; i < roadTiles.Count; i++)
    {
      RoadTile roadTile = roadTiles[i];

      if (roadTile.IsPainted) { continue; }

      float delay = i * (stepDuration / 2f);
      Paint(roadTile, totalDuration, delay);
    }
    _levelManager.CheckLevelWin(_paintedRoadTiles);
  }

  private void Paint(RoadTile roadTile, float duration, float delay)
  {
    roadTile.MeshRenderer.material.DOColor(_paintColor, duration).SetDelay(delay);
    roadTile.IsPainted = true;
    _paintedRoadTiles++;
  }
}
