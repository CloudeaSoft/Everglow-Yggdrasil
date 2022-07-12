﻿using Everglow.Sources.Commons.Core;

namespace Everglow.Sources.Commons.Function.Player
{
    internal class MouseTrail : INetUpdate<Vector2>
    {
        public const int Capacity = 30;
        private int updateTime = 0;
        public LinkedList<Vector2> position = new LinkedList<Vector2>();
        public void Forcast()
        {
            if(updateTime != HookSystem.UITimer)
            {
                var last = position.Last;
                var lastlast = last.Previous;
                if (position.Count == Capacity)
                {
                    position.RemoveFirst();
                }
                position.AddLast(2 * last.Value - lastlast.Value);
                updateTime = HookSystem.UITimer;
            }
        }

        public void LocalUpdate(Vector2 input)
        {
            if (position.Count == Capacity)
            {
                position.RemoveFirst();
            }
            position.AddLast(input);
        }

        public void NetUpdate(Vector2 input)
        {
            if (updateTime == HookSystem.UITimer)
            {
                position.Last.ValueRef = input;
            }
            else
            {
                if (position.Count == Capacity)
                {
                    position.RemoveFirst();
                }
                position.AddLast(input);
                updateTime = HookSystem.UITimer;
            }
        }
    }
}
