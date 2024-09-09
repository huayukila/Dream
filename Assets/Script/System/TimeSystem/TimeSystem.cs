using System.Collections.Generic;
using UnityEngine;

namespace Framework.Farm
{
    public interface IUpdateTime
    {
        void Pause();
        void Tick();
        void Resume();
    }

    public class Seed: IUpdateTime
    {
        public void Pause()
        {
        }

        public void Tick()
        {
        }

        public void Resume()
        {
        }
    }

    public interface ITimeSystem : ISystem
    {
        void Register(IUpdateTime item);

        void TickAll();
        void Pause();
        void Resume();
    }

    public class TimeSystem : AbstractSystem, ITimeSystem
    {
        private List<IUpdateTime> itemList = new List<IUpdateTime>();

        private bool isPuase;
        protected override void OnInit()
        {
            isPuase = false;
            
        }

        public void Register(IUpdateTime item)
        {
            itemList.Add(item);
        }

        public void TickAll()
        {
            if(isPuase)
                return;
            foreach (var item in itemList)
            {
                item.Tick();
            }
        }

        public void Pause()
        {
            isPuase = true;
            foreach (var item in itemList)
            {
                item.Pause();
            }
        }

        public void Resume()
        {
            isPuase = false;
            foreach (var item in itemList)
            {
                item.Pause();
            }
        }
    }
}