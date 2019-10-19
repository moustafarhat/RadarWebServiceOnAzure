namespace FlightRadarWebService.CoordinateSystemConverter3D
{
    using System;

    public static class SystemConverter3D
    {
        /// <summary>
        /// Convert from CartesianCoordinates3D to SphericalCoordinater3D
        /// </summary>
        /// <param name="cartesianCoordinates"></param>
        /// <returns></returns>
        public static SphericalCoordinater3D ConvertToSphericalCoordinator(CartesianCoordinates3D cartesianCoordinates)
        {
            try
            {
                var sph = new SphericalCoordinater3D();

                var alt2 = cartesianCoordinates.Altitude * cartesianCoordinates.Altitude;
                var lng2 = cartesianCoordinates.Longitude * cartesianCoordinates.Longitude;
                var lat2 = cartesianCoordinates.Latitude * cartesianCoordinates.Latitude;

                sph.R = Math.Sqrt(lat2 + lng2);

                //A is an azimuth angle, which is the angle between
                //the longitude-axis and latitude-axis of the plane
                sph.A = Math.Atan(cartesianCoordinates.Latitude / cartesianCoordinates.Longitude);

                sph.A = CorrectAzimuthInRadians(cartesianCoordinates.Longitude, cartesianCoordinates.Latitude, sph.A);

                //I is an inclination, which is the angle between the radius R
                // and the vector from the center to the point

                //sph.I = Math.Acos(c.Z / sph.R2);
                sph.D = Math.Atan(cartesianCoordinates.Altitude / sph.R);

                return sph;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("longitude is null");
                throw;
            }
        }

        /// <summary>
        /// Convert from SphericalCoordinator3D to CartesianCoordinates3D
        /// </summary>
        /// <param name="sphericalCoordinator"></param>
        /// <returns></returns>
        public static CartesianCoordinates3D ConvertToCartesianCoordinator(SphericalCoordinater3D sphericalCoordinator)
        {
            if (sphericalCoordinator == null) return null;

            var c = new CartesianCoordinates3D
            {
                Longitude = Math.Cos(sphericalCoordinator.A) * sphericalCoordinator.R,
                Latitude = Math.Sin(sphericalCoordinator.A) * sphericalCoordinator.R,
                Altitude = Math.Tan(sphericalCoordinator.D) * sphericalCoordinator.R
            };

            //x is longitude
            //y is latitude
            //z is altitude

            //c.Z = Math.Cos(sph.I) * sph.R2;

            return c;
        }

        private static double ConvertRadiansToDegree(double r)
        { return r * 180 / Math.PI; }

        private static double ConvertDegreeToRadians(double d)
        { return d * Math.PI / 180; }

        private static double CorrectAzimuthInRadians(double x, double y, double aRadians)
        {
            if (y == 0)
            {
                if (x < 0) return Math.PI;
                return 0;
            }
            if (x == 0)
            {
                if (y > 0) return Math.PI / 2;
                if (y < 0) return (3 / 2) * Math.PI;
                return 0;
            }

            if (y < 0)
            {
                if (x > 0) return aRadians + (2 * Math.PI);
                return aRadians + Math.PI;
            }

            if (x > 0) return aRadians;
            return aRadians + Math.PI;
        }
    }
}