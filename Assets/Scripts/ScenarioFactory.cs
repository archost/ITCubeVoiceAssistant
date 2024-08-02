using System.Collections.Generic;

public class ScenarioFactory
{
    private Dictionary<string, Scenario> _scenarios = new();
    public ScenarioFactory()
    {
        _scenarios.Add("MainScenario", new MainScenario());
        _scenarios.Add("SwitchScenario", new SwitchScenario());
        _scenarios.Add("MoveScenario", new MoveScenario());
    }

    public Scenario GetScenario(string scenarioName)
    {
        if (_scenarios.ContainsKey(scenarioName))
        {
            return _scenarios[scenarioName];
        }
        throw new KeyNotFoundException();
    }
}