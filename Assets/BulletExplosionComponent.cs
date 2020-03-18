using UnityEngine;

namespace Assets
{
    public class BulletExplosionComponent : MonoBehaviour
    {


        // Start is called before the first frame update
        void Start()
        {
            Destroy(gameObject, 0.2f);
        }
    }
}
