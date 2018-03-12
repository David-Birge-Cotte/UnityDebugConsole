using System.Collections;
using UnityEngine;

/*
	The CommandManager class
	Contains the implementation of each command and its mapping regarding to DebugCommand
*/
public class CommandManager : MonoBehaviour {

	// ------------------------------- Variables ------------------------------
	public static CommandManager singleton;

	// ---------------------------- Unity Functions ---------------------------
	// is used for singleton setup
	private void Awake ()
	{
		if (CommandManager.singleton != null)
			Destroy(this.gameObject);
		else
			singleton = this;
	}

	// -------------------------- Map Commands Here ---------------------------
	// maps all commands to their function
	public void SendCommand(DebugCommand _cmd)
	{
		if (_cmd.command == "/quit" || _cmd.command == "/exit")
			QuitGame();
		if (_cmd.command == "/timescale")
			TimeScale(_cmd.args);
		// if (_cmd.command == "/your_command")
		//	call_corresponding_function(_cmd.args)
	}

	// ----------------------- Implement Commands Here ------------------------
	// function that changes the timescale
	private void TimeScale(string[] _args)
	{
		float newTimeScale = Time.timeScale;
		float.TryParse(_args[0], out newTimeScale);
		Time.timeScale = newTimeScale;
	}

	// function that exits the game
	private void QuitGame()
	{
		Application.Quit();
	}

	// ------------------------------------------------------------------------
}
