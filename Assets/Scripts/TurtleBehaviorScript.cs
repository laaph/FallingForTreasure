using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleBehaviorScript : MonoBehaviour
{
    float direction = 1f;
    float locationOnBlock;
    float speed = 1f;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(direction * speed * Time.deltaTime, 0, 0);
        locationOnBlock += direction * speed * Time.deltaTime;
        if(locationOnBlock > 2.0f)
        {
            direction = -1f;
            sr.flipX = true;
        }
        if(locationOnBlock < -2.0f)
        {
            direction = 1f;
            sr.flipX = false;
        }
        
    }

    public void StartWalking(float offset)
    {
        locationOnBlock = offset;
        if(offset < 0f)
        {
            direction = -1f;
            sr.flipX = true;
        } else
        {
            direction = 1f;
            sr.flipX = false;
        }
    }
}
