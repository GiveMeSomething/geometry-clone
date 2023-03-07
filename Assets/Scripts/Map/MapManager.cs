using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private List<MapPattern> _mapPatterns;

    private List<MapCohesion> _mapCohesions;

    private void Awake()
    {
        ICommand loadMapPatternCommand = new LoadMapPattern(SetMapPatterns);
        ICommand loadMapCohesionCommand = new LoadMapCohesion(SetMapCohesions);

        loadMapPatternCommand.Execute();
        loadMapCohesionCommand.Execute();
    }

    private void SetMapPatterns(MapPatternFile mapPatterns)
    {
        _mapPatterns = new(mapPatterns.mapPatterns);
    }

    private void SetMapCohesions(MapCohesionFile mapCohesions)
    {
        _mapCohesions = new(mapCohesions.cohesions);
    }
}

