using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private GameObject player;


    private Rigidbody2D rg;

    private float bullet_speed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rg = GetComponent<Rigidbody2D>();

        rg.velocity = new Vector3(bullet_speed, 0f, 0f);

        Destroy(gameObject,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}
