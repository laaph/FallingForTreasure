
using UnityEngine;

public class CreatePlatforms : MonoBehaviour
{
    public GameObject[] platformPrefabs;
    public GameObject platform48;
    public GameObject[] platformPool = new GameObject[10];
    public GameObject[] treasures;
    GameObject[] treasurePool = new GameObject[5];
    public GameObject turtle;
    GameObject[] turtlePool = new GameObject[5];
    CameraDrop cameraDropScript;

    Transform cameraT;
    float lastCameraChangePosY;
    float nextYPos; 


    // Start is called before the first frame update
    void Start()
    {
        GameObject holder = new GameObject();
        cameraT = Camera.main.transform;
        cameraDropScript = Camera.main.GetComponent<CameraDrop>();
        for(int i = 0; i < platformPool.Length; i++)
        {
            platformPool[i] = Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Length)]);
            platformPool[i].gameObject.transform.position = new Vector3(10f, 10f, 10f);
            platformPool[i].name = $"platform {i}";
            platformPool[i].transform.parent = holder.transform;
        }
        for(int i = 0; i < treasurePool.Length; i++)
        {
            treasurePool[i] = Instantiate(treasures[Random.Range(0, treasures.Length)]);
            treasurePool[i].gameObject.transform.position = new Vector3(10f, 10f, 10f);
            treasurePool[i].name = $"treasure {i}";
            treasurePool[i].transform.parent = holder.transform;
        }
        for (int i = 0; i < turtlePool.Length; i++)
        {
            turtlePool[i] = Instantiate(turtle);
            turtlePool[i].gameObject.transform.position = new Vector3(10f, 10f, 10f);
            turtlePool[i].name = $"turtle {i}";
            treasurePool[i].transform.parent = holder.transform;
        }
        // Set some initial platforms

        platformPool[0].transform.position = new Vector3(0, -4f, 0);

        for (int i = 3; i < 5; i++)
        {
            platformPool[i].transform.position = new Vector3(Random.Range(-4f, 4f), -2f * (float)i, 0);
        }
        nextYPos = -12f;
    }

    void FixedUpdate()
    {
        if(cameraT.position.y - lastCameraChangePosY < -2)
        {
            Debug.Log("Adding platform to screen");
            // Get a prefab that is off the screen
            GameObject p = GetObjectFromOffScreen(platformPool);
            if(p != null)
            {
                float ranX = Random.Range(-4f, 4f);
                p.transform.position = new Vector3(ranX, nextYPos, 0);
                float f = Random.Range(0f, 1f);
                if (f < 0.15f)
                {
                    GameObject g = GetObjectFromOffScreen(treasurePool);
                    if (g != null)
                    {
                        g.transform.position = new Vector3(ranX + Random.Range(-2f, 2f), nextYPos + 0.6f, 0);
                    }
                }
                if(f > 0.15f && f < 0.30f)
                {
                    GameObject g = GetObjectFromOffScreen(turtlePool);
                    if (g != null)
                    {
                        float offset = Random.Range(-1f, 1f);
                        g.transform.position = new Vector3(ranX + offset, nextYPos + 1.1f, 0);
                        g.GetComponent<TurtleBehaviorScript>().StartWalking(offset);
                    }
                }
                nextYPos = nextYPos + Random.Range(-2f, -4f);

            }
            lastCameraChangePosY = cameraT.position.y;
        }
    }

    private GameObject GetObjectFromOffScreen(GameObject[] gameObjects)
    {
        foreach(GameObject p in gameObjects)
        {
            if(cameraT.transform.position.y - p.transform.position.y < -10.4f)
            {
                Debug.Log($"Found {p}");
                return p;
            }
        }
        Debug.Log("Could not find object");
        return null;
    }
}
