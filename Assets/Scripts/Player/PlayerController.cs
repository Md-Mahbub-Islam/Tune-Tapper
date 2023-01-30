using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRrgbd;
    public float jumpAmount = 0;
    private bool canJump = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRrgbd = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Jump, adjust jump amount from inspector, not from here
        if (Input.GetKeyDown("space") && canJump)
        {
            playerRrgbd.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
            canJump = false;

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            canJump = true;
        }
    }
}
