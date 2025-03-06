
using UnityEngine;

namespace Pikamoon.Controller
{
    public interface IDroppable
    {
        void OnDrop();
        void OnDrop(Transform Dropper, LayerMask DropLayer);
    }
}