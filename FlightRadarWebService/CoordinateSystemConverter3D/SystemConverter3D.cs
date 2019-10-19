using System;

namespace FlightRadarWebService.CoordinateSystemConverter3D
{

    public static class SystemConverter3D
    {
        public static SphericalCoordinater3D ConvertToSphericalCoord(CartesianCoordinates3D c)
        {
            try
            {
                SphericalCoordinater3D sph = new SphericalCoordinater3D();

                double alt2 = c.Altitude * c.Altitude;
                double lng2 = c.Longitude * c.Longitude;
                double lat2 = c.Latitude * c.Latitude;


                sph.R = Math.Sqrt(lat2 + lng2);


                //A is an azimuth angle, which is the angle between 
                //the longitude-axis and latitude-axis of the plane  
                sph.A = Math.Atan(c.Latitude / c.Longitude);


                sph.A = correctAzimuthInRadians(c.Longitude, c.Latitude, sph.A);


                //I is an inclination, which is the angle between the radius R 
                // and the vector from the center to the point 

                //sph.I = Math.Acos(c.Z / sph.R2);
                sph.D = Math.Atan(c.Altitude / sph.R);

                return sph;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("longitude is null");
                throw;
                
            }

        }

        public static CartesianCoordinates3D ConvertToCartesianCoord(SphericalCoordinater3D sph)
        {
            if (sph == null) return null;

            CartesianCoordinates3D c = new CartesianCoordinates3D();

            //x is longitude
            //y is latitude
            //z is altitude

            c.Longitude = Math.Cos(sph.A) * sph.R;
            c.Latitude = Math.Sin(sph.A) * sph.R;
            //c.Z = Math.Cos(sph.I) * sph.R2;
            c.Altitude = Math.Tan(sph.D) * sph.R;

            return c;
        }

        static double convertRadiansToDegree(double r) { return (r * 180) / Math.PI; }
        static double convertDegreeToRadians(double d) { return (d * Math.PI) / 180; }


        static double correctAzimuthInRadians(double x, double y, double aRadians)
        {
            if (y == 0)
            {
                if (x < 0) return Math.PI;
                else return 0;
            }
            if (x == 0)
            {
                if (y > 0) return Math.PI / 2;
                else if (y < 0) return (3 / 2) * Math.PI;
                else return 0;
            }

            if (y < 0)
            {
                if (x > 0) return aRadians + (2 * Math.PI);
                else return aRadians + Math.PI;
            }
            else
            {
                if (x > 0) return aRadians;
                else return aRadians + Math.PI;
            }

        }


    }
}
