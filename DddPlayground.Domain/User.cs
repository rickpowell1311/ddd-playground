using DddPlayground.Infrastructure.EventSourcing;
using System;

namespace DddPlayground.Domain
{
    public static class User
    {
        public class Aggregate
        {
            public State State { get; }

            public Aggregate(State state)
            {
                State = state;
            }

            public Aggregate(string name) : this(new State())
            {
                State.Name = name;
            }

            public void ChangeName(string name, IEventDispatcher eventDispatcher)
            {
                eventDispatcher.Dispatch(new NameChanged(State.Id, State.Name, name));

                State.Name = name;
            }
        }

        public class State
        {
            public long Id { get; set; }

            public string Name { get; set; }
        }

        public class NameChanged : IEvent
        {
            public long UserId { get; }

            public string OldName { get; }

            public string NewName { get; }

            public NameChanged(long userId, string oldName, string newName)
            {
                UserId = userId;
                OldName = oldName;
                NewName = newName;
            }
        }
    }
}
