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
            this.Altitude = this.Latitude = this.Longitude = 0;
        }
        public CartesianCoordinates3D(double alt, double lng, double lat)
        {
            this.Altitude = alt;
            this.Latitude = lat;
            this.Longitude = lng;

        }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Altitude { get; set; }


    }
}
