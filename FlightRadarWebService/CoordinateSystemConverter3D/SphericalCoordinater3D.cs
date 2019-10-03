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

        //R is a radius between x-axis and y-axis 
        public double R { get; set; }

        //I is the declination angle, which is the angle between the radius R 
        // and the vector from the center to the point 
        public double D { get; set; }

        //A is an azimuth angle, which is the angle between the X-axis and Y-axis of the point        
        public double A { get; set; }
    }
}
