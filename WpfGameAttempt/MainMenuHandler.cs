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

            Command command = identifyCommand(brokenInput);

            iterator = new InputIterator(archivedInput, brokenInput);

            if (command == lib.options)
            {
                optionsCommand();
            } else if (command == lib.create)
            {
                createCommand();
            }
        }

        public void createCommand()
        {
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
