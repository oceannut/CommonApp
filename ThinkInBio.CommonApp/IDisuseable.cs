using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp
{

    public interface IDisuseable<T>
    {

        bool Disused { get; }

        void Disuse(Action<T> action);

        void Use(Action<T> action);

    }

}
