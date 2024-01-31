using UnityEngine;

public static class LevelBounds
{
    public static Bounds bounds
    {
        get
        {
            if (_bounds.size.magnitude == 0)
            {
                float vertical = Camera.main.orthographicSize;
                float horizontal = vertical * Screen.width / Screen.height;
                _bounds = new Bounds(Vector3.zero, new Vector3(horizontal * 2, vertical * 2, 0.0f));
            }

            return _bounds;
        }
    }

    private static Bounds _bounds;
}
