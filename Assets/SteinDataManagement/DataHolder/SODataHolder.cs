using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class SteinSODataform:ScriptableObject
{
	public abstract int Count {
		get;
	}

	public abstract IClientData GetData (int index);
}

public class SODataHolder:ASteinGameDataHolder
{
	SteinSODataform so;
	int curDataIndex = 0;
	IClientData curData;

	#region implemented abstract members of ASteinGameDataHolder

	public override void SetData (ResourceBase resource)
	{
		ScriptableObjectResource soRes = resource as ScriptableObjectResource;
		if (soRes == null)
		{
			Debug.LogError ("Invalid resource [" + resource.GetType () + "]");
			return;
		}
		so = soRes.Asset as SteinSODataform;

	}

	public override void Release ()
	{
		if (so != null)
		{
			so = null;
		}
	}

	public override bool MoveNext ()
	{
		if (so == null)
			return false;
		if (curDataIndex >= so.Count)
			return false;
		else
		{
			curData = so.GetData (curDataIndex);
			curDataIndex++;
			return true;
		}
	}

	public override IClientData ReadDataObject ()
	{
		return curData;
	}

	public override byte ReadByte (string name)
	{
		throw new System.NotImplementedException ();
	}

	public override sbyte ReadSByte (string name)
	{
		throw new System.NotImplementedException ();
	}

	public override int ReadInt (string name)
	{
		throw new System.NotImplementedException ();
	}

	public override long ReadLong (string name)
	{
		throw new System.NotImplementedException ();
	}

	public override string ReadUTF8 (string name)
	{
		throw new System.NotImplementedException ();
	}

	public override float ReadFloat (string name)
	{
		throw new System.NotImplementedException ();
	}

	public override byte[] ReadBlob (string name)
	{
		throw new System.NotImplementedException ();
	}

	public override string DataName {
		get;
		set;
	}

	#endregion
}
