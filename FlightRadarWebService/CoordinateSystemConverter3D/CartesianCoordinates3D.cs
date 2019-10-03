using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightRadarWebService.CoordinateSystemConverter3D
{
    public class CartesianCoordinates3D
    {
        public CartesianCoordinates3D()
        {
            this.X = this.Y = this.Z = 0;
        }
        public CartesianCoordinates3D(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }
}
