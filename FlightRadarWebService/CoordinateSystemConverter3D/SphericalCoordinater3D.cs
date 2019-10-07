using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightRadarWebService.CoordinateSystemConverter3D
{
    public class SphericalCoordinater3D
    {
        //R is a radius, which is the distance between center and the point 
        //public double R2 { get; set; }

        //R is a radius between longitude-axis and latitude-axis 
        public double R { get; set; }

        //I is the declination angle, which is the angle between the radius R 
        // and the vector from the center to the plane 
        public double D { get; set; }

        //A is an azimuth angle, which is the angle between 
        //the longitude-axis and latitude-axis of the plane        
        public double A { get; set; }

        public SphericalCoordinater3D()
        {
            this.A = this.D = this.R = 0;
        }

        public SphericalCoordinater3D(double[] values)
        {
            this.A = values[0];
            this.D = values[1];
            this.R = values[2];
        }

        public double[] ToArray()
        {
            return new double[]
            {
                A,D,R
            };
        }

    }
}
