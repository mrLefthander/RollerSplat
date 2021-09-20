using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Levels/Level")]
public class LevelData : ScriptableObject
{
  [Header("Level texture")]
  [SerializeField] private Texture2D _levelTexture;
  public Texture2D LevelTexture
  {
    get { return _levelTexture; }
    private set { }
  }

  [Header("Ball and Road paint color")]
  [SerializeField] private Color _paintColor;
  public Color PaintColor
  {
    get { return _paintColor; }
    private set { }
  }
}
