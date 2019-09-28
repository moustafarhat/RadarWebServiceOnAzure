using System.Collections.Generic;
using System.Threading.Tasks;
using UnscentedKalmanFilter;
namespace FlightRadarWebService.Core
{
    /// <summary>
    /// </summary>
    public class KalmanRunner
    {
        private static KalmanRunner _kalmanRunnerInstance;

        public UKF Ukf;
        public KalmanRunner()
        {
            Ukf = new UKF();

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

        #region help Methods


        #endregion


        public double[] Update(double? lng, double? Lat,double? Alt)
        {
            if (!lng.HasValue)
            {
                Ukf.Predict();
            }
            else
            {
                var listval = new List<double> { lng.Value, Lat.Value,Alt.Value };
                Ukf.Update(listval.ToArray());
            }

            return Ukf.getState();


        }

        public void Predict()
        {
            //Ukf.Predict();
        }

        public double[] getState()
        {
            return Ukf.getState();
        }

    }
}