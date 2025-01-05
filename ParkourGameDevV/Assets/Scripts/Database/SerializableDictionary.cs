using System.Collections.Generic;

public class SerializableDictionary
{
    public List<string> Keys = new List<string>();
    public List<float> Values = new List<float>();

    public SerializableDictionary(Dictionary<string, float> dictionary)
    {
        foreach (KeyValuePair<string, float> kvp in dictionary)
        {
            Keys.Add(kvp.Key);
            Values.Add(kvp.Value);
        }
    }

    public Dictionary<string, float> ToDictionary()
    {
        Dictionary<string, float> dictionary = new Dictionary<string, float>();
        for (int i = 0; i < Keys.Count; i++)
        {
            dictionary.Add(Keys[i], Values[i]);
        }
        return dictionary;
    }
}