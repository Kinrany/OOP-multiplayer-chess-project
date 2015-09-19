using PlayerIO.GameLibrary;

namespace BouncePlus {
	[RoomType("Bounce+ v1.0")]
	public class Game : Game<BasePlayer> {

		public override bool AllowUserJoin(BasePlayer player) {
			string requestedId = player.ConnectUserId;
			foreach (var p in Players) {
				if (requestedId == p.ConnectUserId) {
					p.Send("Denied", "Name " + requestedId + " is already in use.");
					return false;
				}
			}
			return true;
		}

		public override void UserJoined(BasePlayer player) {
			Broadcast("User joined", player.ConnectUserId);
		}

		public override void UserLeft(BasePlayer player) {
			Broadcast("User left", player.ConnectUserId);
		}

		public override void GotMessage(BasePlayer player, Message message) {
			if (message.Type == "Denied"
				|| message.Type == "User joined"
				|| message.Type == "User left") {
				player.Send("Denied", @"Message types ""Denied"", ""User joined"" and ""User left"" are reserved.");
			}
			else {
				message.Add(player.ConnectUserId);
				Broadcast(message);
			}
		}
	}
}
