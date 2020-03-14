using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombScript : MonoBehaviour
{
    public GameObject explosion;
    private GameObject gameController;
    private GameController GHS;
    private Rigidbody2D rg;

    private int bomb_speed ;

    

    // Start is called before the first frame update
    void Start()
    {
        //爆弾を動かす
        rg = GetComponent<Rigidbody2D>();
        bomb_speed = Random.Range(3, 7);
        rg.velocity = new Vector3(-bomb_speed, 0f, 0f);

        //残り時間を追加する準備
        gameController = GameObject.Find("GameController");
        GHS = gameController.GetComponent<GameController>();

        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "bullet")
        {
            


            Destroy(gameObject);
            Destroy(collision.gameObject);

            //爆発
            Instantiate(explosion, this.transform.position, Quaternion.identity);

            //残り時間追加
            GHS.formerTime += 3.0f;
            GHS.PlusTimeUI();
            
        }

        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            //爆発
            Instantiate(explosion, this.transform.position , Quaternion.identity);

        }
    }

    
}
