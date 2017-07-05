using UnityEngine;
using System.Collections;

public class Star_Spawner_Script : MonoBehaviour {

    [SerializeField]
    GameObject Star1;
    [SerializeField]
    GameObject Star2;
    float timer;
    int whichStar;
	// Use this for initialization
	void Awake () {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (timer > Random.Range(0.2f,1f))
        {
            whichStar = Random.Range(0, 2);
            Instantiate(
                (whichStar == 0 ? Star1 : Star2),
                new Vector3(Random.Range(transform.position.x-2f, transform.position.x+2f),Random.Range(transform.position.y - 2f, transform.position.y + 2f),1),
                Quaternion.identity
                );
            timer = 0;
        }

	
	}
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
