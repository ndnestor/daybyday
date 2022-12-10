using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PersistentDataSaver : MonoBehaviour
{
	[SerializeField] private string saveFileName;
	[SerializeField] private bool resetOnStart;

	private string absoluteSavePath;
	private Data data;

    public static PersistentDataSaver Instance;
    
    private void Awake()
    {
	    DontDestroyOnLoad(this);
	    Instance = this;

	    absoluteSavePath = Application.persistentDataPath + "/" + saveFileName;

	    if(resetOnStart && File.Exists(absoluteSavePath))
		    File.Delete(absoluteSavePath);
	    
	    Load();
    }

    public void Set(string key, object value)
    {
	    data.Set(key, value.ToString());
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

    public T TryGet<T>(string key, T fallback)
    {
	    if (Contains(key))
		    return Get<T>(key);

	    Set(key, fallback);
	    return fallback;
    }

    public bool Contains(string key)
    {
	    return data.Contains(key);
    }

    private void Save()
    {
	    string json = JsonUtility.ToJson(data);
	    
	    File.WriteAllText(absoluteSavePath, json);
    }

    private void Load()
    {
	    if (File.Exists(absoluteSavePath))
	    {
		    string json = File.ReadAllText(absoluteSavePath);
		    data = Data.FromJson(json);
		    return;
	    }
	    
		File.WriteAllText(absoluteSavePath, "{}");
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
