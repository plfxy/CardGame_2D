using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;


public class SteinClientDataManager// : IHFUpdateListener
{
    #region singleton

    private static SteinClientDataManager _instance;

    private SteinClientDataManager()
    {
    }

    public static SteinClientDataManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new SteinClientDataManager();
        }
        return _instance;
    }

    #endregion

    #region dataholder factory


    static public ASteinGameDataHolder GetDataHolder(ClientDataType datatype)
    {
        switch (datatype)
        {
#if CONFIG_SUPPORT_XML
			case ClientDataType.XML:
			return new XMLDataHolder ();
#endif
#if CONFIG_SUPPORT_JSON
            case ClientDataType.JSON:
                return new JsonDataHolder();
#endif
#if CONFIG_SUPPORT_SQL
			case ClientDataType.SQLITE:
				return new SQLiteDataHolder ();
#endif
#if CONFIG_SUPPORT_XLS
			case ClientDataType.XLS:
				return new XLSDataHolder ();
#endif
#if CONFIG_SUPPORT_CSV
			case ClientDataType.CSV:
				return new CSVDataHolder ();
#endif
            case ClientDataType.ScriptableObject:
                return new SODataHolder();
            default:
                Debug.LogError("Trying to get dataholder for UNSUPPORT data type[" + datatype + "]");
                return null;
        }
    }


    #endregion

    /// <summary>
    /// Client config request group.
    /// 分批次加载的批次
    /// </summary>
    class ClientConfigRequestGroup
    {
        public event SteinSimpleDele OnGroupInited;

        int mSecondpassDataCount;
        bool mDone;
        SteinClientDataManager mConfigManager;

        List<ClientDataRegistEntry> mRequestQueue = new List<ClientDataRegistEntry>();

        public bool Done
        {
            get
            {
                return mDone;
            }
        }

        public ClientConfigRequestGroup(SteinClientDataManager configManager)
        {
            mConfigManager = configManager;
        }

        public void Add(ClientDataRegistEntry entry)
        {
            mRequestQueue.Add(entry);
        }

        public void BeginLoad()
        {
            if (mRequestQueue.Count > 0)
            {
                mSecondpassDataCount = mRequestQueue.Count;

                foreach (var entry in mRequestQueue)
                {
                    entry.form.Init(entry.dataName, entry);
                    if (entry.form.Ready)
                    {
                        form_OnDataFromReady(entry.form);
                    }
                    else
                    {
                        entry.form.OnDataFromReady += form_OnDataFromReady;
                    }
                }
            }
            else
            {
                mDone = true;
                if (OnGroupInited != null)
                    OnGroupInited();
            }
        }

        public IEnumerator Load()
        {
            BeginLoad();

            while (!mDone)
            {
                yield return null;
            }
        }

        void form_OnDataFromReady(ASteinDataForm dataform)
        {
            mSecondpassDataCount--;
            if (mSecondpassDataCount == 0)
            {
                mDone = true;
                if (OnGroupInited != null)
                    OnGroupInited();
            }
        }
    }

    bool _inited = false;

    IClientDataRegister mRegister;
    ClientConfigRequestGroup mInitOnGameInitGroup;
    ClientConfigRequestGroup mInitPostGameInitGroup;

    Dictionary<System.Type, ASteinDataForm> _activeDataForm = new Dictionary<System.Type, ASteinDataForm>();

    #region private




    void mInitPostGameInitGroup_OnGroupInited()
    {
        if (OnFirstPassDataReady != null)
        {
            OnFirstPassDataReady();
        }
        mInitPostGameInitGroup.BeginLoad();
    }

    void mInitOnGameInitGroup_OnGroupInited()
    {
        if (OnClientDataInitComplete != null)
        {
            OnClientDataInitComplete();
        }
    }


    #endregion

    #region public

    public event SteinSimpleDele OnFirstPassDataReady;
    public event SteinSimpleDele OnClientDataInitComplete;


    public void RegistClientConfig(string configName, ASteinDataForm dataform, ClientDataType dataType, ClientConfigLoadingPriority loadingPriority, bool needResLoad, string resPath)
    {
        ClientDataRegistEntry entry = new ClientDataRegistEntry(configName, dataform, dataType, loadingPriority, needResLoad, resPath);
        switch (loadingPriority)
        {
            case ClientConfigLoadingPriority.OnGameInit:
                mInitOnGameInitGroup.Add(entry);
                break;
            case ClientConfigLoadingPriority.PostGameInit:
                mInitPostGameInitGroup.Add(entry);
                break;
            default:
                Debug.LogError("Unhandled loadingPriority [" + loadingPriority + "]");
                break;
        }


        _activeDataForm.Add(dataform.GetType(), dataform);
    }



    public void Init(IClientDataRegister register)
    {
        if (_inited)
            return;
        _inited = true;

        mInitOnGameInitGroup = new ClientConfigRequestGroup(this);
        mInitOnGameInitGroup.OnGroupInited += mInitOnGameInitGroup_OnGroupInited;
        mInitPostGameInitGroup = new ClientConfigRequestGroup(this);
        mInitPostGameInitGroup.OnGroupInited += mInitPostGameInitGroup_OnGroupInited;
        mRegister = register;
        mRegister.RegistConfigs(this);

        mInitOnGameInitGroup.BeginLoad();
    }

    public IEnumerator InitCoro(IClientDataRegister register)
    {
        Init(register);
        while (!mInitOnGameInitGroup.Done)
        {
            yield return null;
        }
    }

    // public void FrameUpdate(float deltaTime)
    // {
    //     //throw new System.NotImplementedException();
    // }

    public void Reload()
    {
        Release();
        Init(mRegister);
    }

    public void Release()
    {
        _inited = false;
        foreach (var kvp in _activeDataForm)
        {
            kvp.Value.Release();
        }
        _activeDataForm.Clear();
    }

    public T GetDataform<T>() where T : ASteinDataForm
    {
        return _activeDataForm[typeof(T)] as T;
    }

    public ASteinDataForm GetDataForm(System.Type formType)
    {
        return _activeDataForm[formType];
    }

    #endregion

}
