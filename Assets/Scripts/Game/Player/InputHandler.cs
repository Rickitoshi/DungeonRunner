using UnityEngine;
using Zenject;

public class InputHandler: ITickable
{
    private const float DEAD_ZONE = 0.01f;
    
    public bool LeftSwipe { get; private set; }
    public bool RightSwipe { get; private set; }
    public bool UpSwipe { get; private set; }
    public bool DownSwipe { get; private set; }
    
    private Vector3 _prevPos = Vector3.zero;
    private Vector2 _normalizeDelta =  Vector2.zero;
    private bool _isHold;
    private bool _isSwiped;

    public void Tick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isHold = true;
            _prevPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isHold = false;
            _prevPos = Vector3.zero;
        }

        if (_isHold)
        {
            CalculateDelta();
            
            if (_normalizeDelta != Vector2.zero && !_isSwiped)
            {
                _isSwiped = true;

                if (Mathf.Abs(_normalizeDelta.x) > Mathf.Abs(_normalizeDelta.y) - DEAD_ZONE)
                {
                    RightSwipe = _normalizeDelta.x > 0;
                    LeftSwipe = _normalizeDelta.x < 0;
                }
                else
                {
                    UpSwipe = _normalizeDelta.y > 0;
                    DownSwipe = _normalizeDelta.y < 0;
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

    private void CalculateDelta()
    {
        Vector3 delta = Input.mousePosition - _prevPos;

        _normalizeDelta.x = delta.x / Screen.width;
        _normalizeDelta.y = delta.y / Screen.height;

        _prevPos = Input.mousePosition;
    }
}
