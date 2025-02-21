
using UnityEngine;

namespace Pikamoon.Controller
{
    public interface IPickable
    {
        void OnPicked();
        void OnPicked(Transform Picker);
    }
}