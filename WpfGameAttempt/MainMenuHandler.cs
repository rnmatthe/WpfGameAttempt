using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using WpfGameAttempt.CommandsNamespace;

namespace WpfGameAttempt
{
    class MainMenuHandler : Commandable
    {
        public MainMenuHandler() {
            lib = new CommandLibrary();
            validCommands.Add(lib.options);
            validCommands.Add(lib.create);
        }

        private string defaultText = "Welcome to the main menu!\nFrom here you can do all sorts of things!";
        private string currentText;
        private ArrayList archivedInput;
        private Command archivedCommand;
        private CommandLibrary lib;

        public string getText()
        {
            if(currentText != null)
            {
                return currentText;
            }
            return defaultText;
        }

        public void acceptInput(string input)
        {
            currentText = null;
            input = input.Trim();

            ArrayList brokenInput = new ArrayList(input.Split());

            Command command;

            //if there's an archived command:
            if(archivedCommand != null)
            {
                command = archivedCommand;
            } else
            {
                command = identifyCommand(brokenInput[0].ToString());
                //you must have enetered the name of a command, so remove that from the input
                brokenInput.RemoveAt(0);
            }

            //if you entered a command that doesn't seek input but provide(d) input
            //if you didn't enter a command and there isn't an archived one
            if(command == null || (!command.seeksInput && brokenInput.Count > 0) || (!command.seeksInput && archivedInput != null))
            {
                currentText = "Not valid input.";
            }

            if(command == lib.options)
            {
                optionsCommand();
            } else if (command == lib.create)
            {
                createCommand(brokenInput);
            }
        }

        public void createCommand(ArrayList input)
        {
            InputIterator iterator = new InputIterator(archivedInput, input);
            archivedCommand = lib.create;

            currentText = "What would you like to create? Your options include: \n- character\n- dungeon";
            if (iterator.hasNext())
            {
                switch (iterator.next())
                {
                    case "character":
                        createCharacter(iterator);
                        return;
                    case "dungeon":
                        return;
                    case "quit":
                        quit();
                        return;
                    default:
                        currentText += "\nNot a valid option.";
                        archivedInput = iterator.getWalkedMinusOne();
                        return;
                }
            } else
            {
                archivedInput = iterator.getWalked();
            }

        }

        public void createCharacter(InputIterator iterator)
        {
            currentText = "Please enter a name for your character.";
            string name;
            if (iterator.hasNext())
            {
                name = iterator.next();
                if(name == "quit")
                {
                    quit();
                    return;
                }
            } else
            {
                archivedInput = iterator.getWalked();
                return;
            }

            currentText = "Please enter your character's class. Your options are: \n- mage \n- warrior \n- hunter";

            if (iterator.hasNext())
            {
                switch (iterator.next())
                {
                    case "mage":
                        break;
                    case "warrior":
                        break;
                    case "hunter":
                        break;
                    case "quit":
                        quit();
                        return;
                    default:
                        currentText += "\nNot a valid option.";
                        archivedInput = iterator.getWalkedMinusOne();
                        return;
                }
            } else
            {
                archivedInput = iterator.getWalked();
                return;
            }
            
        }

        public void optionsCommand()
        {
            currentText = "options command";
            archivedCommand = null;
        }



        public void quit()
        {
            currentText = null;
            archivedCommand = null;
            archivedInput = null;
        }

        
        
    }
}
