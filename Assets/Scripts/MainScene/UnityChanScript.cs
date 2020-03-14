using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UnityChanScript : MonoBehaviour
{
    //ユニティちゃんステータス
    private float speed = 0.2f;
    public float jump_power = 700;
    private float gravity = 3.0f;

    private Rigidbody2D rg;
    private Animator animator;
    public LayerMask ground_layer;
    public GameObject gameController;
    private SpriteRenderer sr;

    //判定系
    private bool isGround;


    //フィールドスクリプトに渡す用のスピードの変数
    [HideInInspector] public float move_speed_UCS = 1.8f;

    //弾
    public GameObject bullet;

    //経過時間
    private float ElapseTime;

    //点滅
    private float flashTime;
    private bool isDamasing = false;

    //UI
    private TextController speedup_text;
    private TextController speeddown_text;
    private TextController gameover_text;
    private changeText CTS;

    private void Awake()
    {
        //UI
        speedup_text = new TextController(textType.announce);
        speeddown_text = new TextController(textType.announce);
        gameover_text = new TextController(textType.gameover);
        CTS = GetComponent<changeText>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        animator.SetFloat("Speed", speed);

        Invoke("StartRunning", 3.0f);   
    }




    void Update()
    {
        ElapseTime += Time.deltaTime;

        //着地してるか判定
        isGround = Physics2D.Linecast(
        transform.position + transform.up * 1,
        transform.position - transform.up * 0.05f,
        ground_layer);
        if (ElapseTime <= 3.0f) {
            var temp = transform.position;
            temp.y = 1.7f;
            transform.position = temp;
            //animator.SetBool("Start", true);
        }
        if (Input.GetKeyDown("space")) { 
            if (isGround)
            {
                animator.SetBool("Dash",false);
                animator.SetTrigger("Jump");

                isGround = false;

                rg.AddForce(Vector2.up * jump_power);
            
            }
        }
       
        //ジャンプ中の判定
        float velY = rg.velocity.y;
        bool isJumping = velY > 0.1f ? true:false;
        bool isFalling = velY < -0.1f ? true:false;

        
        animator.SetBool("isJumping",isJumping);
        animator.SetBool("isFalling",isFalling);
        //

        //射撃
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            animator.SetTrigger("Shot");
            Instantiate(bullet, this.transform.position + new Vector3(0f, 1.2f,0f), Quaternion.identity);
        }


        //ゲームオーバー処理
        if (this.transform.position.y <= -4.0f)
        {
            GameOver();
            Invoke("ToResult", 1.0f);
            rg.gravityScale = 30f;
            GetComponent<BoxCollider2D>().enabled = false;
        }

        //点滅処理
        if (isDamasing == true)
        {   
                float level = Mathf.Abs(Mathf.Sin(Time.deltaTime * 10));
                sr.color = new Color(1f, 1f, 1f, level); 
        }

    }



    



    private void OnTriggerEnter2D(Collider2D collision)
    {   


        if(collision.gameObject.tag == "onigiri")
        {
            Destroy(collision.gameObject);
            collision.gameObject.tag = "not_onigiri";

            //加速
            speed += 0.2f;
            animator.SetFloat("Speed",speed);

            gravity += 0.2f;
            rg.gravityScale = gravity;

            move_speed_UCS += 1.8f;
            //

            //UI
            CTS.change_Text(speedup_text.textComponent, "スピードアップ!");
            CTS.change_color(speedup_text.textComponent,textColor.yellow);
            StartCoroutine(CTS.FalseUI(2.0f, speedup_text.textComponent));

        }

        if (collision.gameObject.tag == "bomb" || collision.gameObject.tag == "rock")
        {
            Destroy(collision.gameObject);

            StartCoroutine("WaitFlash");//点滅用コルーチン

            //減速
            speed -= 0.4f;
            if(speed < 0.2f)
            {
                speed = 0.2f;
            }
            animator.SetFloat("Speed", speed);

            gravity -= 0.2f;
            if(gravity < 3.0f)
            {
                gravity = 3.0f;
            }
            collision.gameObject.tag = "not_bomb";


            move_speed_UCS -= 3.6f;
            if(move_speed_UCS < 1.8f)
            {
                move_speed_UCS = 1.8f;
            }
            //

            //UI
            CTS.change_Text(speeddown_text.textComponent, "スピードダウン…");
            CTS.change_color(speeddown_text.textComponent, textColor.red);
            StartCoroutine(CTS.FalseUI(2.0f, speeddown_text.textComponent));
        }

        

    }


    //点滅用
    IEnumerator WaitFlash()
    {
        isDamasing = true;
        yield return new WaitForSeconds(1);

        //戻す
        isDamasing = false;
        sr.color = new Color(1f, 1f, 1f, 1f);
    }

    //アニメーションスタート
    private void StartRunning()
    {
        animator.SetBool("Dash", true);
    }


    //リザルトシーンへ
    private void ToResult()
    {
        SceneManager.LoadScene("resultScene");
    }

    //ゲームオーバー処理
    private void GameOver()
    {

        CTS.change_Text(gameover_text.textComponent, "GAME\nOVER");
        gameController.GetComponent<GameController>().startFlag = 2;

    }

    
}
