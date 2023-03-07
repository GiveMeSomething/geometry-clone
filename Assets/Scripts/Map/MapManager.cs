using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
class Placeable
{
    public int ObjectCode;
    public GameObject GameObjectPrefab;
}

// Screen size: 20*10
// 
// Base platform size: 20*2
// 
// => The remaining area that the map can be rendered is:
// (20*10) - (20*2) = (20*8)
public class MapManager : MonoBehaviour
{
    // Renderable screen area height
    private const int PLAY_SCREEN_HEIGHT = 8;

    // The screen use its center as its pivot,
    // so it goes from -10 -> 10 on x axis and -5 -> 5 on y axis
    // We need to offset those different so we can place blocks at the right place
    private const int SCREEN_OFFSET_X = -10;
    // Offset y is a little different because it have the platform at the bottom
    // Calculated by screen offset - platform size
    private const int SCREEN_OFFSET_Y = -3;

    // GameObject have the same problem with screen, calculated from their center to their edge
    // We need to offset those different so we can place blocks at the right place
    private const float OBJECT_OFFSET = 0.5f;

    private List<MapPattern> _mapPatterns;
    private List<MapCohesion> _mapCohesions;

    [SerializeField]
    private List<Placeable> _placeables = new();

    private Dictionary<int, GameObject> _placeableMap = new();

    private void Awake()
    {
        ICommand loadMapPatternCommand = new LoadMapPattern(SetMapPatterns);
        ICommand loadMapCohesionCommand = new LoadMapCohesion(SetMapCohesions);

        loadMapPatternCommand.Execute();
        loadMapCohesionCommand.Execute();
    }

    private void SetMapPatterns(MapPatternFile mapPatterns)
    {
        _mapPatterns = new(mapPatterns.MapPatterns);
    }

    private void SetMapCohesions(MapCohesionFile mapCohesions)
    {
        _mapCohesions = new(mapCohesions.MapCohesions);
    }

    private void Start()
    {
        // Map game blocks into a dictionary
        foreach(Placeable placeable in _placeables)
        {
            _placeableMap.Add(placeable.ObjectCode, placeable.GameObjectPrefab);
        }

        // TODO: Remove this after tested
        var currentPattern = _mapPatterns.Find(pattern => pattern.Id == 1);
        ValidateMap(currentPattern.Data, currentPattern.MapLen);
        GenerateMap(currentPattern.Data, currentPattern.MapLen);
    }

    private void ValidateMap(int[] data, int length)
    {
        if(data.Length % length != 0)
        {
            throw new Exception($"Map data is not compatible with length: {length}");
        }

        var row = data.Length / length;
        if(row > PLAY_SCREEN_HEIGHT)
        {
            throw new Exception("Unable to render map with over 8 rows");
        }
    }

    private void GenerateMap(int[] data, int length)
    {
        var row = data.Length / length;
        for(int i = 0; i < row; i++)
        {
            for (int j = 0; j < length; j++)
            {
                var currentValue = data[i * length + j];
                if (currentValue == 0)
                {
                    continue;
                }

                if(_placeableMap.TryGetValue(currentValue, out var placeable))
                {
                    var instantiatePos = new Vector3(
                        transform.position.x + j + SCREEN_OFFSET_X + OBJECT_OFFSET,
                        transform.position.y + i + SCREEN_OFFSET_Y + OBJECT_OFFSET,
                        transform.position.z
                    );

                    // TODO: Move this into respective ObjectPool/Factory
                    Instantiate(placeable, instantiatePos, transform.rotation);
                }
            }
        }
    }
}

