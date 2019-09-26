using System.Threading.Tasks;

namespace FlightRadarWebService.Core
{
    /// <summary>
    /// </summary>
    public class KalmanRunner
    {
        private static KalmanRunner _kalmanRunnerInstance;

        /// <summary>
        /// 
        /// </summary>
        private static Task<int> task ;

        private KalmanRunner()
        {}

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


        /// <summary>
        /// </summary>
        public static void KalmanStarter()
        {
           
        }
    }
}