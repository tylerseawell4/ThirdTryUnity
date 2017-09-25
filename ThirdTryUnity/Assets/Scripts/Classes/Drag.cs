
using UnityEngine;

public static class Drag{

    public static DragEnum GetDragDirection(Vector3 dragVector)
    {
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);
        DragEnum draggedDir;
        if (positiveX > positiveY)
        {
            draggedDir = (dragVector.x > 0) ? DragEnum.Right : DragEnum.Left;
        }
        else
        {
            draggedDir = (dragVector.y > 0) ? DragEnum.Up : DragEnum.Down;
        }
        Debug.Log(draggedDir);
        return draggedDir;
    }
}
