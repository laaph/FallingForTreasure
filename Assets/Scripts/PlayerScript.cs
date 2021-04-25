using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    public float maxSpeed = 1f;
    float speed = 0;
    Transform cameraT;
    AudioSource audioS;
    public AudioClip deathTune;
    public AudioClip[] pickupTune;
    bool dead = false;
    GameObject musicHoldingObject;
    int score = 0;
    public Text scoreText;
    public SpriteRenderer background1;
    public SpriteRenderer background2;
    public GameObject bit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        cameraT = Camera.main.transform;
        audioS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        if(h > 0.1f)
        {
            speed += (maxSpeed/4f);
            sr.flipX = false;
        }
        if(h < -0.1f)
        {
            speed -= (maxSpeed / 4f);
            sr.flipX = true;
        }
        if(Mathf.Abs(h) < 0.1f)
        {
            speed /= 1.2f;
        }
        transform.Translate(speed * Time.deltaTime, 0, 0);

        if (Mathf.Abs(cameraT.position.y - transform.position.y) > 10.4f && dead == false)
        {
            StartCoroutine(DeathRoutine());

        }
    }

    IEnumerator DeathRoutine()
    {
        Camera.main.gameObject.GetComponent<CameraDrop>().enabled = false;
        Camera.main.gameObject.GetComponent<AudioSource>().Stop();
        sr.enabled = false;
        // blood animations?
        for (int i = 0; i < 10; i++)
        {
            // Probably should make these in Start(), but what the hell
            GameObject b = Instantiate(bit);
            b.transform.position = transform.position;
            b.GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(-5f, 5f), Random.Range(0f, 5f));
        }
        audioS.clip = deathTune;
        audioS.Play();
        dead = true;
        yield return new WaitForSeconds(2f);
        PlayerPrefs.SetInt("Score", score);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag.Equals("Treasure"))
        {
            c.gameObject.transform.position = new Vector3(10f, 10f, 10f);
            IncrementScore();
        }
        if (c.tag.Equals("Enemy"))
        {
            if (!dead)
            {
                StartCoroutine(DeathRoutine());
            }
        }
    }

    private void IncrementScore()
    {
        score++;
        scoreText.text = $"Score: {score}";
        audioS.clip = pickupTune[Random.Range(0, pickupTune.Length)];
        audioS.Play();
        Camera.main.gameObject.GetComponent<CameraDrop>().speed += 0.005f;
        background1.color = new Color(1f, 1f - (0.01f * score), 0.8f);
        background2.color = new Color(1f, 1f - (0.01f * score), 0.8f);
        maxSpeed += 0.01f;

    }

}
