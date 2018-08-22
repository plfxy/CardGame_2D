using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void SteinDataFormEventDele (ASteinDataForm dataform);


public abstract class ASteinDataForm
{
	protected ClientDataRegistEntry mConfigEntry;

	protected ASteinGameDataHolder mInternalDataForm;
	protected ResourceBase mDataRes;
	protected string mName;
	protected bool mReady;

	#region internal

	protected abstract IClientData InternalProcessData ();

	protected byte ReadByte (string name)
	{
		return mInternalDataForm.ReadByte (name);
	}

	protected sbyte ReadSByte (string name)
	{
		return mInternalDataForm.ReadSByte (name);
	}

	protected int ReadInt (string name)
	{
		return mInternalDataForm.ReadInt (name);
	}

	protected long ReadLong (string name)
	{
		return mInternalDataForm.ReadLong (name);
	}

	/// <summary>
	///读取毫秒（ms）时间
	/// </summary>
	/// <returns>The MS time.</returns>
	/// <param name="name">Name.</param>
	protected float ReadMSTime (string name)
	{
		return mInternalDataForm.ReadMSTime (name);
	}

	protected float ReadPercentValue (string name)
	{
		return mInternalDataForm.ReadPercentValue (name);
	}

	protected string ReadUTF8 (string name)
	{
		return mInternalDataForm.ReadUTF8 (name);
	}

	protected float ReadFloat (string name)
	{
		return mInternalDataForm.ReadFloat (name);
	}

	protected bool ReadBool (string name)
	{
		return mInternalDataForm.ReadBool (name);
	}

	protected byte[] ReadBlob (string name)
	{
		return mInternalDataForm.ReadBlob (name);
	}

	protected int[] ReadIdList (string name, char splitChar = ',')
	{
		return mInternalDataForm.ReadIdList (name, splitChar);
	}
	protected string[] ReadStringList (string name, char splitChar = ',')
	{
		return mInternalDataForm.ReadStringList (name, splitChar);
	}

	#endregion

	#region public


	public event SteinDataFormEventDele OnDataFromReady;

	public string name {
		get {
			return mName;
		}
	}

	public bool Ready {
		get {
			return mReady;
		}
	}

	public ASteinGameDataHolder DataForm {
		get {
			return mInternalDataForm;
		}
	}

	public virtual void Init (string dataName, ClientDataRegistEntry configEntry)
	{
		mConfigEntry = configEntry;
		mName = dataName;
		mReady = false;
		if (!configEntry.needResLoad)
		{
			//using sqlite ,all data is saved in a single db file,so no resource load for every dataform
			HandleDataResReady (true, dataName);
		}
		else
		{
			mDataRes = ResourceManager.Instance.GetResource (configEntry.dataResPath + ClientDataCommon.ConfigType2Ext (configEntry.dataType));
			mDataRes.ApplyResource (HandleDataResReady);
		}
	}

	protected abstract void SaveData (IClientData data);

	protected void HandleDataResReady (bool success, string name)
	{
		mInternalDataForm = SteinClientDataManager.GetDataHolder (mConfigEntry.dataType);
		if (mInternalDataForm == null)
		{
			Debug.LogError ("GetDataHolder[" + mConfigEntry.dataType + "] FAILED!!");
			return;
		}

		mInternalDataForm.DataName = mName;
		mInternalDataForm.SetData (mDataRes);

		while (mInternalDataForm.MoveNext ())
		{
			IClientData data = null;
			try
			{
				data = InternalProcessData ();
			}
			catch (System.Exception e)
			{
				Debug.LogError ("Dataform["+mName+"] read failed");
				Debug.LogError (e.ToString ());
				break;
			}

			if (data == null)
			{
				Debug.LogWarning ("Read a null entry!!");
			}
			else
				SaveData (data);
			
		}

		UnloadSourceData ();

		mReady = true;

		if (OnDataFromReady != null)
		{
			OnDataFromReady (this);
		}
		
		if (mDataRes != null)
		{
			mDataRes.Release ();
			mDataRes = null;
		}
	}

	public virtual void Release ()
	{
		UnloadSourceData ();
		mReady = false;
	}

	public virtual void UnloadSourceData ()
	{
		if (mInternalDataForm != null)
		{
			mInternalDataForm.Release ();
			mInternalDataForm = null;
		}

		if (mDataRes != null)
		{
			ResourceManager.Instance.ReleaseResource (mDataRes);
			mDataRes = null;
		}
	}

	#endregion
}