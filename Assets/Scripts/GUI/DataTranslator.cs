using System.Collections;
using System;
using UnityEngine;

public class DataTranslator : MonoBehaviour {

    private static string COUNTS_SYMBOL = "[COUNTS]";
    private static string POINTS_SYMBOL = "[POINTS]";
    public static int DataToCounts(string data)
    {
        return int.Parse(DataToValue(data, COUNTS_SYMBOL));
    }

    public static int DataToPoints(string data)
    {
        return int.Parse(DataToValue(data, POINTS_SYMBOL));
    }

    private static string DataToValue(string data, string symbol)
    {
        string[] pieces = data.Split('/');
        foreach (string piece in pieces)
        {
            if (piece.StartsWith(symbol))
            {
               return piece.Substring(symbol.Length);
            }
        }
        Debug.LogError(symbol + " not found in " + data);
        return "";
    }
}
