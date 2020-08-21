using UnityEngine;

namespace NextStarIndustries
{
    internal class SonicBoombox : PartModule
    {
        private AudioSource SBSettings;
        private double speed;

        public override void OnInitialize()
        {
            SetupAudio();
        }

        public override void OnUpdate()
        {
            speed = part.machNumber;

            if (speed >= 1.00 && speed <= 1.001)
            {
                Boom();
            }
        }

        private void SetupAudio()
        {
            SBSettings = gameObject.AddComponent<AudioSource>();
            gameObject.GetComponent<AudioSource>().enabled = true;
            SBSettings.volume = 0.75f;
            SBSettings.minDistance = 1;
            SBSettings.maxDistance = 2000;
            SBSettings.dopplerLevel = 0;
            SBSettings.priority = 230;
            SBSettings.spatialBlend = 1;
        }

        private void Boom()
        {
            SBSettings.PlayOneShot(GameDatabase.Instance.GetAudioClip("NSI/Sounds/Sonic Boom-SoundBible.com-876321507"));
        }
    }
}
