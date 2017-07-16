using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private Waypoint waypointPrefab;

    [SerializeField]
    private Texture2D map;

    [SerializeField]
    private ColorToPrefab[] colorMappings;

    public Dictionary<Vector3, Waypoint> wayPoints = new Dictionary<Vector3, Waypoint>();
    private int offSet = 10;

    public static LevelGenerator Instance;

    void Awake ()
	{
        Instance = this;
        GenerateMap();
	}
	
    private void GenerateMap()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateObject(x, y);
            }
        }
    }

    private void GenerateObject(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);
        
        if (pixelColor.a == 0)
        {
            return;
        }

        foreach (ColorToPrefab colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                Vector3 position = new Vector3(x * offSet, 0f, y * offSet);

                for (int i = 0; i < colorMapping.prefabs.Length; i++)
                {
                    if (Random.Range(1, 100) < 20 && colorMapping.color.Equals(Color.black))
                    {
                        wayPoints.Add(position, waypointPrefab);
                    }

                    Instantiate(colorMapping.prefabs[i], position, Quaternion.identity, transform);
                }
            }
        }

        foreach (KeyValuePair<Vector3, Waypoint> waypoint in wayPoints)
        {
            Instantiate(waypoint.Value, waypoint.Key, Quaternion.identity, transform);
        }
    }

    public Vector3 GetWaypoint(int index)
    {
        return wayPoints.ElementAt(index).Key;        
    }

}
