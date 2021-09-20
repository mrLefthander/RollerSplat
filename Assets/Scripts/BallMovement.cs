using UnityEngine;
using UnityEngine.Events;
using GG.Infrastructure.Utils.Swipe;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;

public class BallMovement: MonoBehaviour
{
  [SerializeField] private SwipeListener _swipeListener;
  [SerializeField] private LevelManager _levelManager;

  [SerializeField] private float stepDuration = 0.1f;
  [SerializeField] private LayerMask _wallAndRoadsLayerMask;

  public event UnityAction<List<RoadTile>, float> MoveEvent;

  private float _rayDistance = 0f;
  private Vector3 _moveDirection;
  private bool _canMove = true;

  private void Start()
  {
    Vector3 ballStartPosition = _levelManager.LevelStartRoadTile.transform.position;
    transform.position = SetBallTargetPosition(ballStartPosition);

    _rayDistance = _levelManager.GetLevelMaxDimension();

    _swipeListener.OnSwipe.AddListener(OnSwipeHander);
  }

  private void OnDestroy()
  {
    _swipeListener.OnSwipe.RemoveListener(OnSwipeHander);
  }

  private void OnSwipeHander(string swipe)
  {
    switch (swipe)
    {
      case "Right":
        _moveDirection = Vector3.right;
        break;
      case "Left":
        _moveDirection = Vector3.left;
        break;
      case "Up":
        _moveDirection = Vector3.forward;
        break;
      case "Down":
        _moveDirection = Vector3.back;
        break;
    }
    MoveBall();
  }

  private void MoveBall()
  {
    if (!_canMove) { return; }

    _canMove = false;
    Vector3 targetBallPosition = transform.position;
    int steps = 0;
    List<RoadTile> ballPathRoadTiles = new List<RoadTile>();

    RaycastHit[] hits = Physics
      .RaycastAll(transform.position, _moveDirection, _rayDistance, _wallAndRoadsLayerMask)
      .OrderBy(h => h.distance)
      .ToArray();

    for (int i = 0; i < hits.Length; i++)
    {
      if (hits[i].collider.isTrigger) //RoadTile
      {
        ballPathRoadTiles.Add(hits[i].transform.GetComponent<RoadTile>());
      }
      else if (i == 0) //WallTile in that direction
      {
        _canMove = true;
        return;
      }
      else //WallTile - end of the road
      {
        steps = i;
        Vector3 targetRoadTilePosition = hits[i - 1].transform.position;
        targetBallPosition = SetBallTargetPosition(targetRoadTilePosition);
        break;
      }
    }

    float moveDuration = stepDuration * steps;
    transform.DOMove(targetBallPosition, moveDuration)
        .SetEase(Ease.OutExpo)
        .OnComplete(() => _canMove = true);

    MoveEvent?.Invoke(ballPathRoadTiles, moveDuration);
  }

  private Vector3 SetBallTargetPosition(Vector3 position)
  {
    position.y = transform.position.y;
    return position;
  }
}
