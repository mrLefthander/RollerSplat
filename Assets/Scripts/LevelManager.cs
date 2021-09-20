using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
  [SerializeField] private LevelsSelector _levelSelector;

  [Header("Tiles Prefabs")]
  [SerializeField] private GameObject _wallTilePrefab;
  [SerializeField] private GameObject _roadTilePrefab;

  public static event UnityAction LevelWinEvent;
  public RoadTile LevelStartRoadTile { get; private set; }  

  private Color _wallColor = Color.white;
  private Color _roadColor = Color.black;

  private int _roadTilesCount;
  private float _unitPerPixel;
  private LevelData _levelData;

  private void Awake()
  {
    _levelData = _levelSelector.GetCurrentLevelData();
    Generate();
  }

  public float GetLevelMaxDimension()
  {
    return Mathf.Max(_levelData.LevelTexture.width, _levelData.LevelTexture.height);
  }

  public Color GetPaintColor()
  {
    return _levelData.PaintColor;
  }

  public void CheckLevelWin(int paintedRoadTiles)
  {
    if (paintedRoadTiles == _roadTilesCount)
    {
      LevelWinEvent?.Invoke();
    }
  }

  private void Generate()
  {
    _unitPerPixel = _wallTilePrefab.transform.lossyScale.x;
    float halfUnitPerPixel = _unitPerPixel / 2f;

    float width = _levelData.LevelTexture.width;
    float height = _levelData.LevelTexture.height;

    Vector3 offset = (new Vector3(width / 2f, 0f, height / 2f) * _unitPerPixel)
                        - new Vector3(halfUnitPerPixel, 0f, halfUnitPerPixel);

    SpawnMap(width, height, offset);
  }

  private void SpawnMap(float width, float height, Vector3 offset)
  {
    for (int x = 0; x < width; x++)
    {
      for (int y = 0; y < height; y++)
      {
        Color pixelColor = _levelData.LevelTexture.GetPixel(x, y);

        Vector3 spawnPosition = (new Vector3(x, 0f, y) * _unitPerPixel) - offset;

        if (pixelColor == _wallColor)
          Spawn(_wallTilePrefab, spawnPosition);
        if (pixelColor == _roadColor)
          Spawn(_roadTilePrefab, spawnPosition);
      }
    }
  }

  private void Spawn(GameObject tilePrefab, Vector3 position)
  {
    position.y = tilePrefab.transform.position.y;

    GameObject tileGO = Instantiate(tilePrefab, position, Quaternion.identity, transform);

    if (tilePrefab != _roadTilePrefab) { return; }

    _roadTilesCount++;

    if (LevelStartRoadTile != null) { return; }

    LevelStartRoadTile = tileGO.GetComponent<RoadTile>();
  }
}
