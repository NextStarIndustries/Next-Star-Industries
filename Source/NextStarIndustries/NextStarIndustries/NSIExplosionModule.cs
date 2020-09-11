using UnityEngine;
using BDArmory.Misc;
using BDArmory.FX;
using BDArmory.Modules;

namespace NextStarIndustries
{
    public class NSIExplosionModule : PartModule
    {
        private float blastRadius;
        private float blastPower;
        private bool isMissile;
        private float caliber;
        private Part explosivePart;
        [KSPField(isPersistant = false)]
        public string explSpacePath = "BDArmory/Models/explosion/explosionLarge";

        [KSPField(isPersistant = false)]
        public string explAirPath = "BDArmory/Models/explosion/explosionLarge";

        [KSPField(isPersistant = false)]
        public string explGroundPath = "BDArmory/Models/explosion/explosionLarge";

        [KSPField(isPersistant = false)]
        public string explSoundPath = "NSI/MilitaryDivision/Sounds/MK84";

        [KSPField(isPersistant = false)]
        public float explosiveYield = 100;

        [KSPField(isPersistant = false)]
        public float particleSize = 5;

        [KSPField(isPersistant = false)]
        public string shockPath = "NSI/MilitaryDivision/Weapons/NKD/effects/shockwave";

        [KSPAction("Explode")]
        public void DetonateAG(KSPActionParam param)
        {
            Explode();
        }

        bool hasExploded;

        MissileLauncher weapon;
        BDExplosivePart weapon2;
        public override void OnStart(PartModule.StartState state)
        {
            part.OnJustAboutToBeDestroyed += new Callback(Explode);
            part.force_activate();
        }

        public override void OnInitialize()
        {
            weapon = GetComponent<MissileLauncher>();
            weapon2 = GetComponent<BDExplosivePart>();
        }

        public void Explode()
        {
            Color white = new Color(1, 1, 1, 1);
            Color whitef = new Color(1, 1, 1, 0);

            weapon.vessel.GetHeightFromTerrain();
            blastRadius = weapon2.GetBlastRadius();
            blastPower = weapon2.tntMass;

            Vector3 position = transform.position;
            Vector3 direction = transform.up;
            Quaternion rotation = Quaternion.LookRotation(VectorUtils.GetUpDirection(position));

            if (!hasExploded && weapon.TimeFired >= weapon.dropTime && weapon.vessel.heightFromTerrain >= 70000)
            {
                hasExploded = true;

                if (part != null) part.temperature = part.maxTemp + 100;

                GameObject lightBall = new GameObject();
                lightBall.SetActive(true);
                lightBall.transform.position = position;
                lightBall.transform.rotation = rotation;
                Detonator sdetonator = lightBall.AddComponent<Detonator>();
                lightBall.GetComponent<Detonator>().enabled = true;
                sdetonator.duration = 2f;
                sdetonator.size = blastRadius;
                sdetonator.detail = 5.0f;

                Debug.Log("Space Explosion Activated");
            }
            else
            {
                if (!hasExploded && weapon.vessel.heightFromTerrain <= 69999 && weapon.vessel.heightFromTerrain >= 501 && weapon.TimeFired >= weapon.dropTime && weapon.blastRadius >= 1000)
                {
                    hasExploded = true;

                    if (part != null) part.temperature = part.maxTemp + 100;

                    GameObject source = new GameObject();
                    source.SetActive(true);
                    source.transform.position = position;
                    source.transform.rotation = rotation;
                    source.transform.up = direction;
                    //Detonator ndetonator = source.AddComponent<Detonator>();
                    //source.GetComponent<Detonator>().enabled = true;
                    //ndetonator.direction = direction;
                    //ndetonator.duration = 5.0f;
                    //ndetonator.size = blastRadius;
                    //ndetonator.detail = 5.0f;
                    CameraFade flashEffect = source.AddComponent<CameraFade>();
                    source.GetComponent<CameraFade>().enabled = true;
                    flashEffect.SetScreenOverlayColor(white);
                    flashEffect.StartFade(whitef, 2.0f);
                    ExplosionFx.CreateExplosion(position, blastPower, explAirPath, explSoundPath, isMissile = true, caliber = 0, explosivePart = null, default);

                    Debug.Log("Air NExplosion Activated");
                }
                else
                {
                    if (!hasExploded && weapon.vessel.heightFromTerrain <= 69999 && weapon.vessel.heightFromTerrain >= 501 && weapon.TimeFired >= weapon.dropTime && weapon.blastRadius <= 1000)
                    {
                        hasExploded = true;

                        if (part != null) part.temperature = part.maxTemp + 100;

                        GameObject csource = new GameObject();
                        csource.SetActive(true);
                        csource.transform.position = position;
                        csource.transform.rotation = rotation;
                        csource.transform.up = direction;
                        //Detonator cdetonator = csource.AddComponent<Detonator>();
                        //csource.GetComponent<Detonator>().enabled = true;
                        //cdetonator.direction = direction;
                        //cdetonator.duration = 3.0f;
                        //cdetonator.size = blastRadius;
                        //cdetonator.detail = 5.0f;
                        ExplosionFx.CreateExplosion(position, blastPower, explAirPath, explSoundPath, isMissile = true, caliber = 0, explosivePart = null, default);

                        Debug.Log("Air CExplosion Activated");
                    }
                    else
                    {
                        if (!hasExploded && weapon.TimeFired >= weapon.dropTime && weapon.vessel.heightFromTerrain <= 500 && weapon.blastRadius >= 1000)
                        {
                            hasExploded = true;

                            if (part != null) part.temperature = part.maxTemp + 100;

                            GameObject source = new GameObject();
                            source.SetActive(true);
                            source.transform.position = position;
                            source.transform.rotation = rotation;
                            source.transform.up = direction;
                            //Detonator ndetonator = source.AddComponent<Detonator>();
                            //source.GetComponent<Detonator>().enabled = true;
                            //ndetonator.direction = direction;
                            //ndetonator.duration = 5.0f;
                            //ndetonator.size = blastRadius;
                            //ndetonator.detail = 5.0f;
                            CameraFade flashEffect = source.AddComponent<CameraFade>();
                            source.GetComponent<CameraFade>().enabled = true;
                            flashEffect.SetScreenOverlayColor(white);
                            flashEffect.StartFade(whitef, 2.0f);

                            GameObject shockCenter = new GameObject("Shockwave Center");
                            shockCenter.transform.position = gameObject.transform.position;
                            Shockwave shockEmit = shockCenter.AddComponent<Shockwave>();
                            shockEmit.maxRadius = 14.8f * (Mathf.Pow(explosiveYield, 1 / 3));
                            shockEmit.shockModelPath = shockPath;
                            shockEmit.partSize = particleSize;

                            ExplosionFx.CreateExplosion(position, blastPower, explGroundPath, explSoundPath, isMissile = true, caliber = 0, explosivePart = null, default);
                            Debug.Log("Ground NExplosion Activated");
                        }
                        else
                        {
                            if (!hasExploded && weapon.TimeFired >= weapon.dropTime && weapon.vessel.heightFromTerrain <= 500 && weapon.blastRadius <= 1000)
                            {
                                hasExploded = true;

                                if (part != null) part.temperature = part.maxTemp + 100;

                                GameObject csource = new GameObject();
                                csource.SetActive(true);
                                csource.transform.position = position;
                                csource.transform.rotation = rotation;
                                csource.transform.up = direction;
                                //Detonator cdetonator = csource.AddComponent<Detonator>();
                                //csource.GetComponent<Detonator>().enabled = true;
                                //cdetonator.direction = direction;
                                //cdetonator.duration = 3.0f;
                                //cdetonator.size = blastRadius;
                                //cdetonator.detail = 5.0f;
                                ExplosionFx.CreateExplosion(position, blastPower, explGroundPath, explSoundPath, isMissile = true, caliber = 0, explosivePart = null, default);

                                Debug.Log("Ground CExplosion Activated");
                            }
                        }
                    }
                }
            }
        }
    }
}
