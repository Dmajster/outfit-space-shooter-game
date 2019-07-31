using Assets.Code.Abstractions;
using UnityEngine;

namespace Assets.Code
{
    public class ViewManager : Singleton<ViewManager>
    {
        // This are fields instead of members because the view is static and recalculating data that doesn't change is wasteful 
        public Vector2 MinimumView;
        public Vector2 MaximumView;
        public Vector2 Center;
        public Vector2 Dimensions;

        private void Start()
        {
            // Minimum world position we can still see on screen
            MinimumView = Camera.main.ScreenToWorldPoint(Vector3.zero);

            // Maximum world position we can still see on screen
            MaximumView = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));

            // Dimensions of the view in world scales;
            Dimensions = MaximumView - MinimumView;

            // World coordinates for center of sreen;
            Center = MinimumView + (Dimensions) / 2;
        }
    }
}
