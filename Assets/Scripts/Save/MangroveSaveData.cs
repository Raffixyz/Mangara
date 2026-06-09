using System.Collections.Generic;
using Mangrove;

namespace Save
{
    [System.Serializable]
    public class MangroveSaveData
    {
        public string PlantSiteID;
        public string MangroveId;
        public int CurrentStage;
        public int DaysInCurrentStage;
        public PlantState PlantState;
    }
    
}