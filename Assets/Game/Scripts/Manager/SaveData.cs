using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int Resource;

    public List<FacilityState> Facilities =
        new List<FacilityState>();

    public long LastQuitTime;
}