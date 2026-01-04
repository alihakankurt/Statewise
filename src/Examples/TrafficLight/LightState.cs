using Statewise;

namespace TrafficLight;

abstract class LightState : State<Light>
{
    private double _timer;

    protected double InitialTime { get; }

    public override Light Key { get; }

    public LightState(Light key, double time)
    {
        Key = key;
        InitialTime = time;
    }

    protected override void Enter()
    {
        _timer = InitialTime;
    }

    protected override void Update(double deltaTime)
    {
        _timer -= deltaTime;
        if (_timer <= 0.0d)
        {
            TransitionTo(GetNextLight());
        }
    }

    protected abstract Light GetNextLight();
}
