using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Classes
{
    public static class Drag
    {
        public static DragDirection GetDragDirection(Vector3 dragVector)
        {
            float positiveX = Mathf.Abs(dragVector.x);
            float positiveY = Mathf.Abs(dragVector.y);
            DragDirection draggedDir;
            if (positiveX > positiveY)
            {
                draggedDir = (dragVector.x > 0) ? DragDirection.Right : DragDirection.Left;
            }
            else
            {
                draggedDir = (dragVector.y > 0) ? DragDirection.Up : DragDirection.Down;
            }
            Debug.Log(draggedDir);
            return draggedDir;
        }
    }
}
