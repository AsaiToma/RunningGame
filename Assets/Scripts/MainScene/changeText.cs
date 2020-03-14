using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeText : MonoBehaviour
{
	public void change_Text(Text info_text, string message)
	{

		info_text.text = message;

	}

    public void change_color(Text announce,textColor textcolor)
    {
        switch (textcolor)
        {
            
            case textColor.red:
                announce.color = new Color(255f / 255f, 0f / 255f, 0f / 255f);
                break;

            case textColor.yellow:
                announce.color = new Color(255f / 255f, 255f / 255f, 0f / 255f);
                break;

            case textColor.green:
                announce.color = new Color(0f / 255f, 255f / 255f, 0f / 255f);
                break;
        }
    }

	public IEnumerator FalseUI(float deleteTime, Text deleteText)
	{
		yield return new WaitForSeconds(deleteTime);

		deleteText.text = "";
	}
}
