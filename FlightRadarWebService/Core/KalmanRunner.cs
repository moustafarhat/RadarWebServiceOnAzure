////////////////////////////////////////////////////////////////////
//FileName: IDataBaseOperations.cs
//FileType: Visual C# Source file
//Size : 0
//Author : Moustafa Farhat
//Created On : 0
//Last Modified On : 0
//Copy Rights : Flight Radar API
//Description : Interface contains all Data Transmission operations
////////////////////////////////////////////////////////////////////
using System.Collections.Generic;
using FlightRadarWebService.CoordinateSystemConverter3D;
using UnscentedKalmanFilter;

namespace FlightRadarWebService.Core
{
    /// <summary>
    /// Kalman Algorithm Instance
    /// </summary>
    public class KalmanRunner
    {
        public Ukf Ukf;
        private static KalmanRunner _kalmanRunnerInstance;
        /// <summary>
        /// 
        /// </summary>
        public KalmanRunner()
        {
            Ukf = new Ukf();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public static KalmanRunner GetKalmanRunner()
        {
            if (_kalmanRunnerInstance == null)
            {
                _kalmanRunnerInstance = new KalmanRunner();
            }
            return _kalmanRunnerInstance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double[] GetState()
        {
            return Ukf.GetState();
        }

        public void Predict()
        {
            //Ukf.Predict();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lng"></param>
        /// <param name="lat"></param>
        /// <param name="alt"></param>
        /// <returns></returns>

        public double[] Update(double? lng, double? lat, double? alt)
        {
            CartesianCoordinates3D objt=new CartesianCoordinates3D(lng.Value,lat.Value,alt.Value);
            
            var result= SystemConverter3D.ConvertToSphericalCoord(objt);
            
            
            if (!lng.HasValue)
            {
                Ukf.Predict();
            }
            else
            {
                var listval = new List<double> { lng.Value, lat.Value, alt.Value };
                Ukf.Update(listval.ToArray());
            }

            return Ukf.GetState();
        }
    }
}