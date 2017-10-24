using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TypeIn : MonoBehaviour {
	[SerializeField] InputField inputField;
	[SerializeField] DisplayText display;
	GameController controller;
	int MaxDialogAllow;
	protected int NANA_NUM;
	protected int PAUL_NUM;
	// Use this for initialization
	void Start () {
		NANA_NUM = 0;
		PAUL_NUM = 0;
		MaxDialogAllow = 3;
		inputField.onEndEdit.AddListener(AcceptStringInput);
		controller = FindObjectOfType<GameController>();
	}
	void AcceptStringInput(string userInput){
		userInput = userInput.ToLower();
		if(userInput == "reset"){
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		int method = Random.Range(0,6);
		int flag = Random.Range(0,2);
		if(NANA_NUM >= MaxDialogAllow){
			flag = 0;
			while(method == 1){
				method = Random.Range(0,6);
			}
		}
		if(PAUL_NUM >= MaxDialogAllow){
			flag = 1;
			while(method == 0){
				method = Random.Range(0,6);
			}
		}

		switch (method)
		{
			case 0:
				controller.LogInText(userInput,0);
				PAUL_NUM ++;
				NANA_NUM = 0;
				break;
			case 1:
				controller.LogInText(userInput,2);
				PAUL_NUM = 0;
				NANA_NUM ++;
				break;
			case 2:
				if(flag == 1){
					PAUL_NUM = 0;
					NANA_NUM ++;
					flag = 2;
				}
				else{
					PAUL_NUM ++;
					NANA_NUM = 0;
				}
				controller.LogInText_Line(userInput,Random.Range(3,6), flag);
				break;
			case 3:
				if(flag == 1){
					PAUL_NUM = 0;
					NANA_NUM ++;
					flag = 2;
				}
				else{
					PAUL_NUM ++;
					NANA_NUM = 0;
				}
				controller.LogInText_Space(userInput,Random.Range(3,6), flag);
				break;
			case 4:
				controller.LogInText_Space(userInput,Random.Range(3,6), 3);
				break;
			case 5:
				controller.LogInText_Line(userInput,Random.Range(3,6), 3);
				break;
		}

		InputComplete();
	}
	void InputComplete(){
		controller.DisplayLoggedText ();
        inputField.ActivateInputField ();
        inputField.text = null;
	}
}
