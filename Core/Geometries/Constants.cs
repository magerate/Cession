namespace Cession.Geometries
{
    public static class Constants
    {
        /// why pick 1e-7
        /// make sure cross and project point will return true on invoke isPointAlmostOnLine and isAlmostOrthogarol
        public const double Epsilon = 1e-4;
    }
}
