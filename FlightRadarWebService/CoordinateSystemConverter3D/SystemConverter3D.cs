using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightRadarWebService.CoordinateSystemConverter3D
{
    public static class SystemConverter3D
    {
        public static SphericalCoordinater3D ConvertToSphericalCoord(CartesianCoordinates3D c)
        {
            var sph = new SphericalCoordinater3D();

            var x2 = c.X * c.X;
            var y2 = c.Y * c.Y;
            var z2 = c.Z * c.Z;

            //sph.R2 = Math.Sqrt(x2 + y2 + z2);
            sph.R = Math.Sqrt(x2 + y2);

            //A is an azimuth, which is the angle between the X-axis and Y-axis of the point
            sph.A = Math.Atan(c.Y / c.X);

            sph.A = correctAzimuthInRadians(c.X, c.Y, sph.A);

            //I is an inclination, which is the angle between the radius R 
            // and the vector from the center to the point 

            //sph.I = Math.Acos(c.Z / sph.R2);
            sph.D = Math.Atan(c.Z / sph.R);

            return sph;
        }

        public static CartesianCoordinates3D ConvertToCartesianCoord(SphericalCoordinater3D sph)
        {
            CartesianCoordinates3D c = new CartesianCoordinates3D
            {
                X = Math.Cos(sph.A) * sph.R, 
                Y = Math.Sin(sph.A) * sph.R, 
                Z = Math.Tan(sph.D) * sph.R
            };

            //c.Z = Math.Cos(sph.I) * sph.R2;

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
