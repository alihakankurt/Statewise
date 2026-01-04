using Statewise;
using TrafficLight;

var fsm = FiniteStateMachineBuilder<Light>
    .WithInitialState<RedState>()
    .WithState<YellowState>()
    .WithState<GreenState>()
    .Build();

fsm.EnterState();

const double MaxTime = 30.0d;
const double DeltaTime = 1.0d;
double time = 0.0d;

Console.WriteLine($"{time}: {fsm.Current}");
while (time < MaxTime)
{
    fsm.UpdateState(DeltaTime);
    time += DeltaTime;
    Console.WriteLine($"{time}: {fsm.Current}");
}

fsm.ExitState();
