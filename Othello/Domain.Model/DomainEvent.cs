using Prism.Events;

namespace Othello.Domain.Model
{
    public class PutStoneEvent : PubSubEvent<Position> { }
}