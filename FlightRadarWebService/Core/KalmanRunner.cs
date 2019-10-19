using System;
using UnscentedKalmanFilter;
using FlightRadarWebService.CoordinateSystemConverter3D;


namespace FlightRadarWebService.Core
{
    /// <summary>
    /// Kalman Algorithm Instance
    /// </summary>
    public class KalmanRunner
    {
        /// <summary>
        /// Kalman Instance
        /// </summary>
        private readonly Ukf Ukf;

        /// <summary>
        /// Static Instance
        /// </summary>
        private static KalmanRunner _kalmanRunnerInstance;

        /// <summary>
        /// Initialization
        /// </summary>
        public KalmanRunner()
        {
            Ukf = new Ukf();
        }


        /// <summary>
        /// Static Instance
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
        /// Predict Values
        /// </summary>
        /// <returns></returns>
        public CartesianCoordinates3D Predict(double times = 1)
        {
            for (var i = 0; i < times; i++)
            {
                Ukf.Predict();
            }

            var result = Ukf.GetState();
            if (result == null) return null;

            var sph = new SphericalCoordinater3D(result);

            return SystemConverter3D.ConvertToCartesianCoordinator(sph);
        }

        /// <summary>
        /// Update System State
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public CartesianCoordinates3D Update(CartesianCoordinates3D c)
        {
            try
            {
                var sphericalCoordinater3D = new SphericalCoordinater3D();

                sphericalCoordinater3D = SystemConverter3D.ConvertToSphericalCoordinator(c);

                var measurement = sphericalCoordinater3D.ToArray();

                Ukf.Update(measurement);

                var result = Ukf.GetState();
                sphericalCoordinater3D = new SphericalCoordinater3D(result);

                return SystemConverter3D.ConvertToCartesianCoordinator(sphericalCoordinater3D);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}