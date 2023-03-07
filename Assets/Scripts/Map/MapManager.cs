using UnityEngine;

public class MapManager : MonoBehaviour
{
    private MapPatternFile _mapPatterns;

    private MapCohesionFile _mapCohesions;

    private void Awake()
    {
        ICommand loadMapPatternCommand = new LoadMapPattern(SetMapPatterns);
        ICommand loadMapCohesionCommand = new LoadMapCohesion(SetMapCohesions);

        loadMapCohesionCommand.Execute();
        loadMapCohesionCommand.Execute();
    }

    private void SetMapPatterns(MapPatternFile mapPatterns)
    {
        _mapPatterns = mapPatterns;
    }

    private void SetMapCohesions(MapCohesionFile mapCohesions)
    {
        _mapCohesions = mapCohesions;
    }
}

