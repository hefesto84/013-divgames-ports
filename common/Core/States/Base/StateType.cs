using System;

namespace common.Core.States.Base
{
    public class StateType
    {
        public Type Type { get; }
        
        public StateType(Type type)
        {
            Type = type;
        }
    }
}