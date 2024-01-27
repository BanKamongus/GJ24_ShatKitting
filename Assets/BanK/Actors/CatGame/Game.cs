using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    [Header("GetFromScene")]
    public CatRow[] CatRows;
    public Spawner[] Spawners;
    public float RowOffset = 0.4f, RowScale = 2.25f;

    [Header("Properties")]
    public GameObject[] SpawnOBJ;

    [Header("Screens")]
    public GameObject Screen_End;

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
                        for (int i = 0; i < CatRows.Length; i++){
                            CatRows[i].Spawner.Spawn(SpawnOBJ[i]);
                        }
                    }

                    //float SpawnTime = 0;   
                    void Spawners_Update() {
                        CatRows = CatRows.OrderBy(row => row.Cat.CurrentScore).ToArray();
                        for (int i = 0; i < CatRows.Length; i++)
                        {
                            CatRow catRow = CatRows[i];
                            Vector3 newPosition = new Vector3(catRow.Cat.gameObject.transform.position.x, i * RowScale + RowOffset, 0); // Adjust the yOffset as needed
                            catRow.Cat.gameObject.transform.position = Vector3.Lerp(catRow.Cat.gameObject.transform.position, newPosition, 5 * Time.deltaTime); ;
                        }
                    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        Endhere GetEndhere = collision.gameObject.GetComponent<Endhere>();
        if (GetEndhere != null) {
            Screen_End.SetActive(true);
        }
    }
}
