namespace TrafficLight;

sealed class YellowState() : LightState(Light.Yellow, 1.0d)
{
    protected override Light GetNextLight()
        => (Owner.Previous == Light.Red) ? Light.Green : Light.Red;
}
