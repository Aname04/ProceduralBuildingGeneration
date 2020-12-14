using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
    
{
    public BuildingSettings settings;
    // Start is called before the first frame update
    void Start()
    {
        Building building = Generator.Generate(settings);
        GetComponent<BuildingRenderer>().Render(building);
        Debug.Log(building.ToString());
        
    }
}
