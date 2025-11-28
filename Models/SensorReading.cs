public class SensorReading
{
    public string id { get; set; }
    public string location { get; set; }
    public DateTime window_end { get; set; }
    public double avg_ice_cm { get; set; }
    public double min_ice_cm { get; set; }
    public double max_ice_cm { get; set; }
    public double avg_surface_temp { get; set; }
    public double min_surface_temp { get; set; }
    public double max_surface_temp { get; set; }
    public double max_snow_cm { get; set; }
    public double avg_external_temp { get; set; }
    public int reading_count { get; set; }
    public string safety_status { get; set; }
}
