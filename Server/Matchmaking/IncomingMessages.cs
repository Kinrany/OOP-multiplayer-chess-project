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
			incomingMessages["Create figure"] = create_figure;
			incomingMessages["Move figure"] = move_figure;
			incomingMessages["Delete figure"] = delete_figure;
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
				Broadcast("Say", player.ConnectUserId, message.GetString(0));
			}
			catch (Exception e){
				Log("Say message processing failed.", e);
				player.Send("Denied", "Incorrect message format.");
			}
		}

		private void create_figure(Player player, Message message) {
			try {
				string position = message.GetString(0);
				string figure = message.GetString(1);
				player.GameModel.CreateFigure(position, figure);
				Broadcast("Create figure", player.ConnectUserId, position, figure);
			}
			catch (Exception e) {
				Log("Create figure message processing failed.", e);
				player.Send("Denied", "Incorrect message format.");
			}
		}

		private void move_figure(Player player, Message message) {
			try {
				string pos1 = message.GetString(0);
				string pos2 = message.GetString(1);
				player.GameModel.MoveFigure(pos1, pos2);
				Broadcast("Move figure", player.ConnectUserId, pos1, pos2);
			}
			catch (Exception e) {
				Log("Move figure message processing failed.", e);
				player.Send("Denied", "Incorrect message format.");
			}
		}

		private void delete_figure(Player player, Message message) {
			try {
				string position = message.GetString(0);
				player.GameModel.DeleteFigure(position);
				Broadcast("Delete figure", player.ConnectUserId, position);
			}
			catch (Exception e) {
				Log("Delete figure message processing failed.", e);
				player.Send("Denied", "Incorrect message format.");
			}
		}
	}
}
