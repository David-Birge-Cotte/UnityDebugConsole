using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PrintMsgType
{
	NORMAL,
	ERROR,
	COMMAND
}

public class DebugConsole : MonoBehaviour {

	public bool			b_consoleOpened;

	[Header("Prefabs")]
	public GameObject	TextBox;
	[Header("Children")]
	public Transform	TextContainer;
	public Transform	Container;
	public InputField	InputField;
	
	void Start()
	{
		InputField.onEndEdit.AddListener(delegate {SendCmd(InputField); });
		OpenConsole(b_consoleOpened);
	}

	void Update ()
	{
		// alt + tab
		if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Tab))
			ToggleConsole();
	}

	private void ToggleConsole()
	{
		b_consoleOpened = !b_consoleOpened;
		OpenConsole(b_consoleOpened);
	}

	private void OpenConsole(bool bOpen)
	{
		Container.gameObject.SetActive(b_consoleOpened);
		
		// Select to type
		if(b_consoleOpened)
		{
			InputField.Select();
			InputField.ActivateInputField();
		}
	}

	public void	PrintMsg(string msg, PrintMsgType type = PrintMsgType.NORMAL)
	{
		Text msgBox = Instantiate(TextBox, Vector3.zero, Quaternion.identity, TextContainer).GetComponent<Text>();
		msgBox.text = msg;
		switch (type)
		{
			case PrintMsgType.COMMAND:
				msgBox.color = Color.green;
				break;
			case PrintMsgType.ERROR:
				msgBox.color = Color.red;
				break;
			default:
				msgBox.color = Color.white;
				break;
		}
	}

	private void SendCmd(InputField input)
	{
		// get the passed text
		string text = input.text;
		if (text == "")
			return;

		input.text = "";

		if (text.StartsWith("/"))
		{
			ExecuteCmd(text.Substring(1));
			PrintMsg(text, PrintMsgType.COMMAND);
		} 
		else
			PrintMsg(text);

		// Put back control on the input field
		input.Select();
		input.ActivateInputField();
	}

	private void ExecuteCmd(string cmd)
	{
		Debug.Log("command: " + cmd);
	}
}
