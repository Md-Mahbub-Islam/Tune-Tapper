using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRrgbd;
    public float jumpAmount = 0;
    private bool canJump = false;

    public Sprite jumpingSprite;
    private Animator spriteAnimator;

    public AudioSource effectsSource;
    public AudioClip jump;
    public AudioClip land;

    public AudioSource audioSource;
    public AudioSource environmentAudioSource;
    public EnvinronmentController envinronmentController;
    public ParallaxBackground parallaxBackground;

    public Transform lastSavepoint;
    private bool respawning = false;
    private float originalBackgroundSpeed = 0;
    private float originalObjectSpeed = 0;

    public int collectedItemsCount = 0;
    public int collectedItemsFactor = 0;
    public GameObject[] collectedItems;
    public Sprite[] collectedItemsSprites;

    // Start is called before the first frame update
    void Start()
    {
        playerRrgbd = gameObject.GetComponent<Rigidbody2D>();
        spriteAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Jump, adjust jump amount from inspector, not from here
        if (Input.GetKeyDown("space") && canJump)
        {
            playerRrgbd.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
            canJump = false;
            effectsSource.PlayOneShot(jump);

        }
        if (canJump == false)
        {
            spriteAnimator.enabled = false;
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = jumpingSprite;
        }
        else if (spriteAnimator.enabled == false)
        {
            spriteAnimator.enabled = true;
        }

        if (respawning && Vector2.Distance(transform.position, lastSavepoint.position) < .5f)
        {
            respawning = false;
            envinronmentController.objectSpeed = originalObjectSpeed;
            parallaxBackground.parallaxEffectMultiplier = originalBackgroundSpeed;
            gameObject.GetComponent<Rigidbody2D>().simulated = true;
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
            audioSource.pitch = 1;
            environmentAudioSource.Play();

        }
        else if (respawning)
        {
            audioSource.pitch = Mathf.Clamp(audioSource.pitch - .1f * Time.deltaTime, -100, 1);
            envinronmentController.objectSpeed = Mathf.Clamp(envinronmentController.objectSpeed - (originalObjectSpeed * .1f * Time.deltaTime), -100, 1);
            parallaxBackground.parallaxEffectMultiplier = Mathf.Clamp(parallaxBackground.parallaxEffectMultiplier - (originalBackgroundSpeed * .1f * Time.deltaTime), -100, 1);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            if (canJump == false)
            {
                effectsSource.PlayOneShot(land);
            }
            canJump = true;
        }
        if (collision.gameObject.tag == "FallDeath")
        {
            canJump = false;
            originalBackgroundSpeed = parallaxBackground.parallaxEffectMultiplier;
            originalObjectSpeed = envinronmentController.objectSpeed;
            envinronmentController.objectSpeed = 0;
            parallaxBackground.parallaxEffectMultiplier = 0;
            audioSource.pitch = 0;
            environmentAudioSource.Pause();
            StartCoroutine(Respawn());
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Collectible")
        {
            collectedItems[collectedItemsCount].SetActive(true);
            collectedItemsCount++;
            if (collectedItemsCount % 5 == 0)
            {
                collectedItemsCount = 0;
                collectedItemsFactor++;
                for (int i = 0; i < collectedItems.Length; i++)
                {
                    collectedItems[i].GetComponent<SpriteRenderer>().sprite = collectedItemsSprites[collectedItemsFactor];
                    if (i != 0)
                    {
                        collectedItems[i].SetActive(false);
                    }
                }
            }
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2);
        respawning = true;
        transform.position = new Vector2(transform.position.x, lastSavepoint.position.y);
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;


        parallaxBackground.parallaxEffectMultiplier = originalBackgroundSpeed * -1;
        envinronmentController.objectSpeed = originalObjectSpeed * -1;
        audioSource.pitch = -1;

        yield return null;
    }

}
