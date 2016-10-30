﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_To_The_Future_Game
{
    /// <summary>
    /// Console class for the MVC pattern
    /// </summary>
    public class ConsoleView
    {
        #region FIELDS

        //
        // declare a Year and Traveler object for the ConsoleView object to use
        //
        Year _gameYear;
        Traveler _gameTraveler;

        #endregion

        #region PROPERTIES

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// default constructor to create the console view objects
        /// </summary>
        public ConsoleView(Traveler gameTraveler, Year gameYear)
        {
            _gameTraveler = gameTraveler;
            _gameYear = gameYear;

            InitializeConsole();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// initialize all console settings
        /// </summary>
        private void InitializeConsole()
        {
            ConsoleUtil.WindowTitle = "Robemaps";
            ConsoleUtil.HeaderText = "Back to the Future";
        }

        /// <summary>
        /// display the Continue prompt
        /// </summary>
        public void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;

            Console.WriteLine();

            ConsoleUtil.DisplayMessage("Press any key to continue.");
            ConsoleKeyInfo response = Console.ReadKey();

            Console.WriteLine();

            Console.CursorVisible = true;
        }

        /// <summary>
        /// display the Exit prompt on a clean screen
        /// </summary>
        public void DisplayExitPrompt()
        {
            ConsoleUtil.HeaderText = "Exit";
            ConsoleUtil.DisplayReset();

            Console.CursorVisible = false;

            Console.WriteLine();
            ConsoleUtil.DisplayMessage(" Great Scott!");
            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayMessage(" Are you sure you want to leave the Future?");
            Console.ReadKey();

            System.Environment.Exit(1);
        }

        /// <summary>
        /// display the welcome screen
        /// </summary>
        public void DisplayWelcomeScreen()
        {
            StringBuilder sb = new StringBuilder();

            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Back To The Future Game");
            ConsoleUtil.DisplayMessage("");
            Console.WriteLine();

            sb.Clear();
            ConsoleUtil.DisplayMessage("");
            sb.AppendFormat("- You have just realized that the Flux Capacitor really does work!  -");
            ConsoleUtil.DisplayMessage("");
            sb.AppendFormat("- You now can drive or fly the DeLorean throughout time.  -");
            ConsoleUtil.DisplayMessage("");
            sb.AppendFormat("- Visit a destination year and see where the Future takes you -");
            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayMessage(sb.ToString());
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// setup the new Traveler object
        /// </summary>
        public void DisplayMissionSetupIntro()
        {
            //
            // display header
            //
            ConsoleUtil.HeaderText = "Time Traveler Setup";
            ConsoleUtil.DisplayReset();

            //
            // display intro
            //
            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayMessage("You will now be prompted to enter your personal information to start your time travels.");
            DisplayContinuePrompt();
        }

        /// <summary>
        /// display a message confirming mission setup
        /// </summary>
        public void DisplayMissionSetupConfirmation()
        {
            //
            // display header
            //
            ConsoleUtil.HeaderText = "Time Traveler Setup";
            ConsoleUtil.DisplayReset();

            //
            // display confirmation
            //
            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayMessage("Your time travel setup is complete.");
            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayMessage("To view your traveler information use the Main Menu.");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// get player's name
        /// </summary>
        /// <returns>name as a string</returns>
        public string DisplayGetTravelersName()
        {
            string travelersName;

            //
            // display header
            //
            ConsoleUtil.HeaderText = "Traveler's Name and Age";
            ConsoleUtil.DisplayReset();
            ConsoleUtil.DisplayPromptMessage("Enter your Name: ");
            travelersName = Console.ReadLine();
            _gameTraveler.Age = DisplayGetInteger(1, 130, "Enter your Age: ");
            ConsoleUtil.DisplayReset();
            ConsoleUtil.DisplayMessage($"You have indicated {travelersName} as your name and you are {_gameTraveler.Age} years old.");

            DisplayContinuePrompt();

            return travelersName;
        }
        private int DisplayGetInteger(int min, int max, string prompt)
        {
            int integer = 0;

            bool validAge = false;

            while (!validAge)
            {
                ConsoleUtil.DisplayPromptMessage(prompt);
                if (int.TryParse(Console.ReadLine(), out integer))
                {
                    if (integer > min && integer < max)
                    {
                        validAge = true;
                    }
                    else
                    {
                        Console.WriteLine("Thats not within the range! Try Again");
                        DisplayContinuePrompt();
                    }
                }
                else
                {
                    Console.WriteLine("That's not an integer!");
                    DisplayContinuePrompt();
                }
            }

            return integer;
        }
        /// <summary>
        /// get and validate the player's race
        /// </summary>
        /// <returns>race as a GameCharacterType</returns>
        public Traveler.GameCharacterType DisplayGetTravelersRace()
        {
            bool validResponse = false;
            Traveler.GameCharacterType travelersCharacter = Traveler.GameCharacterType.None;

            while (!validResponse)
            {
                //
                // display header
                //
                ConsoleUtil.HeaderText = "Time Traveler's GameCharacter";
                ConsoleUtil.DisplayReset();

                //
                // display all character types on a line
                //
                ConsoleUtil.DisplayMessage("GameCharacters");
                StringBuilder sb = new StringBuilder();
                foreach (Character.GameCharacterType characterType in Enum.GetValues(typeof(Character.GameCharacterType)))
                {
                    if (characterType != Character.GameCharacterType.None)
                    {
                        sb.Append($" [{characterType}] ");
                    }

                }
                ConsoleUtil.DisplayMessage(sb.ToString());

                ConsoleUtil.DisplayPromptMessage("Enter your character: ");

                //
                // validate user response for character
                //
                if (Enum.TryParse<Character.GameCharacterType>(Console.ReadLine(), out travelersCharacter))
                {
                    validResponse = true;
                    ConsoleUtil.DisplayReset();
                    ConsoleUtil.DisplayMessage("");
                    ConsoleUtil.DisplayMessage($"You have indicated {travelersCharacter} as your character.");
                }
                else
                {
                    ConsoleUtil.DisplayMessage("");
                    ConsoleUtil.DisplayMessage("You must choose a character from the list above.");
                    ConsoleUtil.DisplayMessage("Please re-enter your character.");
                }

                DisplayContinuePrompt();
            }

            return travelersCharacter;
        }

        /// <summary>
        /// get and validate the player's year destination
        /// </summary>
        /// <returns>space-time location</returns>
        public YearLocation DisplayGetTravelersNewDestination()
        {
            bool validResponse = false;
            int locationID;
            YearLocation nextYearTimeLocation = new YearLocation();

            while (!validResponse)
            {
                //
                // display header
                //
                ConsoleUtil.HeaderText = "Year Destination";
                ConsoleUtil.DisplayReset();

                //
                // display a table of locations
                DisplayYearDestinationTable();

                //
                // get and validate user's response for a location
                //
                ConsoleUtil.DisplayMessage("");
                ConsoleUtil.DisplayPromptMessage("Choose the year destination by entering the ID: ");

                //
                // user's response is an integer
                //
                if (int.TryParse(Console.ReadLine(), out locationID))
                {
                    ConsoleUtil.DisplayMessage("");

                    try
                    {
                        nextYearTimeLocation = _gameYear.GetYearTimeLocationByID(locationID);

                        ConsoleUtil.DisplayReset();
                        ConsoleUtil.DisplayMessage($"You have indicated {nextYearTimeLocation.Name} as your destination.");
                        ConsoleUtil.DisplayMessage("");

                        if (nextYearTimeLocation.Accessable == true)
                        {
                            validResponse = true;
                            ConsoleUtil.DisplayMessage("You have reached 88 miles per hour in the DeLorean. Were off to the Future!");
                        }
                        else
                        {
                            ConsoleUtil.DisplayMessage("The Flux Capacitor is broke and you can't travel to this year at this time. This could have something to do with your inventory value");
                            ConsoleUtil.DisplayMessage("");
                            ConsoleUtil.DisplayMessage("Please make another choice.");
                        }
                    }
                    //
                    // user's response was not in the correct range
                    //
                    catch (ArgumentOutOfRangeException ex)
                    {
                        ConsoleUtil.DisplayMessage("It appears you entered an invalid year ID.");
                        ConsoleUtil.DisplayMessage(ex.Message);
                        ConsoleUtil.DisplayMessage("Please try again.");
                    }
                }
                //
                // user's response was not an integer
                //
                else
                {
                    ConsoleUtil.DisplayMessage("It appears you did not enter a number for the year ID.");
                    ConsoleUtil.DisplayMessage("");
                    ConsoleUtil.DisplayMessage("Please try again.");
                }

                DisplayContinuePrompt();
            }

            return nextYearTimeLocation;
        }

        /// <summary>
        /// generate a table of  location names and ids
        /// </summary>
        public void DisplayYearDestinationTable()
        {
            int locationNumber = 1;

            //
            // table headings
            //
            ConsoleUtil.DisplayMessage("ID".PadRight(10) + "Year".PadRight(20));
            ConsoleUtil.DisplayMessage("---".PadRight(10) + "-------------".PadRight(20));

            //
            // location name and id
            //
            foreach (YearLocation location in _gameYear.YearLocations)
            {
                ConsoleUtil.DisplayMessage(location.YearTimeLocationID.ToString().PadRight(10) + location.Name.PadRight(20));
                locationNumber++;
            }

        }
        /// <summary>
        /// generate a table of item names and ids
        /// </summary>
        /// <param name="items"></param>
        public void DisplayItemTable(List<Item> items)
        {
            // table headings
            ConsoleUtil.DisplayMessage("ID".PadRight(10) + "Name".PadRight(20));
            ConsoleUtil.DisplayMessage("---".PadRight(10) + "-------------".PadRight(20));

            // item name and id
            foreach (Item item in items)
            {
                ConsoleUtil.DisplayMessage(item.GameObjectID.ToString().PadRight(10) + item.Name.PadRight(20));
            }
        }
        /// <summary>
        /// generate a table of treasure names and ids
        /// </summary>
        /// <param name="items"></param>
        public void DisplayTreasureTable(List<Treasure> treasures)
        {
            // table headings
            ConsoleUtil.DisplayMessage("ID".PadRight(10) + "Name".PadRight(20));
            ConsoleUtil.DisplayMessage("---".PadRight(10) + "-------------".PadRight(20));

            // treasure name and id
            foreach (Treasure treasure in treasures)
            {
                ConsoleUtil.DisplayMessage(treasure.GameObjectID.ToString().PadRight(10) + treasure.Name.PadRight(20));
            }
        }
        /// <summary>
        /// get the action choice from the user
        /// </summary>
        public TravelerAction DisplayGetTravelerActionChoice()
        {
            TravelerAction travelerActionChoice = TravelerAction.None;
            bool usingMenu = true;

            while (usingMenu)
            {
                //
                // set up display area
                //
                ConsoleUtil.HeaderText = "Time Traveler Actions";
                ConsoleUtil.DisplayReset();
                Console.CursorVisible = false;

                //
                // display the menu
                //
                ConsoleUtil.DisplayMessage("What would you like to do (Type Letter)?");
                Console.WriteLine();
                Console.WriteLine(
                    "\t" + "**************************" + Environment.NewLine +
                    "\t" + "Time Traveler Actions" + Environment.NewLine +
                    "\t" + "**************************" + Environment.NewLine +
                    "\t" + "A. Look Around" + Environment.NewLine +
                    "\t" + "B. Look At" + Environment.NewLine +
                    "\t" + "C. Pick Up Item" + Environment.NewLine +
                    "\t" + "D. Pick Up Treasure" + Environment.NewLine +
                    "\t" + "E. Put Down Item" + Environment.NewLine +
                    "\t" + "F. Put Down Treasure" + Environment.NewLine +
                    "\t" + "G. Travel" + Environment.NewLine +
                    "\t" + Environment.NewLine +
                    "\t" + "**************************" + Environment.NewLine +
                    "\t" + "Time Traveler Information" + Environment.NewLine +
                    "\t" + "**************************" + Environment.NewLine +
                    "\t" + "H. Display General Time Traveler Information" + Environment.NewLine +
                    "\t" + "I. Display Time Traveler Inventory" + Environment.NewLine +
                    "\t" + "J. Display Time Traveler Treasure" + Environment.NewLine +
                    "\t" + Environment.NewLine +
                    "\t" + "**************************" + Environment.NewLine +
                    "\t" + "Game Information" + Environment.NewLine +
                    "\t" + "**************************" + Environment.NewLine +
                    "\t" + "K. Display All Year Destinations" + Environment.NewLine +
                    "\t" + "L. Display All Game Items" + Environment.NewLine +
                    "\t" + "M. Display All Game Treasures" + Environment.NewLine +
                    "\t" + Environment.NewLine +
                    "\t" + "**************************" + Environment.NewLine +
                    "\t" + "Q. Quit" + Environment.NewLine);

                //
                // get and process the user's response
                // note: ReadKey argument set to "true" disables the echoing of the key press
                //
                ConsoleKeyInfo userResponse = Console.ReadKey(true);
                switch (userResponse.KeyChar)
                {
                    case 'A':
                    case 'a':
                        travelerActionChoice = TravelerAction.LookAround;
                        usingMenu = false;
                        break;
                    case 'B':
                    case 'b':
                        travelerActionChoice = TravelerAction.LookAt;
                        usingMenu = false;
                        break;
                    case 'C':
                    case 'c':
                        travelerActionChoice = TravelerAction.PickUpItem;
                        usingMenu = false;
                        break;
                    case 'D':
                    case 'd':
                        travelerActionChoice = TravelerAction.PickUpTreasure;
                        usingMenu = false;
                        break;
                    case 'E':
                    case 'e':
                        travelerActionChoice = TravelerAction.PutDownItem;
                        usingMenu = false;
                        break;
                    case 'F':
                    case 'f':
                        travelerActionChoice = TravelerAction.PutDownTreasure;
                        usingMenu = false;
                        break;
                    case 'G':
                    case 'g':
                        travelerActionChoice = TravelerAction.Travel;
                        usingMenu = false;
                        break;
                    case 'H':
                    case 'h':
                        travelerActionChoice = TravelerAction.TravelerInfo;
                        usingMenu = false;
                        break;
                    case 'I':
                    case 'i':
                        travelerActionChoice = TravelerAction.TravelerInventory;
                        usingMenu = false;
                        break;
                    case 'J':
                    case 'j':
                        travelerActionChoice = TravelerAction.TravelerTreasure;
                        usingMenu = false;
                        break;
                    case 'K':
                    case 'k':
                        travelerActionChoice = TravelerAction.ListYearDestinations;
                        usingMenu = false;
                        break;
                    case 'L':
                    case 'l':
                        travelerActionChoice = TravelerAction.ListItems;
                        usingMenu = false;
                        break;
                    case 'M':
                    case 'm':
                        travelerActionChoice = TravelerAction.ListTreasures;
                        usingMenu = false;
                        break;
                    case 'Q':
                    case 'q':
                        travelerActionChoice = TravelerAction.Exit;
                        usingMenu = false;
                        break;
                    default:
                        Console.WriteLine(
                            "It appears you have selected an incorrect choice." + Environment.NewLine +
                            "Press any key to continue or the ESC key to quit the application.");

                        userResponse = Console.ReadKey(true);
                        if (userResponse.Key == ConsoleKey.Escape)
                        {
                            usingMenu = false;
                        }
                        break;
                }
            }
            Console.CursorVisible = true;

            return travelerActionChoice;
        }

        /// <summary>
        /// display information about the current location
        /// </summary>
        public void DisplayLookAround()
        {
            ConsoleUtil.HeaderText = "Current Year Location";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage(_gameYear.GetYearTimeLocationByID(_gameTraveler.TimeLocationID).Description);

            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayMessage("Items in current location.");
            ConsoleUtil.DisplayMessage("");
            foreach (Item item in _gameYear.GetItemsByYearTimeLocationID(_gameTraveler.TimeLocationID))
            {
                ConsoleUtil.DisplayMessage(item.Name + " - " + item.Description);
            }

            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayMessage("Treasures in current location.");
            ConsoleUtil.DisplayMessage("");
            foreach (Treasure treasure in _gameYear.GetTreasuressByYearTimeLocationID(_gameTraveler.TimeLocationID))
            {
                ConsoleUtil.DisplayMessage(treasure.Name + " - " + treasure.Description);
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display a list of all destinations
        /// <summary>
        public void DisplayAllYearDestinations()
        {
            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.HeaderText = "Year Locations";
            ConsoleUtil.DisplayReset();

            foreach (YearLocation location in _gameYear.YearLocations)
            {
                ConsoleUtil.DisplayMessage("");
                ConsoleUtil.DisplayMessage("ID: " + location.YearTimeLocationID);
                ConsoleUtil.DisplayMessage("Name: " + location.Name);
                ConsoleUtil.DisplayMessage("Description: " + location.Description);
                ConsoleUtil.DisplayMessage("Accessible: " + location.Accessable);
                ConsoleUtil.DisplayMessage("");
            }

            DisplayContinuePrompt();
        }
        /// <summary>
        /// display items to look at
        /// </summary>
        public void DisplayLookAt()
        {
            ConsoleUtil.HeaderText = "Items and Treasures in  Current Location";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayMessage("Items");
            ConsoleUtil.DisplayMessage("");

            int locationID;
            locationID = _gameTraveler.TimeLocationID;

            foreach (Item item in _gameYear.Items)
            {
                if (item.YearTimeLocationID == locationID)
                {
                    ConsoleUtil.DisplayMessage(item.GameObjectID + " - " + item.Name + " - " + item.Description);
                }
            }
            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayMessage("Treasures");
            ConsoleUtil.DisplayMessage("");

            locationID = _gameTraveler.TimeLocationID;

            foreach (Treasure treasure in _gameYear.Treasures)
            {
                if (treasure.YearTimeLocationID == locationID)
                {
                    ConsoleUtil.DisplayMessage(treasure.GameObjectID + " - " + treasure.Name + " - " + treasure.Description);
                }

            }
            DisplayContinuePrompt();

        }
        /// <summary>
        /// display items available to pick up in location
        /// </summary>
        /// <returns></returns>
        public int DisplayPickUpItem()
        {
            ConsoleUtil.HeaderText = "Pick Up Item";
            ConsoleUtil.DisplayReset();
            ConsoleUtil.DisplayMessage("");

            int itemID = 0;
            int locationID;
            locationID = _gameTraveler.TimeLocationID;
            List<Item> itemsInCurrentLocation = new List<Item>();
            itemsInCurrentLocation = _gameYear.GetItemsByYearTimeLocationID(locationID);
            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayMessage("Items in current Location");
            ConsoleUtil.DisplayMessage("");
            DisplayItemTable(itemsInCurrentLocation);
            ConsoleUtil.DisplayMessage("Enter Item Number :");
            itemID = int.Parse(Console.ReadLine());
            if (itemsInCurrentLocation == null)
            {
                ConsoleUtil.DisplayMessage("There are no items in the current location");
            }
            DisplayContinuePrompt();
            return itemID;
        }
        /// <summary>
        /// display treasure available for pick up
        /// </summary>
        public int DisplayPickUpTreasures()
        {
            ConsoleUtil.HeaderText = "Pick Up Treasure";
            ConsoleUtil.DisplayReset();

            int treasureID = 0;
            int locationID;
            locationID = _gameTraveler.TimeLocationID;
            List<Treasure> treasuresInCurrentLocation = new List<Treasure>();
            treasuresInCurrentLocation = _gameYear.GetTreasuressByYearTimeLocationID(locationID);
            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayMessage("Treasures in current Location");
            ConsoleUtil.DisplayMessage("");
            DisplayTreasureTable(treasuresInCurrentLocation);
            ConsoleUtil.DisplayMessage("Enter Treasure Number :");
            treasureID = int.Parse(Console.ReadLine());
            if (treasuresInCurrentLocation == null)
            {
                ConsoleUtil.DisplayMessage("There are no treasures in the current location");
            }
            DisplayContinuePrompt();
            return treasureID;
        }

        /// <summary>
        /// display items available to put down in location
        /// </summary>
        /// <returns></returns>
        public int DisplayPutDownItem()
        {
            ConsoleUtil.HeaderText = "Put Down Item";
            ConsoleUtil.DisplayReset();

            int itemID = 0;
            int locationID;
            locationID = _gameTraveler.TimeLocationID;
            List<Item> itemsInCurrentLocation = new List<Item>();
            itemsInCurrentLocation = _gameTraveler.TravelersItems;
            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayMessage("Items in Inventory");
            ConsoleUtil.DisplayMessage("");
            DisplayItemTable(itemsInCurrentLocation);
            ConsoleUtil.DisplayMessage("Enter Item Number :");
            itemID = int.Parse(Console.ReadLine());
            if (_gameTraveler.TravelersItems == null)
            {
                ConsoleUtil.DisplayMessage("You currently have no items in your inventory ");
            }
            DisplayContinuePrompt();
            return itemID;
        }
        /// <summary>
        /// display treasure available for pick up
        /// </summary>
        public int DisplayPutDownTreasures()
        {
            ConsoleUtil.HeaderText = "Put Down Treasure";
            ConsoleUtil.DisplayReset();

            int treasureID = 0;
            int locationID;
            locationID = _gameTraveler.TimeLocationID;

            List<Treasure> treasuresInCurrentLocation = new List<Treasure>();
            treasuresInCurrentLocation = _gameTraveler.TravelersTreasures;
            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayMessage("Treasures in Inventory");
            ConsoleUtil.DisplayMessage("");
            DisplayTreasureTable(treasuresInCurrentLocation);
            ConsoleUtil.DisplayMessage("Enter Treasure Number :");
            treasureID = int.Parse(Console.ReadLine());
            if (_gameTraveler.TravelersTreasures == null)
            {
                ConsoleUtil.DisplayMessage("You currently have no treasures in your inventory ");
            }
            DisplayContinuePrompt();
            return treasureID;
        }
       
        /// <summary>
        /// display a list of all game items
        /// <summary>
        public void DisplayListAllGameItems()
        {
            ConsoleUtil.HeaderText = "Game Items";
            ConsoleUtil.DisplayReset();

            foreach (Item item in _gameYear.Items)
            {
                ConsoleUtil.DisplayMessage("ID: " + item.GameObjectID);
                ConsoleUtil.DisplayMessage("Name: " + item.Name);
                ConsoleUtil.DisplayMessage("Description: " + item.Description);

                //
                // all treasure in the traveler's inventory have a TimeLocationID of 0
                //
                if (item.YearTimeLocationID != 0)
                {
                    ConsoleUtil.DisplayMessage("Location: " + _gameYear.GetYearTimeLocationByID(item.YearTimeLocationID).Name);
                }
                else
                {
                    ConsoleUtil.DisplayMessage("Location: Traveler's Inventory");
                }


                ConsoleUtil.DisplayMessage("Value: " + item.Value);
                ConsoleUtil.DisplayMessage("Can Add to Inventory: " + item.CanAddToInventory.ToString().ToUpper());
                ConsoleUtil.DisplayMessage("");
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display a list of all game treasures
        /// <summary>
        public void DisplayListAllGameTreasures()
        {
            ConsoleUtil.HeaderText = "Game Treasures";
            ConsoleUtil.DisplayReset();

            foreach (Treasure treasure in _gameYear.Treasures)
            {
                ConsoleUtil.DisplayMessage("ID: " + treasure.GameObjectID);
                ConsoleUtil.DisplayMessage("Name: " + treasure.Name);
                ConsoleUtil.DisplayMessage("Description: " + treasure.Description);
                
                //
                // all treasure in the traveler's inventory have a TimeLocationID of 0
                //
                if (treasure.YearTimeLocationID != 0)
                {
                    ConsoleUtil.DisplayMessage("Location: " + _gameYear.GetYearTimeLocationByID(treasure.YearTimeLocationID).Name);
                }
                else
                {
                    ConsoleUtil.DisplayMessage("Location: Time Traveler's Inventory");
                }

                ConsoleUtil.DisplayMessage("Value: " + treasure.Value);
                ConsoleUtil.DisplayMessage("Can Add to Inventory: " + treasure.CanAddToInventory.ToString().ToUpper());
                ConsoleUtil.DisplayMessage("");
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display the current traveler information
        /// </summary>
        public void DisplayTravelerInfo()
        {
            ConsoleUtil.HeaderText = "Time Traveler Info";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage($"Time Traveler's Name: {_gameTraveler.Name}");
            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayMessage($" Time Traveler's Age:  {_gameTraveler.Age}");
            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayMessage($"Time Traveler's Game Character: {_gameTraveler.GameCharacter}");
            ConsoleUtil.DisplayMessage("");
            string spaceTimeLocationName = _gameYear.GetYearTimeLocationByID(_gameTraveler.TimeLocationID).Name;
            ConsoleUtil.DisplayMessage($"Traveler's Current Location: {spaceTimeLocationName}");
            ConsoleUtil.DisplayMessage("");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display the current traveler items
        /// </summary>
        public void DisplayTravelerItems()
        {
            ConsoleUtil.HeaderText = "Time Traveler Inventory";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Time Traveler Items");
            ConsoleUtil.DisplayMessage("");

            foreach (Item item in _gameTraveler.TravelersItems)
            {
                ConsoleUtil.DisplayMessage("ID: " + item.GameObjectID);
                ConsoleUtil.DisplayMessage("Name: " + item.Name);
                ConsoleUtil.DisplayMessage("Description: " + item.Description);
                ConsoleUtil.DisplayMessage("Item value: " + item.Value);
                ConsoleUtil.DisplayMessage("");
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display the current traveler's treasure
        /// </summary>
        public void DisplayTravelerTreasure()
        {
            ConsoleUtil.HeaderText = "Time Traveler Inventory";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayMessage("Time Traveler Treasure");
            ConsoleUtil.DisplayMessage("");

            foreach (Treasure treasure in _gameTraveler.TravelersTreasures)
            {
                ConsoleUtil.DisplayMessage("ID: " + treasure.GameObjectID);
                ConsoleUtil.DisplayMessage("Name: " + treasure.Name);
                ConsoleUtil.DisplayMessage("Description: " + treasure.Description);
                ConsoleUtil.DisplayMessage("Treasure Value: " + treasure.Value);
                ConsoleUtil.DisplayMessage("");
            }
            
            DisplayContinuePrompt();
        }

        #endregion
    }
}
