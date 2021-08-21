namespace NWaves.DemoStereo.ViewModels
{
    public class BinauralPlotPoint
    {
        /// <summary>
        /// X coord
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Y coord
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Point type:
        ///     0 - "hrir" (grid) point
        ///     1 - "direction" point (can be moved by user)
        ///     2 - "head" point
        /// </summary>
        public int Type { get; set; }
    }
}
