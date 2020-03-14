using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;









public class GameController : MonoBehaviour
{

    //ゲームオブジェクト
    [SerializeField, NamingArrayAttribute(new string[] { "speedup_item", "bomb_item"})]
    private GameObject[] objects = new GameObject[2];
    [SerializeField, NamingArrayAttribute(new string[] { "centerHole", "leftRock","doubleHole","centerRock" })]
    public GameObject[] field = new GameObject[4];

    private Animator _animator;

    //時間管理
    private float timeElapsed;
    private float time_bombmake = 10.0f;
    private float time_countdown = 3.0f;
    [HideInInspector]
    public int startFlag = 0;

    //unitychan
    private GameObject UnityChan;
    private UnityChanScript UCS;

    //スコア
    private float score;

    //時間
    private float count = 3.0f;
    private float remainingTime = 45f;
    public float formerTime = 45f; 
	private float totalTime;
	private float startTime;


    //テキスト
    private TextController main_text;
    private TextController score_text;
    private TextController remainingtime_text;
    private TextController plustime_text;
    private changeText CTS;

    private void Awake()
    {
        //UI
        main_text = new TextController(textType.main);
        score_text = new TextController(textType.score);
        remainingtime_text = new TextController(textType.remainingtime);
        plustime_text = new TextController(textType.announce);
        CTS = GetComponent<changeText>();

        //ユニティちゃん
        UnityChan = GameObject.Find("UnityChan");
        UCS = UnityChan.GetComponent<UnityChanScript>();
    }

    // Start is called before the first frame update
    void Start()
    {

        //フィールド生成
        for (int i = 3; i < 200; i++) {
            int field_value = Random.Range(0, field.Length);
            GameObject newField = Instantiate(field[field_value], new Vector3(19f * i, 0, 0), Quaternion.identity) as GameObject;

            //おにぎり生成
            var parent = newField.transform; // おにぎりをフィールドの子要素にする
            int onigiri_probability = Random.Range(1, 5);
            if (onigiri_probability == 1)
            {
                Instantiate(objects[0], new Vector3(19f * i  -0.5f, 0, 0), Quaternion.identity, parent);
                timeElapsed = 0.0f;
            }
            
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        //時間管理
        timeElapsed += Time.deltaTime;
		totalTime += Time.deltaTime;
        
        

        //スタートまでのカウントダウン
        if (timeElapsed >= time_countdown)
        {

            startFlag = 1;
			startTime = timeElapsed;
			time_countdown = 100.0f;

            //UI系
            CTS.change_Text(main_text.textComponent, "Start!");
            StartCoroutine(CTS.FalseUI(0.8f, main_text.textComponent));
            

            //filedScript有効に
            GameObject[] fields = GameObject.FindGameObjectsWithTag("field");
            foreach (GameObject x in fields)
            {
                x.GetComponent<FieldScript>().enabled = true;
            }

        }
        else if(startFlag == 0)
        {
            count -= Time.deltaTime;
            CTS.change_Text(main_text.textComponent, count.ToString("F0"));
        }



        //爆弾生成
        if (timeElapsed >= time_bombmake && startFlag == 1)
        {
            Instantiate(objects[1], new Vector3(30f, -1.6f, 0), Quaternion.identity);
            timeElapsed = 0.0f;
        }


        //タイムアップ
        if(remainingTime <= 0.0f)
        {
            TimeUp();
        }
    }

    private void FixedUpdate()
    {
        if (startFlag == 1)
        {
            //スコアUI
            score += UCS.move_speed_UCS / 10;
            int score_int = Mathf.FloorToInt(score);
            CTS.change_Text(score_text.textComponent, (score_int).ToString());

            //残り時間UI
			remainingTime = formerTime - totalTime + startTime;
            CTS.change_Text(remainingtime_text.textComponent, (remainingTime).ToString("F1"));

            //データ保持
            Data.Instance.finalScore = score;
            Data.Instance.totalRunningTime = formerTime - remainingTime;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        //不要になったフィールドを消去
        if (collision.gameObject.tag == "field") {
            Destroy(collision.gameObject);
        }
    }



    

   　private void TimeUp()
    {
        startFlag = 2;

        GameObject[] fields = GameObject.FindGameObjectsWithTag("field");
        foreach (GameObject x in fields)
        {
            x.GetComponent<FieldScript>().enabled = false;
        }
        UCS.enabled = false;
        UnityChan.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        CTS.change_Text(main_text.textComponent, "Time UP!!");

        Invoke("ToResultScene", 1.0f);
    }

    private void ToResultScene()
    {
        SceneManager.LoadScene("resultScene");
    }

    //爆弾破壊時のUI
    public void PlusTimeUI()
    {
        //UI
        CTS.change_Text(plustime_text.textComponent, "+3秒!");
        CTS.change_color(plustime_text.textComponent, textColor.green);
        StartCoroutine(CTS.FalseUI(2.0f,plustime_text.textComponent));
    }

    
}
