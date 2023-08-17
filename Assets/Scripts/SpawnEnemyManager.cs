﻿using UnityEngine;

public class SpawnEnemyManager : MonoBehaviour
{
    [Header("生成怪物系統陣列")]
    public SpawnSystem[] spawnSystems;
    [Header("生成怪物資料")]
    public DataSpawnEnemy[] dataSpawnEnemys;

    private float timer;
    private int index;

    private void Update()
    {
        ChangeSpawnEnemy();
    }

    private void ChangeSpawnEnemy()
    {
        timer += Time.deltaTime;

        if (index >= dataSpawnEnemys.Length) return;

        if (timer >= dataSpawnEnemys[index].timeToSpawn)
        {

            for (int i = 0; i < spawnSystems.Length; i++)
            {
                spawnSystems[i].prefabEnemy = dataSpawnEnemys[index].prefabEnemy;
                spawnSystems[i].interval = dataSpawnEnemys[index].intervalSpawn;
            }

            index++;
            print("生成的波數：" + index);
        }
    }
}
