using UnityEngine;
using System.Collections;

public class ClientDataRegistEntry
{
	public string dataName;
	public string dataResPath;
	public ASteinDataForm form;
	public ClientConfigLoadingPriority loadingPriority;
	public ClientDataType dataType;
	/// <summary>
	/// 是否需要加载数据表资源
	/// 比如Sqlite的就不需要
	/// </summary>
	public bool needResLoad;

	public ClientDataRegistEntry (string dataName, ASteinDataForm form, ClientDataType dataType, ClientConfigLoadingPriority loadingPriority, bool needResLoad, string resPath)
	{
		this.dataName = dataName;
		this.form = form;
		this.dataType = dataType;
		this.loadingPriority = loadingPriority;
		this.needResLoad = needResLoad;
		this.dataResPath = resPath;
	}
}

public enum ClientConfigLoadingPriority
{
	OnGameInit,
	PostGameInit,
}

public enum ClientDataType
{
	XML,
	CSV,
	XLS,
	JSON,
	SQLITE,
	ScriptableObject,
}

public class ClientDataCommon
{

	public static string ConfigType2Ext (ClientDataType configType)
	{
		switch (configType)
		{
			case ClientDataType.XML:
				return ".xml";
			case ClientDataType.JSON:
				return ".json";
			case ClientDataType.CSV:
				return ".csv";
			case ClientDataType.XLS:
				return ".xls";
			case ClientDataType.ScriptableObject:
				return ".assets";
			default:
				Debug.LogWarning ("Unhandled config type [" + configType + "]");
				return string.Empty;
		}
	}
}
