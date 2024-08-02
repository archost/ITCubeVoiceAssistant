using UnityEngine.Events;

public abstract class Scenario
{
    public UnityAction<Animation> OnAnimate;

    public UnityAction<string, string> OnSwitchScenario;
    public Scenario NextScenario { get; set; }

    public UnityAction<string> OnSay;
    abstract public void InputProcessing(string inputPhrase);
}