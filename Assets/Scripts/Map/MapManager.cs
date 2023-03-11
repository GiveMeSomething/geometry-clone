using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;

[Serializable]
class Placeable
{
    public int BlockCode;
    public BlockType BlockType;
}

// Screen size: 20*10
// 
// Base platform size: 20*2
// 
// => The remaining area that the map can be rendered is:
// (20*10) - (20*2) = (20*8)
public class MapManager : MonoBehaviour
{
    // Renderable screen area
    private const int PLAY_SCREEN_HEIGHT = 8;
    private const int PLAY_SCREEN_WIDTH = 20;

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

    // Reference to NPC Manager to access its ObjectPools
    [SerializeField]
    private NPCManager _npcManager;

    [SerializeField]
    private List<Placeable> _placeables = new();

    private Dictionary<int, BlockType> _placeableCodeMap = new();

    // Data for map patterns and map cohesions
    private List<MapPattern> _mapPatterns;
    private List<MapCohesion> _mapCohesions;

    // Data for map streaming
    private MapPattern _currentMapPattern;
    private MapPattern _nextMapPattern;
    private bool renderable = true;
    private float _mapCoverTime;

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
        foreach (Placeable placeable in _placeables)
        {
            _placeableCodeMap.Add(placeable.BlockCode, placeable.BlockType);
        }

        SetCurrentMapPattern(_mapPatterns.Find(pattern => pattern.Id == 2));
        _nextMapPattern = _mapPatterns.Find(pattern => pattern.Id == 2);

    }

    private void FixedUpdate()
    {
        // Time it take to current map pattern to finish on screen
        if (renderable)
        {
            GenerateMap(_currentMapPattern);
            renderable = false;
        }

        _mapCoverTime -= Time.deltaTime;
        if (_mapCoverTime > 0)
        {
            return;
        }

        // TODO: Remove after test
        // ==================================
        var temp = _currentMapPattern;
        SetCurrentMapPattern(_nextMapPattern);
        _nextMapPattern = temp;
        // ==================================

        renderable = true;
    }

    private void ValidateMap(int[] data, int length)
    {
        if (data.Length % length != 0)
        {
            throw new Exception($"Map data is not compatible with length: {length}");
        }

        var row = data.Length / length;
        if (row > PLAY_SCREEN_HEIGHT)
        {
            throw new Exception("Unable to render map with over 8 rows");
        }
    }

    private void GenerateMap(MapPattern mapPattern)
    {
        var data = mapPattern.Data;
        var length = mapPattern.MapLen;

        var row = data.Length / length;

        List<Action> cleanupActions = new();
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < length; j++)
            {
                var currentCode = data[i * length + j];
                if (currentCode == 0)
                {
                    continue;
                }

                if (_placeableCodeMap.TryGetValue(currentCode, out var blockType))
                {
                    var instantiatePos = new Vector3(
                        transform.position.x + j + SCREEN_OFFSET_X + OBJECT_OFFSET + PLAY_SCREEN_WIDTH,
                        transform.position.y + i + SCREEN_OFFSET_Y + OBJECT_OFFSET,
                        transform.position.z
                    );

                    var currentObjectPool = _npcManager.GetObjectPool(blockType);
                    currentObjectPool.Pool.Get(out var placeable);

                    placeable.transform.position = instantiatePos;

                    cleanupActions.Add(() => currentObjectPool.Pool.Release(placeable));
                }
            }
        }

        // Start cleaning up out of screen objects after the map pattern is finished
        StartCoroutine(ReleaseAfterSeconds((PLAY_SCREEN_WIDTH + mapPattern.MapLen) / GameConst.PLATFORM_SPEED, cleanupActions));
    }

    private void SetCurrentMapPattern(MapPattern mapPattern)
    {
        // Calculate new map pattern info
        _mapCoverTime = mapPattern.MapLen / GameConst.PLATFORM_SPEED;
        _currentMapPattern = mapPattern;
    }

    private IEnumerator ReleaseAfterSeconds(float seconds, List<Action> actions)
    {
        yield return new WaitForSecondsRealtime(seconds);

        foreach(Action action in actions)
        {
            action.Invoke();
        }
    }
}

