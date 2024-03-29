﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace WpfGameAttempt
{
    namespace CommandsNamespace
    {
        public class Command
        {
            public string name;
            public ArrayList validAlias;
            public bool seeksInput;

            public Command(string commandName, bool willWantInput)
            {
                seeksInput = willWantInput;
                name = commandName;
                validAlias = new ArrayList();
                validAlias.Add(name);
            }

            public void addAlias(string alias)
            {
                validAlias.Add(alias);
            }

            public bool identify(string str)
            {
                foreach (string alias in validAlias)
                {
                    if (str.Contains(alias) && str.Length == alias.Length)
                    {
                        return true;
                    }
                }
                return false;
            }

        }

        //------------------------------------------------------------------------------

        public class CommandLibrary
        {
            public Command options;
            public Command create;

            public CommandLibrary()
            {
                if (options == null)
                {
                    options = new Command("options", false);
                    options.addAlias("op");
                    options.addAlias("ops");

                    create = new Command("create", true);
                    create.addAlias("c");
                }
            }
        }

        //-------------------------------------------------------------------------------

        public class Commandable
        {

            public ArrayList validCommands;
            public ArrayList archivedInput;
            public Command archivedCommand;
            public CommandLibrary lib;
            public InputIterator iterator;

            public Commandable()
            {
                validCommands = new ArrayList();
            }


            public Command identifyCommand(ArrayList brokenInput)
            {
                Command command = null;

                //if there's an archived command:
                if (archivedCommand != null)
                {
                    command = archivedCommand;
                }
                else
                {   //the first word must be a command
                    foreach (Command com in validCommands)
                    {
                        if (com.identify(brokenInput[0].ToString()))
                        {
                            command = com;
                        }
                    }
                    //remove name of command from the input
                    brokenInput.RemoveAt(0);
                }

                //if you entered a command that doesn't seek input but provide(d) input
                //if you didn't enter a command and there isn't an archived one
                if (command == null || (!command.seeksInput && brokenInput.Count > 0) || (!command.seeksInput && archivedInput != null))
                {
                    return null;
                }
                

                return command;
            }
        }


        //--------------------------------------------------------------------------------------

        public class InputIterator
        {
            private ArrayList oldInput;
            private int oldInputIndex;

            private ArrayList newInput;
            private int newInputIndex;

            private ArrayList walkedThrough;

            public InputIterator(ArrayList oldInput, ArrayList newInput)
            {
                this.oldInput = oldInput;
                this.newInput = newInput;
                walkedThrough = new ArrayList();
                oldInputIndex = 0;
                newInputIndex = 0;

                if (oldInput == null)
                {
                    oldInputIndex = -1;
                }

                if(newInput == null)
                {
                    throw new Exception("newInput cannot be null");
                }
            }
            

            public string next()
            {
                string result;

                //no old input
                if(oldInput == null)
                {
                    if(newInputIndex < newInput.Count)
                    {
                        result = (string)newInput[newInputIndex];
                        newInputIndex++;
                    } else
                    {
                        return null;
                    }
                } else //there was old input
                {
                    if (oldInputIndex < oldInput.Count)
                    {
                        result = (string)oldInput[oldInputIndex];
                        oldInputIndex++;
                    }
                    else //moved on to new input
                    {
                        if (newInputIndex < newInput.Count)
                        {
                            result = (string)newInput[newInputIndex];
                            newInputIndex++;
                        } else
                        {
                            return null; //new input is all used up
                        }

                    }
                }
                
                walkedThrough.Add(result);
                return result;
            }

            public bool hasNext()
            {
                //assuming there always must be new input
                if (newInputIndex < newInput.Count)
                {
                    return true;
                }
                return false;
            }

            public ArrayList getWalked()
            {
                return this.walkedThrough;
            }

            public ArrayList getWalkedMinusOne()
            {
                walkedThrough.RemoveAt( walkedThrough.Count - 1);
                return walkedThrough;
            }
        }



    } //end CommandNamespace
}
