using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
	The DebugConsole class
	Contains the implementation of the a Unity UI Gameobject to send debug commands
*/
public class DebugConsole : MonoBehaviour {

	// ------------------------------- Variables ------------------------------
	public int maxMsg = 6;
	// Toggle in inspector to choose initial state
	public bool b_consoleOpened;

	[Header("Prefabs")]
	public GameObject TextBox;
	[Header("Children")]
	public Transform TextContainer;
	public Transform Container;
	public InputField InputField;

	// the list of the previous messages
	private List<GameObject> messages;
	
	// ---------------------------- Unity Functions ---------------------------
	// is used for setup
	private void Start()
	{
		messages = new List<GameObject>();
		InputField.onEndEdit.AddListener(delegate {SendCmd(InputField); });
		OpenConsole(b_consoleOpened);
	}

	// is used to listen to the keyboard shortcut
	private void Update ()
	{
		// alt + tab
		if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Tab))
			ToggleConsole();
	}

	// -------------------------- Private Functions ---------------------------
	// switches the current state of the console then calls to open / close it
	private void ToggleConsole()
	{
		b_consoleOpened = !b_consoleOpened;
		OpenConsole(b_consoleOpened);
	}

	// open or close the console
	private void OpenConsole(bool _bOpen)
	{
		Container.gameObject.SetActive(b_consoleOpened);
		
		// Select to type
		if(b_consoleOpened)
		{
			InputField.Select();
			InputField.ActivateInputField();
		}
	}

	// gets called when the users validates send the text from the input field
	private void SendCmd(InputField _input)
	{
		// get the passed text
		string text = _input.text;
		// verify it's not empty
		if (text == "")
			return;

		// if it's a command
		if (text.StartsWith("/"))
			ExecuteCmd(text);
		else
			PrintMsg(text);

		// reset the input
		_input.text = "";
		// Put back control on the input field
		_input.Select();
		_input.ActivateInputField();
	}

	// prints then calls the CommandManager with the requested command
	private void ExecuteCmd(string _msg)
	{
		PrintMsg(_msg, true);
		CommandManager.singleton.SendCommand(new DebugCommand(_msg));
	}

	// --------------------------- Public Functions ---------------------------
	// prints a message into the box. Changes color if isCmd
	public void	PrintMsg(string _msg, bool _isCmd = false)
	{
		Text msgBox = Instantiate(TextBox, Vector3.zero, Quaternion.identity, TextContainer).GetComponent<Text>();
		msgBox.text = _msg;
		messages.Add(msgBox.gameObject);
		if (_isCmd)
			msgBox.color = Color.green;
		if (messages.Count > maxMsg)
		{
			GameObject m = messages[0];
			messages.Remove(m);
			Destroy(m);
		}
	}

	// ------------------------------------------------------------------------
}