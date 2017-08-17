using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    float FadeSpeed;

    Color Opacity;

    void Start()
    {
        Opacity = GetComponent<SpriteRenderer>().color;

        if (FadeSpeed == 0)
            FadeSpeed = .035f;
    }

    void Update ()
    {
        if (gameObject.GetComponent<SpriteRenderer>().color.a < 1)
            FadeIn();

        if (transform.position.x >= 13 || transform.position.x <= -13 || transform.position.y >= 13 || transform.position.y <= -13)
        {
            //Calc Points

            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            //Calc Points

            Destroy(gameObject);
        }
        else if(other.tag == "Ship")
        {
            if(other.gameObject.GetComponent<Ship>().isAlive)
            {
                Destroy(gameObject);
            }
        }
    }

    void FadeIn()
    {
        if (GetComponent<SpriteRenderer>().color.a < 1)
        {
            GetComponent<SpriteRenderer>().color = Opacity;
            Opacity.a += FadeSpeed;
        }
    }
}
