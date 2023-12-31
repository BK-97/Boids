using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishModel : MonoBehaviour
{
    public List<GameObject> fishModels;
    private GameObject currentModel;

    public void SetModel(BoidTypes fishType)
    {
        currentModel = fishModels[(int)fishType];
        currentModel.SetActive(true);
    }
}
