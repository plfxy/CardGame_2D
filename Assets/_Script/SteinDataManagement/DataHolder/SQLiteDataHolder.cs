#if CONFIG_SUPPORT_SQL
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;

public class SQLiteDataHolder :ASteinGameDataHolder
{
	static readonly string SQLITE_CONNECTION_STRING = @"Data Source=" + Application.streamingAssetsPath + "/ClientData/dashenquan_p0_design.db";


	protected  SqliteDataReader curNode;
	public string tableName;
	//	SqliteDataReader list = null;
	public override string DataName
	{
		get
		{
			return tableName;
		}
		set
		{
			tableName = value;
		}
	}

	/// <summary>
	/// SqliteDataHolder has no resources
	/// </summary>
	/// <param name="resource">Resource.</param>
	public override void SetData (TextResource resource)
	{
		if (SteinSqliteHelper.GetCurSqliteConnectionString () != SQLITE_CONNECTION_STRING)
		{
			SteinSqliteHelper.CloseDb ();
			SteinSqliteHelper.OpenDb (SQLITE_CONNECTION_STRING);
		}
	}

	public override void Release ()
	{
//		list = null;
		curNode = null;
		//DO NOT close db here,other dataform may need it open
//				DbQueryHelper.CloseDB ();
	}

	public override bool MoveNext ()
	{
		if (curNode == null)
		{
			SqliteCmdSelect selectAll = SteinSqliteHelper.CreateSelectCommand ();
			selectAll.SetTableName (tableName);
			curNode = SteinSqliteHelper.ExecuteQuery (selectAll);
		}
//		return curNode.NextResult ();
		return curNode.Read ();
	}

	public override byte ReadByte (string name)
	{
		return curNode.GetByte (curNode.GetOrdinal (name));
	}

	public override sbyte ReadSByte (string name)
	{
		return (sbyte)curNode.GetInt16 (curNode.GetOrdinal (name));
	}

	public override int ReadInt (string name)
	{
		try
		{
			return curNode.GetInt32 (curNode.GetOrdinal (name));
		}
		catch (System.Exception e)
		{
			Debug.LogError ("Read Int [" + name + "] " + e.ToString ());
			return 0;
		} 
		
	}

	public override long ReadLong (string name)
	{
		try
		{
			return  curNode.GetInt64 (curNode.GetOrdinal (name));
		}
		catch (System.Exception e)
		{
			Debug.LogError ("Read long [" + name + "] " + e.ToString ());
			return 0;
		} 
	}

	public override string ReadUTF8 (string name)
	{
//		return curNode.GetString (curNode.GetOrdinal (name));
		try
		{
			return curNode.GetString (curNode.GetOrdinal (name));
		}
		catch (System.Exception e)
		{
			Debug.LogError ("ReadUTF8 [" + name + "] failed..." + e.ToString ());
			return null;
		}
	}

	public override float ReadFloat (string name)
	{
		return curNode.GetFloat (curNode.GetOrdinal (name));
	}

	public override byte[] ReadBlob (string name)
	{
		byte[] blobResult = new byte[curNode.GetBytes (curNode.GetOrdinal (name), 0, null, 0, int.MaxValue)];
		curNode.GetBytes (curNode.GetOrdinal (name), 0, blobResult, 0, blobResult.Length);
		return blobResult;
	}
}
#endif