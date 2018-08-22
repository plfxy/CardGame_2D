#if CONFIG_SUPPORT_CSV
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// CSV data holder.
/// line 0:keys
/// line 1:reserve
/// line 2:reserve
/// line 3:data start
/// </summary>
public class CSVDataHolder:ASteinGameDataHolder
{
	int curLineIndex = 0;
	List<string> dataKeys;
	string[][] splitedDataLines;
	#region implemented abstract members of ASteinGameDataHolder
	public override void SetData (ResourceBase resource)
	{
		TextResource textRes = resource as TextResource;
		string dataString = textRes.Text;
		dataString = dataString.Replace("\r\n","\n");
		string[] dataLines = dataString.Split (new string[]{"\n"},System.StringSplitOptions.RemoveEmptyEntries);
		if(dataLines.Length<3)
		{
			Debug.LogError("Crrupted csv file["+textRes.ResourcePath+"]\n["+dataString+"]");
			Debug.LogError("line 0:keys \nline 1:reserve \nline 2:reserve \nline 3:data start");
		}

		dataKeys = new List<string> (dataLines [0].Split(','));
		curLineIndex = 2;//HACK data start from 3;

		splitedDataLines = new string[dataLines.Length][];
		for (int i = 0; i < dataLines.Length; ++i) {
			splitedDataLines [i] = dataLines [i].Split (',');
		}
	}
	public override void Release ()
	{
		if (splitedDataLines != null) {
			splitedDataLines = null;
		}
		if (dataKeys != null) {
			dataKeys.Clear ();
			dataKeys = null;
		}
	}
	public override bool MoveNext ()
	{
		curLineIndex++;

		if (curLineIndex == splitedDataLines.Length)
			return false;
		else
			return true;
	}
	public override byte ReadByte (string name)
	{
		return byte.Parse (splitedDataLines [curLineIndex] [GetDataIndex(name)]);
	}
	public override sbyte ReadSByte (string name)
	{
		return sbyte.Parse (splitedDataLines [curLineIndex] [GetDataIndex(name)]);
	}
	public override int ReadInt (string name)
	{
		return int.Parse (splitedDataLines [curLineIndex] [GetDataIndex(name)]);
	}
	public override long ReadLong (string name)
	{
		return long.Parse (splitedDataLines [curLineIndex] [GetDataIndex(name)]);
	}
	public override string ReadUTF8 (string name)
	{
		try
		{
			return splitedDataLines [curLineIndex] [GetDataIndex(name)];
		}
		catch(System.Exception e)
		{
			Debug.LogError( this+"curLineIndex["+curLineIndex+"]name["+name+"]");
			//不能在这里吃掉这个异常
			throw e;
		}
	}
	public override float ReadFloat (string name)
	{
		return float.Parse (splitedDataLines [curLineIndex] [GetDataIndex(name)]);
	}
	public override byte[] ReadBlob (string name)
	{
		throw new System.NotImplementedException ();
	}
	public override IClientData ReadDataObject ()
	{
		throw new System.NotImplementedException ();
	}
	public override string DataName {
		get;
		set;
	}
	#endregion

	int GetDataIndex(string name)
	{
		int dataIndex = dataKeys.IndexOf(name);
		if (dataIndex == -1) {
			throw new System.ArgumentOutOfRangeException ("name","No property with name of["+name+"]");
		}

		return dataIndex;
	}
}

#endif