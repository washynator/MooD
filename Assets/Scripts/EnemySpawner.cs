using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();

	void Start ()
	{
        foreach(Enemy enemy in enemies)
        {
            SpawnEnemy(enemy);
        }
	}

    private void SpawnEnemy(Enemy enemyToSpawn)
    {
        Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
    }
	
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.U))
        {
            SpawnEnemy(enemies[0]);
        }
	}

}
