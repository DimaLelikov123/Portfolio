using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zootopia
{
    public abstract class ListCommands
    {
        public List<string> commands = new List<string>();
        public abstract void fillCommands();
    }

    public class Commands : ListCommands
    {
        public override void fillCommands()
        {
            return;
        }
    }

    public abstract class CommandsDecorator : ListCommands
    {
        protected ListCommands command;

        public CommandsDecorator(ListCommands command)
        {
            this.command = command;
        }

        public void setCommands(ListCommands command)
        {
            this.command = command;
        }

        public override void fillCommands()
        {
            if (command != null)
            {
                command.fillCommands();
                commands.AddRange(command.commands);
            }
        }
    }

    public class Medical_commands : CommandsDecorator
    {
        public Medical_commands(ListCommands command) : base(command) { }
        public override void fillCommands()
        {
            base.fillCommands();
            commands.Add("Treat animal");
        }
    }

    public class Feeder_commands : CommandsDecorator
    {
        public Feeder_commands(ListCommands command) : base(command) { }

        public override void fillCommands()
        {
            base.fillCommands();
            commands.Add("Feed animal");
        }
    }

    public class Cleaner_commands : CommandsDecorator
    {
        public Cleaner_commands(ListCommands command) : base(command) { }

        public override void fillCommands()
        {
            base.fillCommands();
            commands.Add("Clean the enclosure");
        }
    }
}
