using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CanWinLose : SystemMechanicPattern
{
    public override void Initialize(params object[] args)
    {
        
    }
    
    public void End(bool win)
    {
        SceneManager.LoadScene(win ? "Win" : "Lose");
    }
}