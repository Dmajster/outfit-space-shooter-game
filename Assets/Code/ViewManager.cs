using UnityEngine;

namespace Assets.Code
{
    public class ViewManager : Singleton<ViewManager>
    {
        public Vector2 MinimumView;
        public Vector2 MaximumView;
        public Vector2 Center;

        private void Start()
        {
            MinimumView = Camera.main.ScreenToWorldPoint(Vector3.zero);
            MaximumView = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
            Center = MinimumView + (MaximumView - MinimumView) / 2;
        }
    }
}
