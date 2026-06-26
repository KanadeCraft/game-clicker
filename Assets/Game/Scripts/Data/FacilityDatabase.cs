using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "FacilityDatabase",
    menuName = "Clicker/FacilityDatabase")]
public class FacilityDatabase : ScriptableObject
{
    public List<FacilityDefinition> Facilities =
        new();
}