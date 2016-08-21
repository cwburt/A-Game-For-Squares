using UnityEngine;
using System.Collections;

public class Main_Square_Spawner_LEFTandRIGHT : MonoBehaviour {

    private int WhenToSpawn;// In 3 2 1
    private Vector2 spawn_position;// Where TheSquare is going to be spawned
    [SerializeField] GameObject TheSquare;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        WhenToSpawn++;
        if(WhenToSpawn == 20)
        {
            Spawn(TheSquare);
            WhenToSpawn = 0;
        }
	}
    void Spawn(GameObject _Square)
    {
        spawn_position = new Vector2(transform.position.x, transform.position.y);
        Instantiate(TheSquare, spawn_position, Quaternion.identity);
    }
}