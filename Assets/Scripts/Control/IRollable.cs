using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VeggieNightmare.Control
{
    public interface IRollable //tomatoes roll about the z axis, while mushrooms roll about the y-axis
    {
        void Roll();
    }
}