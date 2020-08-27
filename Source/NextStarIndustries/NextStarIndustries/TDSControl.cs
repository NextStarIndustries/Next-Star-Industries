using UnityEngine;

namespace NextStarIndustries
{
    public class TDSControl : PartModule
    {
        private double Lat;
        private double Lon;
        private double Alt;
        private double HSpeed;
        private double VSpeed;

        public override void OnInitialize()
        {
            Debug.Log("TDSControl Active");
            Debug.Log("Lat:" + Lat);
            Debug.Log("Lon:" + Lon);
            Debug.Log("Alt:" + Alt);
            Debug.Log("HSpeed:" + HSpeed);
            Debug.Log("VSpeed:" + VSpeed);
        }

        public override void OnUpdate()
        {
            Lat = vessel.latitude;
            Lon = vessel.longitude;
            Alt = vessel.heightFromTerrain;
            HSpeed = vessel.horizontalSrfSpeed;
            VSpeed = vessel.verticalSpeed;
        }
    }
}
