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

using System;
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
        public CartesianCoordinates3D Predict(double times=1)
        {
            for (int i = 0; i < times; i++)
            {
                Ukf.Predict();
            }

            var result = Ukf.GetState();
            if (result == null) return null;

            var sph = new SphericalCoordinater3D(result);

            return SystemConverter3D.ConvertToCartesianCoord(sph);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public CartesianCoordinates3D Update(CartesianCoordinates3D c)
        {
            try
            {

                SphericalCoordinater3D sph = new SphericalCoordinater3D();

                sph = SystemConverter3D.ConvertToSphericalCoord(c);

                var mesaurement = sph.ToArray();
                   
                    Ukf.Update(mesaurement);

                    var result = Ukf.GetState();
                    sph = new SphericalCoordinater3D(result);

                    return SystemConverter3D.ConvertToCartesianCoord(sph);
                
            }
            catch (Exception)
            {
                return null;

            }

        }
    }
}