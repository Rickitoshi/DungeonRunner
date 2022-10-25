using UnityEngine;
using Zenject;

public class InputHandler: ITickable
{
    public bool LeftSwipe { get; private set; }
    public bool RightSwipe { get; private set; }
    public bool UpSwipe { get; private set; }
    public bool DownSwipe { get; private set; }

    private bool _isSwiped;

    public void Tick()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            Vector2 delta = touch.deltaPosition;
            
            if (delta != Vector2.zero && !_isSwiped)
            {
                _isSwiped = true;
                
                if(Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                {
                    RightSwipe = delta.x > 0;
                    LeftSwipe = delta.x < 0;
                }
                else
                {
                    UpSwipe = delta.y > 0;
                    DownSwipe = delta.y < 0;
                }
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
