using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float BulletSpeed;

	void Start ()
    {
        if (BulletSpeed <= 0)
            BulletSpeed = 10;
	}
	
	void Update ()
    {
        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3(0, BulletSpeed * Time.deltaTime, 0);
        pos += transform.rotation * velocity;
        transform.position = pos;
    }
}
