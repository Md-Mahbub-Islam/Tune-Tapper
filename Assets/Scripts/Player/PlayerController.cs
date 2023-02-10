using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRrgbd;
    public float jumpAmount = 0;
    private bool canJump = false;

    //variable for text mesh
    public GameObject textMesh;
    

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

        // Get the current position of the player
        Vector3 position = transform.position;
        Debug.Log(position);

        // if player y is less than -20, then game over
        //enable text mesh
        if (position.y < -20)
        {
            textMesh.SetActive(true);
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
