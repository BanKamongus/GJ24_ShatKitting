using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [Header("GetFromScene")]
    public CatRow[] CatRows;
    public Spawner[] Spawners;

    [Header("SpawnablePrefabs")]
    public GameObject[] SpawnOBJ;

    void Start(){
        Game_Init();
    }

    void Game_Init() {
        Spawners_Init();
    }

    void Update()
    {
        Spawners_Update();
    }


                    void Spawners_Init() {
                        Spawners = FindObjectsOfType<Spawner>();
                    }
                    float SpawnTime = 0;   
                    void Spawners_Update() {
                        SpawnTime+= Time.deltaTime;
                        if(SpawnTime>2){ SpawnTime=0;
                            foreach (Spawner EACH in Spawners)
                            {
                                EACH.Spawn(SpawnOBJ[0]);
                            }
                        }
                    }
    
}
