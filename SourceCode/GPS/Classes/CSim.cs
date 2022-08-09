using System;

namespace AgOpenGPS
{
    public class CSim
    {
        private readonly FormGPS mf;

        #region properties sim
        public double altitude = 300;

        public double latitude, longitude;

        public double headingTrue, stepDistance = 0.0, steerAngle;
        public double steerAngleScrollBar = 0;
        public decimal lastTime;
        public double lastSteer;

        #endregion properties sim

        public CSim(FormGPS _f)
        {
            mf = _f;
            latitude = Properties.Settings.Default.setGPS_SimLatitude;
            longitude = Properties.Settings.Default.setGPS_SimLongitude;
        }

        public void DoSimTick(double _st)
        {
            //decimal milliseconds = DateTime.Now.Ticks / (decimal)TimeSpan.TicksPerMillisecond;
            //decimal eTime = (milliseconds - lastTime)/1000;
            //double dPerS = mf.vehicle.maxSteerAngle * 2 /2;
            //double delta = (dPerS * Convert.ToDouble(eTime));
            //if (_st - steerAngle<0) delta *= -1;

            ////steerAngle = _st;
            //if (Math.Abs((Math.Abs(_st)- Math.Abs(steerAngle))) > Math.Abs(delta))
            //{
            //    Console.WriteLine(_st.ToString());
            //    steerAngle = lastSteer + delta;
            //    Console.WriteLine(delta);
            //    Console.WriteLine(steerAngle.ToString());


            //}

            //else steerAngle = _st;
            //lastSteer = steerAngle;
            //lastTime = milliseconds;
            steerAngle = _st;
            double temp = stepDistance * (Math.Tan(glm.toRadians(steerAngle)) / mf.vehicle.wheelbase);

            headingTrue += temp;
            if (headingTrue > glm.twoPI) headingTrue -= glm.twoPI;
            if (headingTrue < 0) headingTrue += glm.twoPI;


            //Calculate the next Lat Long based on heading and distance
            CalculateNewPostionFromBearingDistance(glm.toRadians(latitude), glm.toRadians(longitude), headingTrue, stepDistance / 1000.0);

            
            mf.pn.ConvertWGS84ToLocal(latitude, longitude, out mf.pn.fix.northing, out mf.pn.fix.easting);

            mf.pn.vtgSpeed = Math.Abs(Math.Round(4 * stepDistance * 10, 1));
            mf.pn.AverageTheSpeed();

            mf.pn.headingTrue = mf.pn.headingTrueDual = glm.toDegrees(headingTrue);

            mf.pn.latitude = latitude;
            mf.pn.longitude = longitude;

            mf.sentenceCounter = 0;
            mf.UpdateFixPosition();
        }

        public void CalculateNewPostionFromBearingDistance(double lat, double lng, double bearing, double distance)
        {
            double R = distance / 6371.0; // Earth Radius in Km

            double lat2 = Math.Asin((Math.Sin(lat) * Math.Cos(R)) + (Math.Cos(lat) * Math.Sin(R) * Math.Cos(bearing)));
            double lon2 = lng + Math.Atan2(Math.Sin(bearing) * Math.Sin(R) * Math.Cos(lat), Math.Cos(R) - (Math.Sin(lat) * Math.Sin(lat2)));

            latitude = glm.toDegrees(lat2);
            longitude = glm.toDegrees(lon2);
        }
    }
}