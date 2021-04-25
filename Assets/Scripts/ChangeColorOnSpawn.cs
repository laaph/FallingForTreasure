using UnityEngine;

public class ChangeColorOnSpawn : MonoBehaviour
{
    public bool bright;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (bright)
        {
            float a = Random.Range(0f, 1f);

            switch (Random.Range(0, 3))
            {
                case 0:
                    sr.color = new Color(a, 1f, a);
                    break;
                case 1:
                    sr.color = new Color(1f, a, a);
                    break;
                case 2:
                    sr.color = new Color(a, a, 1f);
                    break;
            }

        }    
        else
        {
            sr.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
    }
}
