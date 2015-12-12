using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlayerIOClient;

namespace ClientNamespace {
	partial class Networking {
		private Dictionary<string, MessageDelegate> messages;

		private delegate void MessageDelegate(Message m);

		private void LoadMessages() {
			messages = new Dictionary<string, MessageDelegate>();
			messages["Denied"]            = delegate(Message m) { OnDeniedMessage          ((string)m[0]                            ); };
			messages["User joined"]       = delegate(Message m) { OnUserJoinedMessage      ((string)m[0]                            ); };
			messages["User left"]         = delegate(Message m) { OnUserLeftMessage        ((string)m[0]                            ); };
			messages["Challenged"]        = delegate(Message m) { OnChallengedMessage      ((string)m[0]                            ); };
			messages["Challenge revoked"] = delegate(Message m) { OnChallengeRevokedMessage((string)m[0]                            ); };
			messages["Game started"]      = delegate(Message m) { OnGameStartedMessage     (                                        ); };
			messages["Game ended"]        = delegate(Message m) { OnGameEndedMessage       (                                        ); };
			messages["Say"]               = delegate(Message m) { OnSayMessage             ((string)m[0], (string)m[1]              ); };
			messages["Create figure"]     = delegate(Message m) { OnCreateFigureMessage    ((string)m[0], (string)m[1], (string)m[2]); };
			messages["Move figure"]       = delegate(Message m) { OnMoveFigureMessage      ((string)m[0], (string)m[1], (string)m[2]); };
			messages["Delete figure"]     = delegate(Message m) { OnDeleteFigureMessage    ((string)m[0], (string)m[1]              ); };
		}
	}
}
