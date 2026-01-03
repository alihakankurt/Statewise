namespace TrafficLight;

sealed class RedState() : LightState(Light.Red, 8.0d)
{
    protected override Light GetNextLight() => Light.Yellow;
}
