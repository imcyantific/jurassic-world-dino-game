using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move the obstacle to the left
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        //destroy obstacle when it goes off screen
        if (transform.position.x < -12f)
        {
            Destroy(gameObject);
        }

    }
}
