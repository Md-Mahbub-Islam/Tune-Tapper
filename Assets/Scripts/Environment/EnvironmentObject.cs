using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentObject : MonoBehaviour
{
    private Rigidbody2D objectrgbd;
    private EnvinronmentController ec;

    // Start is called before the first frame update
    void Start()
    {
        ec = GameObject.Find("EnvironmentController").GetComponent<EnvinronmentController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * ec.objectSpeed);
    }
}
