namespace FlightRadarWebService.CoordinateSystemConverter3D
{
    public class CartesianCoordinates3D
    {
        /// <summary>
        /// Values initialization  
        /// </summary>
        public CartesianCoordinates3D()
        {
            Altitude = Latitude = Longitude = 0;
        }

        /// <summary>
        /// Longitude, Latitude, Altitude initialization
        /// </summary>
        /// <param name="alt"></param>
        /// <param name="lng"></param>
        /// <param name="lat"></param>
        public CartesianCoordinates3D(double alt, double lng, double lat)
        {
            Altitude = alt;
            Latitude = lat;
            Longitude = lng;

        }

        /// <summary>
        /// Longitude value
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Latitude value
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Altitude value
        /// </summary>
        public double Altitude { get; set; }


    }
}
