using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrop : MonoBehaviour
{
    public float speed = 0.05f;

    // The two variables below should be called backgrounds, but being as i'm using bricks...
    public Transform[] bricks;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);
        foreach(Transform t in bricks)
        {
            if (this.transform.position.y - t.position.y < -10.4f)
            {
                t.position = new Vector3(t.position.x, t.position.y - 20.8f, t.position.z);
            }
        }
    }
}
