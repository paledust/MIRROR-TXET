using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TypeIn : MonoBehaviour {
	[SerializeField] InputField inputField;
	[SerializeField] DisplayText display;
	// Use this for initialization
	void Start () {
		inputField.onEndEdit.AddListener(AcceptStringInput);
	}
	void AcceptStringInput(string userInput){
		userInput = userInput.ToLower();
		if(userInput == "reset"){
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		int method = Random.Range(0,4);
		switch (method)
		{
			case 0:
				display.LogInText(userInput,2);
				break;
			case 1:
				display.LogInText(userInput,0);
				break;
			case 2:
				display.LogInText_Line(userInput,3);
				break;
			case 3:
				display.LogInText_Space(userInput,3);
				break;
		}

		InputComplete();
	}
	void InputComplete(){
		display.DisplayLoggedText ();
        inputField.ActivateInputField ();
        inputField.text = null;
	}
}
