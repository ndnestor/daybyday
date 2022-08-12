using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PersistentDataSaver : MonoBehaviour
{
	private Data data;

    public static PersistentDataSaver Instance;
    
    private void Awake()
    {
	    DontDestroyOnLoad(this);
	    Instance = this;
	    
	    Load();
    }

    public void Set(string key, string value)
    {
	    data.Set(key, value);
		Save();
    }

    public T Get<T>(string key)
    {
	    T value;
	    
	    if(typeof(T) == typeof(bool))
		    value = (T)(object)bool.Parse(data.Get(key));
	    else if(typeof(T) == typeof(string))
		    value = (T)(object)data.Get(key);
	    else if(typeof(T) == typeof(int))
		    value = (T)(object)int.Parse(data.Get(key));
	    else
		    value = (T)(object)null;
	    return value;
    }

    public bool Contains(string key)
    {
	    return data.Contains(key);
    }

    private void Save()
    {
	    string json = JsonUtility.ToJson(data);
	    
	    File.WriteAllText("/tmp/data.json", json);
    }

    private void Load()
    {
	    if (File.Exists("/tmp/data.json"))
	    {
		    string json = File.ReadAllText("/tmp/data.json");
		    data = Data.FromJson(json);
		    return;
	    }
	    
		File.WriteAllText("/tmp/data.json", "{}");
		data = new Data();
    }
}

[Serializable]
public class Data
{
	public List<string> keys = new List<string>();
	public List<string> values = new List<string>();
	
	public static Data FromJson(string json)
	{
		return JsonUtility.FromJson<Data>(json);
	}

	public void Set(string key, string value)
	{
		if(keys.Contains(key))
			values[keys.IndexOf(key)] = value;
		else
		{
			keys.Add(key);
			values.Add(value);
		}
	}

	public string Get(string key)
	{
		int indexOfKey = keys.IndexOf(key);
		if(indexOfKey != -1)
			return values[keys.IndexOf(key)];
		return null;
	}

	public bool Contains(string key)
	{
		return keys.Contains(key);
	}

	public override string ToString()
	{
		string output = "{";

		for(int i = 0; i < keys.Count; i++)
		{
			output += $"\"{keys[i]}\": \"{values[i]}\"";
			if(i != keys.Count - 1)
				output += ", ";
		}

		output += "}";
		
		return output;
	}
}
