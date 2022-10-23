using UnityEngine;
using Zenject;

public class InputHandler: ITickable
{
    public float HorizontalAxis { get; private set; }
    public float VerticalAxis { get; private set; }
    
    
    public void Tick()
    {
        if (Input.touchCount <= 0)
        {
            HorizontalAxis = 0;
            VerticalAxis = 0;
            return;
        }
        
        Vector2 delta = Input.GetTouch(0).deltaPosition;
        HorizontalAxis = delta.x;
        VerticalAxis = delta.y;
    }
}
