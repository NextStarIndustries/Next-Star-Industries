using UnityEngine;

namespace NextStarIndustries
{
    public class ModuleShockwave : PartModule
    {
        [KSPField(isPersistant = false)]
        public float explosiveYield = 100;

        [KSPField(isPersistant = false)]
        public float particleSize = 5;

        [KSPField(isPersistant = false)]
        public string shockPath = "NSI/MilitaryDivision/Weapons/NKD/effects/shockwave";

        public override void OnStart(PartModule.StartState state)
        {
            part.OnJustAboutToBeDestroyed += new Callback(Detonate);
            part.force_activate();
        }

        public void Detonate()
        {
            GameObject shockCenter = new GameObject("Shockwave Center");
            shockCenter.transform.position = gameObject.transform.position;
            Shockwave shockEmit = shockCenter.AddComponent<Shockwave>();
            shockEmit.maxRadius = 14.8f * (Mathf.Pow(explosiveYield, 1 / 3));
            shockEmit.shockModelPath = shockPath;
            shockEmit.partSize = particleSize;
            if (part != null)
            {
                part.temperature = part.maxTemp + 100;
            }
        }
    }
}
