using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController
{
	private textType texttype;
	public Text textComponent;

	public TextController(textType texttype){
		this.texttype = texttype;
        
        switch(this.texttype)
		{
			case textType.main:
				this.textComponent = GameObject.Find("Canvas/main_Text").GetComponent<Text>();
				break;

			case textType.score:
				this.textComponent = GameObject.Find("Canvas/UpperInformation/score_Text").GetComponent<Text>();
				break;

			case textType.remainingtime:
            	this.textComponent = GameObject.Find("Canvas/UpperInformation/remainingTime_Text").GetComponent<Text>();
				break;

			case textType.announce:
				this.textComponent = GameObject.Find("Canvas/announce_Text").GetComponent<Text>();
				break;

            case textType.gameover:
                this.textComponent = GameObject.Find("Canvas/gameover_Text").GetComponent<Text>();
                break;
        }

        

        this.textComponent.text = "";

	}
    

 	
}


public enum textType
{
    main,
    score,
    remainingtime,
    announce,
    gameover
}

public enum textColor
{
    white,
    red,
    yellow,
    green
}
