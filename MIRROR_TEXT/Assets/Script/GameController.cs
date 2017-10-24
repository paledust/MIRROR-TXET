using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public TextMesh displayText;
    public InputAction[] inputActions;

    List<string> text = new List<string>();

    // Use this for initialization
    void Awake () {

    }

    void Start(){
		LogInText(displayText.text);
        DisplayConversationText ();
        DisplayLoggedText ();
    }

    public void DisplayConversationText(){

    }

    void UnpackConversation(){
    }

    void ClearCollectionsForNewConversation(){
    }

	public void DisplayLoggedText(){
		Debug.Log("Display");
		string logAsText = string.Join("\n", text.ToArray());
		if(displayText != null){
			displayText.text = logAsText;
		}
	}
	public void LogInText(string logingText){
		text.Add(logingText + "\n");
	}
	public void LogInText(string logingText, int i){
		text.Add("<material=" + i.ToString() + ">" + logingText + "</material>" + "\n");
	}
	public void LogInText_Line(string logingText, int i = 3, int MAT = 0){
		string new_text = logingText;
		int space = new_text.Length/i;
		string SpaceText = CreateSpace(space);

		for(int j = 0; j<i;j++){
			if(space*(1+(i-1)*j) < new_text.Length){
				new_text = new_text.Insert(space*(1+(i-1)*j),"\n"+SpaceText);
				SpaceText += CreateSpace(space);
			}
		}

		LogInText(new_text, MAT);
	}
	public void LogInText_Space(string logingText, int i = 3, int MAT = 0){
		string new_text = logingText;
		int space = new_text.Length/i;
		string SpaceText = CreateSpace(space);

		for(int j = 0; j<i;j++){
			if(space*(1+2*j) < new_text.Length){
				new_text = new_text.Insert(space*(1+2*j), SpaceText);
				SpaceText += CreateSpace(space);
			}
		}
		LogInText(new_text, MAT);
	}
	string CreateSpace(int num){
		string Space = "";
		for(int i = 0; i<num; i++){
			Space += " ";
		}
		return Space;
	}
}