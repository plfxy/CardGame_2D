#if CONFIG_SUPPORT_JSON
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;

public class JsonDataHolder:ASteinGameDataHolder
{
	#region implemented abstract members of ASteinGameDataHolder

	public override string DataName {
		get;
		set;
	}

	#endregion

	JsonObject jo;
	JsonArray ja;
	string mArrayRootName;
	int nodePtr = 0;
	JsonObject curJo;

	/// <summary>
	/// Initializes a new instance of the <see cref="JsonDataHolder"/> class.
	/// set mArrayRootName "ConfigArray"
	/// </summary>
	public JsonDataHolder ()
	{
		mArrayRootName = "ConfigArray";
	}

	public JsonDataHolder (string rootName)
	{
		SetRootName (rootName);
	}

	public void SetRootName (string rootName)
	{
		mArrayRootName = rootName;
	}

	public override void SetData (ResourceBase resource)
	{
		TextResource textRes = resource as TextResource;
		
		object jsonObject = SimpleJson.SimpleJson.DeserializeObject (textRes.Text);
		if (jsonObject == null)
		{
			Debug.LogError ("SetData Failed [" + resource.ResourcePath + "]");
			return;
		}

		if (jsonObject is JsonArray)
		{
			jo = null;
			ja = jsonObject as JsonArray;
		}
		else
		{
			jo = jsonObject as JsonObject;
			ja = jo [mArrayRootName] as JsonArray;
		}
		nodePtr = 0;
	}

	public override void Release ()
	{
		if (jo != null)
		{

			jo.Clear ();
			jo = null;
		}
		if (ja != null)
		{
			ja.Clear ();
			ja = null;
		}
	}

	public override bool MoveNext ()
	{
//		if (jo == null)
//			return false;
		
		if (ja == null)
			return false;

		if (nodePtr >= ja.Count)
			return false;
		else
		{
			curJo = ja [nodePtr] as JsonObject;
			nodePtr++;
			return true;		
		}
	}
	public override IClientData ReadDataObject ()
	{
		throw new System.NotImplementedException ();
	}

	public override byte ReadByte (string name)
	{
		byte result = 0;
		if (curJo == null)
		{
			Debug.LogError ("ReadByte Json index[" + nodePtr + "] [" + name + "] Failed JsonObject is null");
			return result;
		}
		if (!byte.TryParse (curJo [name].ToString (), out result))
		{
			Debug.LogError ("ReadByte Json index[" + nodePtr + "] [" + name + "] Failed, source value [" + curJo [name] + "]");
		}

		return result;
	}

	public override sbyte ReadSByte (string name)
	{
		sbyte result = 0;
		if (curJo == null)
		{
			Debug.LogError ("ReadSByte Json index[" + nodePtr + "] [" + name + "] Failed JsonObject is null");
			return result;
		}
		if (!sbyte.TryParse (curJo [name].ToString (), out result))
		{
			Debug.LogError ("ReadSByte Json index[" + nodePtr + "] [" + name + "] Failed, source value [" + curJo [name] + "]");
		}
		
		return result;
	}

	public override int ReadInt (string name)
	{
		int result = 0;
		if (curJo == null)
		{
			Debug.LogError ("ReadInt Json index[" + nodePtr + "] [" + name + "] Failed JsonObject is null");
			return result;
		}
		if (!int.TryParse (curJo [name].ToString (), out result))
		{
			Debug.LogError ("ReadInt Json index[" + nodePtr + "] [" + name + "] Failed, source value [" + curJo [name] + "]");
		}
		
		return result;
	}

	public override long ReadLong (string name)
	{
		long result = 0;
		if (curJo == null)
		{
			Debug.LogError ("ReadLong Json index[" + nodePtr + "] [" + name + "] Failed JsonObject is null");
			return result;
		}
		if (!long.TryParse (curJo [name].ToString (), out result))
		{
			Debug.LogError ("ReadLong Json index[" + nodePtr + "] [" + name + "] Failed, source value [" + curJo [name] + "]");
		}
		
		return result;
	}

	public override string ReadUTF8 (string name)
	{
		if (curJo == null)
		{
			Debug.LogError ("ReadLong Json index[" + nodePtr + "] [" + name + "] Failed JsonObject is null");
			return string.Empty;
		}

		return curJo [name].ToString ();
	}

	public override float ReadFloat (string name)
	{
		float result = 0;
		if (curJo == null)
		{
			Debug.LogError ("ReadFloat Json index[" + nodePtr + "] [" + name + "] Failed JsonObject is null");
			return result;
		}
		if (!float.TryParse (curJo [name].ToString (), out result))
		{
			Debug.LogError ("ReadFloat Json index[" + nodePtr + "] [" + name + "] Failed, source value [" + curJo [name] + "]");
		}
		
		return result;
	}

	public override byte[] ReadBlob (string name)
	{
		if (curJo == null)
		{
			Debug.LogError ("ReadBlob Json index[" + nodePtr + "] [" + name + "] Failed JsonObject is null");
			return null;
		}

		return	System.Text.Encoding.UTF8.GetBytes (curJo [name].ToString ());
	}
}
#endif