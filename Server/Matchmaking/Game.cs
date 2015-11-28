using System;
using System.Linq;
using System.Collections.Generic;
using PlayerIO.GameLibrary;

namespace Matchmaking {
	[RoomType("Matchmaking v1.2")]
	public class Game : Game<Player> {

		public override bool AllowUserJoin(Player player) {
			Log("Player " + player.ConnectUserId + " tried to join the room.");

			// Не позволяет пользователю присоединиться, если имя уже занято
			string requestedId = player.ConnectUserId;
			foreach (var p in Players) {
				if (requestedId == p.ConnectUserId) {
					p.Send("Denied", "Name " + requestedId + " is already in use.");
					return false;
				}
			}
			return true;
		}

		public override void UserJoined(Player player) {
			Log("Player " + player.ConnectUserId + " joined.");

			player.Initialize(this);
			player.SendPlayerList();

			Broadcast("User joined", player.ConnectUserId);
		}

		public override void UserLeft(Player player) {
			Log("Player " + player.ConnectUserId + " left.");

			Broadcast("User left", player.ConnectUserId);
		}

		public override void GotMessage(Player player, Message message) {
			Log("Player " + player.ConnectUserId + "sent a \"" + message.Type + "\" message. Contents: \n" + message.ToString());

			switch (message.Type) {
				case "Challenge player":
					try {
						if (player.IsPlaying) {
							player.Send("Denied", "You are already playing.");
						}
						Player target = FindPlayerByName(message.GetString(0));
						if (target.IsPlaying) {
							player.Send("Denied", "Target is already playing.");
						}
						player.ChallengePlayer(target);
					}
					catch (Exception e) {
						Log("Challenge player message processing failed.", e);
						player.Send("Denied", "Incorrect message format/player not found.");
					}
					break;
				default:
					player.Send("Denied", "Unknown message type.");
					break;
			}
		}

		private Player FindPlayerByName(string name) {
			return Players.SingleOrDefault<Player>(
				(p) => (p.ConnectUserId == name)
			);
		}

		public void Log(string error) {
			PlayerIO.ErrorLog.WriteError(error);
		}
		public void Log(string error, Exception exception) {
			PlayerIO.ErrorLog.WriteError(error, exception);
		}
		public void Log(string error, string details, string stacktrace, Dictionary<string, string> extraData) {
			PlayerIO.ErrorLog.WriteError(error, details, stacktrace, extraData);
		}
	}
}