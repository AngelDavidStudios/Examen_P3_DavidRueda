using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Examen_PF.Utility;

public class JokeMessaging: ValueChangedMessage<JokeMessage>
{
    public JokeMessaging(JokeMessage value) : base(value)
    {
    }
}