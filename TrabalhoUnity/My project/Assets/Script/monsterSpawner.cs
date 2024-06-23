using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [System.Serializable]
    public class WaveContent
    {
        [SerializeField] GameObject[] monsterSpawn;

        public GameObject[] GetMonsterSpawnList()
        {
            return monsterSpawn;
        }
    }

    [SerializeField] WaveContent[] waves;
    int currentWave = 0;
    float spawnRange = 10;
    public List<GameObject> currentMonster;

    void Start()
    {
        currentMonster = new List<GameObject>(); // Inicializa a lista
        SpawnWave(); // Gera a primeira wave
    }

    void Update()
    {
        // Verifica as condições para gerar a próxima wave
        if (currentMonster.Count == 0 && currentWave < waves.Length)
        {
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        if (currentWave >= waves.Length) return; // Verifica se não ultrapassou o limite de waves

        foreach (var monsterPrefab in waves[currentWave].GetMonsterSpawnList())
        {
            if (monsterPrefab != null) // Verifica se o prefab não é nulo
            {
                GameObject newspawn = Instantiate(monsterPrefab, FindSpawnLoc(), Quaternion.identity);
                currentMonster.Add(newspawn);

                Inimigo monsterComponent = newspawn.GetComponent<Inimigo>();
                if (monsterComponent != null)
                {
                    monsterComponent.SetSpawner(this);
                }
            }
        }

        currentWave++; // Avança para a próxima wave após gerar a wave atual
    }

    Vector3 FindSpawnLoc()
    {
        for (int attempt = 0; attempt < 10; attempt++) // Tenta encontrar uma localização válida 10 vezes
        {
            float xLoc = UnityEngine.Random.Range(-spawnRange, spawnRange) + transform.position.x;
            float zLoc = UnityEngine.Random.Range(-spawnRange, spawnRange) + transform.position.z;
            float yLoc = transform.position.y;

            Vector3 spawnPos = new Vector3(xLoc, yLoc, zLoc);

            if (Physics.Raycast(spawnPos, Vector3.down, 5))
            {
                return spawnPos;
            }
        }

        // Se nenhuma localização válida for encontrada, retorna a posição do transform
        return transform.position;
    }
}