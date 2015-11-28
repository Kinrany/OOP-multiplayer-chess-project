using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlayerIO.GameLibrary;

namespace Matchmaking {
	public partial class Game {
		private Dictionary<string, IncomingMessageDelegate> incomingMessages = 
			new Dictionary<string, IncomingMessageDelegate>();

		private delegate void IncomingMessageDelegate(Player player, Message message);

		private void LoadIncomingMessages() {
			incomingMessages["Challenge player"] = challenge_player;
			incomingMessages["Say"] = say;
		}

		private void challenge_player(Player player, Message message) {
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
				player.Send("Denied", "Incorrect message format.");
			}
		}
		private void say(Player player, Message message) {
			try { 
				Broadcast("Say", message.GetString(0));
			}
			catch (Exception e){
				Log("Say message processing failed.", e);
				player.Send("Denied", "Incorrect message format.");
			}
		}
    }
}
