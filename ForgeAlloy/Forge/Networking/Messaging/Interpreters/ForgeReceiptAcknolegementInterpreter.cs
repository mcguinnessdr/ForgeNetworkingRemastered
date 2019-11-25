﻿using System.Net;
using Forge.Networking.Messaging.Messages;

namespace Forge.Networking.Messaging.Interpreters
{
	public class ForgeReceiptAcknolegementInterpreter : IMessageInterpreter
	{
		public bool ValidOnClient => true;
		public bool ValidOnServer => true;

		public void Interpret(INetworkMediator netHost, EndPoint sender, IMessage message)
		{
			var m = (ForgeReceiptAcknowledgementMessage)message;
			netHost.MessageBus.MessageConfirmed(m.ReceiptSignature);
		}
	}
}
