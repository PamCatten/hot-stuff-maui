
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace HotStuff.Messages;
public class ActiveBuildingMessage : ValueChangedMessage<Building>
{
    public ActiveBuildingMessage(Building building) : base(building)
    {
    }
}
