using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private Texture2D map;

    [SerializeField]
    private ColorToPrefab[] colorMappings;

    private int offSet = 10;

	void Start ()
	{
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
                    Instantiate(colorMapping.prefabs[i], position, Quaternion.identity, transform);
                }
            }
        }
    }

}
