﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFBotyCore.Mechanic.Account;
using System.Net;
using System.IO;
using SFBotyCore.Mechanic.Areas;
using Assert;
using System.Threading;
using SFBotyCore.Constants;
using System.Net.NetworkInformation;

namespace SFBotyCore.Mechanic {
	public abstract class BaseArea : IMenuArea {
		private WebClient RefClient { get; set; }
		protected Random random;
		protected int RandomValue { get { return random.Next(1, 2000000000); } }

		private Stream streamData;
		private StreamReader streamReader;
		protected Account.Account Account;
		public abstract event EventHandler<MessageEventsArgs> MessageOutput;
		public event EventHandler<MessageEventsArgs> ExtendedLog;
		private DateTime LastSendRequestTimeStamp;

		public virtual void Initialize(Account.Account account, WebClient refClient) {
			this.Account = account;
			this.RefClient = refClient;

			random = new Random(System.Environment.TickCount);
			LastSendRequestTimeStamp = DateTime.Now;
		}

		public virtual void PerformArea() {
			if(Account.Settings.HasLogin) {
				return;
			}
		}

		public void RecreateClient() {
			if (RefClient != null) {
				RefClient.Dispose();
				RefClient = null;
			}

			RefClient = new WebClient();
			RefClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:26.0) Gecko/20100101 Firefox/26.0");
			RefClient.Headers.Add(HttpRequestHeader.AcceptLanguage, "de-DE,de;q=0.8,en-US;q=0.6,en;q=0.4");
			RefClient.Headers.Add(HttpRequestHeader.AcceptCharset, "ISO-8859-1,utf-8;q=0.7,*;q=0.3");
		}

		public virtual void Dispose() {
			streamData.Close();
			streamReader.Close();
			streamReader.Dispose();
			streamData.Dispose();
		}

		protected string SendRequest(string action) {
			Account.LastAction = DateTime.Now;
			if ((DateTime.Now - LastSendRequestTimeStamp).TotalSeconds < Account.Settings.MinSendRequestInterval) {
				ThreadSleep(Account.Settings.MinSendRequestInterval, Account.Settings.MinSendRequestInterval);
				ExtendedLog(this, new MessageEventsArgs("Send Action Sleep"));
			}
			
			string s = "";
			int foo = 0;
			if (!CheckServerConnection()) {
				ThreadSleep(900f, 1200f); //Warte 15-20Min
				DoReLogin(ref s, ref foo);
				if (ExtendedLog != null) {
					ExtendedLog(this, new MessageEventsArgs("Relogin"));
				}
			} //else do nothing
			
			if (action == ActionTypes.LoginToSF) {
				if (Account.Settings.HasLogin) {
					throw new Exception("Fehler im SendRequest");
				} else { //einziger Sonderfall ist das Login in SF
					streamData = RefClient.OpenRead(String.Concat("http://", Account.Settings.Server, ".sfgame.de/request.php?req=00000000000000000000000000000000002", Account.Settings.Username, ";", Account.Settings.PasswordHash, ";", "v1.70&random=%2&rnd=", RandomValue, (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds));
					streamReader = new StreamReader(streamData);
					string sData = streamReader.ReadToEnd();
					return sData;
				}
			}

			int count = 3;
			do {
				if (count < 0) {
					throw new Exception("Can't login anymore");
				}

				if (s == "E065" || s == "+E065") {
					DoReLogin(ref s, ref count);
					ExtendedLog(this, new MessageEventsArgs("Relogin wegen Fehler E065"));
				}

				streamData = RefClient.OpenRead(String.Concat("http://", Account.Settings.Server, ".sfgame.de/request.php?req=", Account.Settings.SessionID, action, "&random=%2&rnd=", RandomValue, (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds));
				streamReader = new StreamReader(streamData);
				s = streamReader.ReadToEnd();

				if (ExtendedLog != null) {
					ExtendedLog(this, new MessageEventsArgs(action + Environment.NewLine + s));
				}
			} while (s == "E065" || s == "+E065");

			LastSendRequestTimeStamp = DateTime.Now;
			return s;
		}

		private void DoReLogin(ref string s, ref int count) {
			Account.Logout();
			ThreadSleep(1f, 2f);
			s = SendRequest(ActionTypes.LoginToSF);
			LoginArea.UpdateLoginData(s, Account);
			ThreadSleep(5f, 6f);
			count -= 1;
		}

		[Obsolete]
		private bool CheckInternetConnection() {
			PingReply reply = null;
			Ping ping = new Ping();
			int pingCount = 0;
			do {
				try {
					pingCount += 1;
					reply = ping.Send("8.8.8.8");
					ThreadSleep(0.25f, 0.30f);
				} catch {}
			} while (reply.Status != IPStatus.Success && reply != null);

			return pingCount > 1;
		}

		private bool CheckServerConnection() {
			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("http://" + Account.Settings.Server + ".sfgame.de/request.php");
			int responseCode = 0;

			try {
				HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
				responseCode = (int)response.StatusCode;
			} catch { }

			return (responseCode == 200);
		}

		/// <summary>
		/// Lässt den aktuellen Thread schlafen.
		/// </summary>
		/// <param name="minTime">minimale Zeit in Sekunden</param>
		/// <param name="maxTime">maximale Zeit in Sekunden</param>
		protected void ThreadSleep(float minTime, float maxTime) {
			Asserts.IsTrue(minTime != null && minTime > 0f, "minTime muss größer 0 sein");
			Asserts.IsTrue(maxTime != null && maxTime > 0f, "maxTime muss größer 0 sein");
			Asserts.IsTrue(minTime <= maxTime, "minTime muss kleiner gleich maxTime sein");

			Thread.Sleep(random.Next((int)(minTime * 1000), (int)(maxTime * 1000)));
		}

		public abstract void RaiseMessageEvent(string s);
	}
}
