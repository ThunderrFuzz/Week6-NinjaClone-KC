using UnityEngine;

public class Combo : MonoBehaviour
{
    

    public int combo = 0;
    public int comboMultiplier = 1; 
    public int comboThreshold = 5; //increase combo every X items 

    //combo count
    public void IncreaseCombo()
    {
        combo++;
        if (combo % comboThreshold == 0)
        {
            IncreaseComboMultiplier();
        }
    }

    
    public void ResetCombo()
    {
        combo = 0;
        comboMultiplier = 1;
    }

   //combo multi 
    private void IncreaseComboMultiplier()
    {
        comboMultiplier++;
    }

    
    public void ResetComboMultiplier()
    {
        comboMultiplier = 1;
    }
}
