using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartControllerScript : MonoBehaviour
{

    public GameObject field;
    public GameObject tips;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //フィールドをループさせる
        collision.transform.position = new Vector3(19.0f - 0.1f, -1.0f, 0.0f);
        
    }

    public void push_Start()
    {
        SceneManager.LoadScene("mainScene");
    }

    public void push_explain()
    {
        tips.SetActive(true);
    }

    public void push_close()
    {
        tips.SetActive(false);
    }
}
