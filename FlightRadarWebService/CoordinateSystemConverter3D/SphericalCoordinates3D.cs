namespace FlightRadarWebService.CoordinateSystemConverter3D
{
    public class SphericalCoordinates3D
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

        public SphericalCoordinates3D()
        {
            A = D = R = 0;
        }

        public SphericalCoordinates3D(double[] values)
        {
            A = values[0];
            D = values[1];
            R = values[2];
        }

        public double[] ToArray()
        {
            return new[]
            {
                A,D,R
            };
        }
    }
}