/*
	The DebugCommand struct
	Is used to store the command as a string and the arguments in a string array
*/
public struct DebugCommand
{
	// ------------------------------- Variables ------------------------------
	public string command;
	public string[] args;

	// ------------------------------ Constructor -----------------------------
	public DebugCommand(string text)
	{
		string[] textSplit = text.Split(' ');
		command = textSplit[0];
		int argsNb = textSplit.Length - 1;
		args = new string[argsNb];
		for (int i = 0; i < argsNb; i++)
			args[i] = textSplit[i + 1];
	}
}