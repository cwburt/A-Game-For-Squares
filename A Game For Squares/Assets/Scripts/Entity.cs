using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
    public float HP;
    public bool isAlive;

	protected virtual void Start ()
    {
        isAlive = true;
	}

    protected virtual void Update ()
    {
        if (HP <= 0)
            isAlive = false;
	}
}
