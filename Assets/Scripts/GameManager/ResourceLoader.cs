using System.Collections.Generic;
using UnityEngine;

// File for storing resource settings and load functions
public class ResourceLoader
{
    // Dictionary to save loaded resources
    private static readonly Dictionary<string, object> _resMap = new();
    
    private const string MAP_PATTERN_FILE = "Resources/Settings/Map/maps.json";

    private const string MAP_COHESION_FILE = "Resources/Settings/Map/cohesion.json";

    private static T LoadJsonTextResource<T>(string filePath)
    {
        if(_resMap.TryGetValue(filePath, out var resource))
        {
            return (T)resource;
        }

        var settings = Resources.Load<TextAsset>(filePath);
        return JsonUtility.FromJson<T>(settings.text);
    }

    public static MapPatternFile LoadMapPattern()
    {
        return LoadJsonTextResource<MapPatternFile>(MAP_PATTERN_FILE);
    }

    public static MapCohesionFile LoadMapCohesion()
    {
        return LoadJsonTextResource<MapCohesionFile>(MAP_COHESION_FILE);
    }
}