namespace Cession.Commands
{
	using System;
	using System.Collections.Generic;

	public class MacroCommand:Command
	{
		private List<Command> commands = new List<Command>();
		public MacroCommand ()
		{
		}

		public MacroCommand(IEnumerable<Command> commands)
		{
			this.commands.AddRange(commands);
		}

		public MacroCommand(Command command)
		{
			commands.Add (command);
		}

		public void AddCommand(Command command)
		{
			commands.Add(command);
		}

		public void AddRange(IEnumerable<Command> commands)
		{
			this.commands.AddRange (commands);
		}

		public override void Execute ()
		{
			commands.ForEach(c => c.Execute());
		}

		public override void Undo()
		{
			for (int i = commands.Count - 1; i >= 0; i--) {
				commands[i].Undo();
			}
		}
	}
}

