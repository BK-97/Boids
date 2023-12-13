using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceCalculator : MonoBehaviour
{
    #region Params
    public List<ChanceData> chanceDataList;
    [SerializeField]
    private float totalChance;
    #endregion
    #region Calculator
    public bool initalizeChanceList = false;
    private void OnValidate()
    {
        if (!initalizeChanceList)
        {
            SetEnum();
            initalizeChanceList = true;
        }
        if (chanceDataList.Count > (int)BoidTypes.NUMBER_OF_TYPES)
        {
            chanceDataList.Clear();
            SetEnum();
        }
        else
            UpdateTotalChance();

    }

    private void SetEnum()
    {
        int enumCount = BoidTypes.GetValues(typeof(BoidTypes)).Length - 1;

        for (int i = 0; i < enumCount; i++)
        {
            BoidTypes fishType = (BoidTypes)i;
            ChanceData newChanceData = new ChanceData();
            newChanceData.name = fishType.ToString();
            newChanceData.chance = 0f;
            chanceDataList.Add(newChanceData);
        }
        chanceDataList[0].chance = 1;
        UpdateTotalChance();
    }

    private void UpdateTotalChance()
    {
        totalChance = 0f;
        foreach (var chanceData in chanceDataList)
        {
            totalChance += chanceData.chance;
        }

        if (totalChance > 0f)
        {
            foreach (var chanceData in chanceDataList)
            {
                chanceData.chance /= totalChance;
                chanceData.chance = GetMyFloatParameter(chanceData.chance, 2);
            }
            totalChance = 1f;
        }
    }
    private float GetMyFloatParameter(float param, int decimalPlaces)
    {
        float multiplier = Mathf.Pow(10f, decimalPlaces);
        return Mathf.Round(param * multiplier) / multiplier;
    }
    #endregion

    #region Helpers
    public float GetChance(int index)
    {
        return chanceDataList[index].chance;
    }
    #endregion

    [System.Serializable]
    public class ChanceData
    {
        public string name;
        [Range(0f, 1f)]
        public float chance;
    }
}