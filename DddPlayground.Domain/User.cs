using DddPlayground.Infrastrcuture.EventSourcing;
using DddPlayground.Infrastructure.EventSourcing;
using System;

namespace DddPlayground.Domain
{
    public static class User
    {
        public class Aggregate
        {
            public UserState State { get; private set; }

            public Aggregate(UserState state)
            {
                State = state;

                if (State.HasNewIdentity)
                {
                    EventRaiser.RaiseEvent(this, new UserCreated { Id = State.Id });
                }
            }

            public Aggregate(string name) : this(new UserState())
            {
                State.Name = name;
            }

            public void ChangeName(string name)
            {
                State.Name = name;

                EventRaiser.RaiseEvent(this, new NameChanged(State.Id, State.Name, name));
            }
        }

        public class UserState
        {
            public long Id { get; set; }

            public bool HasNewIdentity { get; set; }

            public string Name { get; set; }
        }

        public class UserCreated : IEvent
        {
            public long Id { get; set; }
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
