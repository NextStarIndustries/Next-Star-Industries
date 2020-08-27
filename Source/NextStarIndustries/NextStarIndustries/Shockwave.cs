using UnityEngine;

namespace NextStarIndustries
{
    public class Shockwave : MonoBehaviour
    {
        KSPParticleEmitter shockParticles;
        public string shockModelPath;
        public float maxRadius;
        public float partSize;
        public float height;
        float rad;
        readonly float sizeConst = 0.01f;

        public void Start()
        {
            GameObject shockModel = GameDatabase.Instance.GetModel(shockModelPath);
            GameObject shockCenter = Instantiate(shockModel);
            shockParticles = shockCenter.GetComponent<KSPParticleEmitter>();
            shockParticles.enabled = true;
            if (height > 0)
            {
                shockParticles.shape2D.x = 0;
                shockParticles.shape2D.y = 0;
            }
        }

        public void FixedUpdate()
        {
            rad += 11;
            height -= 11;
            shockParticles.sizeGrow = 0.05f;
            if(rad < maxRadius && height <= 0)
            {
                shockParticles.shape2D += new Vector2(11, 11);
                shockParticles.shape2D.x -= sizeConst;
                shockParticles.shape2D.y -= sizeConst;
                shockParticles.Emit();
            }
        }
    }
}