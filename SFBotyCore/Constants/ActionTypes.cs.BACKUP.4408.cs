<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFBotyCore.Constants {
	public static class ActionTypes {
		public static string LoginToSF = "002";
		public static string LogOut = "535";
		
		public static string TakeQuest1 = "5101%3B0";
		public static string TakeQuest2 = "5102%3B0";
		public static string TakeQuest3 = "5103%3B0";
		public static string QuestCancel = "511";

		public static string JoinTarvern = "010";
		public static string JoinArena = "011";
		public static string JoinGuild = "006";
		public static string JoinCharacter = "004";//Aufrufen des eigenen Chars
		public static string JoinDungeon = "008";
        public static string JoinForge = "013";
        public static string JoinMagicshop = "014";

		public static string GetChatHistory_NotSure = "5173"; //getChatHistory?
		
		public static string JoinStadtwache = "012";
		public static string DoStadtwache1Hour = "5021";
		public static string DoStadtwache10Hour = "50210";

		public static string JoinToilet = "303";
		public static string FlushToilet = "302";
		public static string ResponseToiletFull = "306";
		public static string ResponseToiletLocked = "304";
        /// <summary>
        /// Achtung die Item Rucksackslot Nummer muss noch angehangen werden (1-5)
        /// und die entsprechende Action ";0;0" verkauft ein Item und ";10;0" packt es in das WC
        /// </summary>
        public static string ItemAction = "5042;";

		/// <summary>
		/// Achtung DungeonID muss noch selbstständigangehangen werden
		/// </summary>
		public static string DoDungeon = "519"; // ID 1-13 wird hierbei dann angehangen

		public static string BuyStatStr = "0211";
		public static string BuyStatDex = "0212";
		public static string BuyStatInt = "0213";
		public static string BuyStatAus = "0214";
		public static string BuyStatLuck = "0215";
		public static string BuyBeer = "518";
	}
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFBotyCore.Constants {
	public static class ActionTypes {
		public static string LoginToSF = "002";
		public static string LogOut = "535";
		
		public static string TakeQuest1 = "5101%3B0";
		public static string TakeQuest2 = "5102%3B0";
		public static string TakeQuest3 = "5103%3B0";
		public static string QuestCancel = "511";

		public static string JoinTarvern = "010";
		public static string JoinArena = "011";
		public static string JoinGuild = "006";
		public static string JoinCharacter = "004";//Aufrufen des eigenen Chars
		public static string JoinDungeon = "008";
        public static string JoinForge = "013";
        public static string JoinMagicshop = "014";
        /// <summary>
        /// Es kann auch noch die Obergrenze des Rangbereiches angegeben werden
        /// Bsp: 0071 Zeigt die Ränge 1-15 an.
        /// </summary>
        public static string JoinHallOfFame = "007";
        
        /// <summary>
        /// Es muss noch der Nick des Spielers angehangen werden.
        /// </summary>
        public static string AttackEnemy = "512";

		public static string GetChatHistory_NotSure = "5173"; //getChatHistory?
		
		public static string JoinStadtwache = "012";
		public static string DoStadtwache1Hour = "5021";
		public static string DoStadtwache10Hour = "50210";

		public static string JoinToilet = "303";
		public static string FlushToilet = "302";
		public static string ResponseToiletFull = "306";
		public static string ResponseToiletLocked = "304";
        /// <summary>
        /// Achtung die Item Rucksackslot Nummer muss noch angehangen werden (1-5)
        /// und die entsprechende Action ";0;0" verkauft ein Item und ";10;0" packt es in das WC Bsp.: 50425;0;0
        /// </summary>
        public static string ItemAction = "5042;";

		/// <summary>
		/// Achtung DungeonID muss noch selbstständigangehangen werden
		/// </summary>
		public static string DoDungeon = "519"; // ID 1-13 wird hierbei dann angehangen

		public static string BuyStatStr = "0211";
		public static string BuyStatDex = "0212";
		public static string BuyStatInt = "0213";
		public static string BuyStatAus = "0214";
		public static string BuyStatLuck = "0215";
		public static string BuyBeer = "518";
	}
>>>>>>> 454a6d78d0b55ae47aba6c5e6b167d41c9d07630
}