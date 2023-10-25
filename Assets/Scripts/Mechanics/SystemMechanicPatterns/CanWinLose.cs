using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CanWinLose : MechanicPattern, IInitializableMechanic
{
    public Dictionary<string, float> WinConditions;
    public Dictionary<string, float> LoseConditions;
    public Dictionary<string, float> CurrentConditions;
    
    public void Initialize(params object[] args)
    {
        WinConditions = (Dictionary<string, float>)args[0];
        LoseConditions = (Dictionary<string, float>)args[1];
        CurrentConditions = (Dictionary<string, float>)args[2];
    }

    public void SetCondition(string condition, float value)
    {
        // TODO: add win/lose conditions for all the other patterns

        if (CurrentConditions != null)
        {
            CurrentConditions[condition] = value;

            if (value >= WinConditions[condition])
            {
                SceneManager.LoadScene("Win");
            }
            else if (value <= LoseConditions[condition])
            {
                SceneManager.LoadScene("Lose");
            }   
        }
    }
}