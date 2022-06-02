using steroid_port.Game.Factories;

namespace steroid_port.Game.Systems.Base
{
    public abstract class System
    {
        public StateFactory StateFactory;
        
        public virtual void Init(){}
        public virtual void Update(){}
        public virtual void Reset(){}
    }
}