using UnityEngine;
using BDArmory.Misc;
using BDArmory.Parts;
using BDArmory.FX;

namespace NSIExplModule
{
    class NSIExplosionModule : PartModule
    {
        bool hasExploded = false;
        MissileLauncher weapon;
        private float blastPower;
        private float blastHeat;
        [KSPField(isPersistant = false)]
        public string explAirPath = "BDArmory/Models/explosion/explosionLarge";
        [KSPField(isPersistant = false)]
        public string explGroundPath = "BDArmory/Models/explosion/explosionLarge";
        [KSPField(isPersistant = false)]
        public string explSoundPath = "NSI/MilitaryDivision/Sounds/MK84";
        [KSPAction("Explode")]
        public void DetonateAG(KSPActionParam param)
        {
            {
                Explode();
            }
        }
        public override void OnStart(StartState state)
        {
            part.OnJustAboutToBeDestroyed += new Callback(Explode);
        }
        public override void OnInitialize()
        {
            weapon = GetComponent<MissileLauncher>();
        }
        void Explode()
        {
            blastPower = weapon.blastPower;
            blastHeat = weapon.blastHeat;
            Color white = new Color(1, 1, 1, 1);
            Color whitef = new Color(1, 1, 1, 0);
            weapon.vessel.GetHeightFromTerrain();
            Vector3 position = transform.position;
            Vector3 direction = transform.up;
            Quaternion rotation = Quaternion.LookRotation(VectorUtils.GetUpDirection(position));
            if (!hasExploded && weapon.vessel.heightFromTerrain <= 69999 && weapon.vessel.heightFromTerrain >= 501 && weapon.TimeFired >= weapon.dropTime)
            {
                hasExploded = true;
                part.temperature = part.maxTemp + blastHeat;
                GameObject source = new GameObject();
                source.SetActive(true);
                source.transform.position = position;
                source.transform.rotation = rotation;
                source.transform.up = direction;
                source.transform.localRotation = rotation;
                CameraFade flashEffect = source.AddComponent<CameraFade>();
                source.GetComponent<CameraFade>().enabled = true;
                flashEffect.SetScreenOverlayColor(white);
                flashEffect.StartFade(whitef, 2.0f);
                ExplosionFx.CreateExplosion(position, blastPower, explAirPath, explSoundPath, true, 0, null, default(Vector3));
            }
            else
            {
                if (!hasExploded && weapon.TimeFired >= weapon.dropTime && weapon.vessel.heightFromTerrain <= 500)
                {
                    hasExploded = true;
                    GameObject source = new GameObject();
                    source.SetActive(true);
                    source.transform.position = position;
                    source.transform.rotation = rotation;
                    source.transform.up = direction;
                    source.transform.localRotation = rotation;
                    CameraFade flashEffect = source.AddComponent<CameraFade>();
                    source.GetComponent<CameraFade>().enabled = true;
                    flashEffect.SetScreenOverlayColor(white);
                    flashEffect.StartFade(whitef, 2.0f);
                    ExplosionFx.CreateExplosion(position, blastPower, explGroundPath, explSoundPath, true, 0, null, default(Vector3));
                }
            }
        }
    }
}

