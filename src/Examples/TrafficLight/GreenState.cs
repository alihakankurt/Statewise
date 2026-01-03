namespace TrafficLight;

sealed class GreenState() : LightState(Light.Green, 6.0d)
{
    protected override Light GetNextLight() => Light.Yellow;
}
