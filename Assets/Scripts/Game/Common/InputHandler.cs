using UnityEngine;
using Zenject;

public class InputHandler: ITickable
{
    public bool LeftSwipe { get; private set; }
    public bool RightSwipe { get; private set; }
    public bool UpSwipe { get; private set; }
    public bool DownSwipe { get; private set; }

    private bool _isSwiped;
    private const int DEAD_ZONE = 20;
    
    public void Tick()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            
            if (touch.phase==TouchPhase.Moved && !_isSwiped)
            {
                Vector2 delta = touch.deltaPosition;
                _isSwiped = true;
                RightSwipe = delta.x > DEAD_ZONE;
                LeftSwipe = delta.x < -DEAD_ZONE;
                UpSwipe = delta.y > DEAD_ZONE;
                DownSwipe = delta.y < -DEAD_ZONE;
            }
            else
            {
                RightSwipe = false;
                LeftSwipe = false;
                UpSwipe = false;
                DownSwipe = false;
            }
        }
        else
        {
            _isSwiped = false;
        }
    }
}
