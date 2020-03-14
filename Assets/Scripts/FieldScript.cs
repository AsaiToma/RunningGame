using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldScript : MonoBehaviour
{
    private Rigidbody2D rg;

    //ユニティちゃん系
    private GameObject UnityChan;
    private UnityChanScript UCS;

    
    [HideInInspector]
    public float move_speed = 1.8f;

    // Start is called before the first frame update
    void Start()
    {
        UnityChan = GameObject.Find("UnityChan");
        UCS = UnityChan.GetComponent<UnityChanScript>();
        
        rg = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (UCS.move_speed_UCS != 0)
        {
            //UnityChanScriptから動くスピード取得
            move_speed = UCS.move_speed_UCS;
        }
        else { move_speed = 1.8f; }

    }

    private void FixedUpdate()
    {
        rg.velocity = new Vector3(-move_speed, 0, 0);
    }
}
