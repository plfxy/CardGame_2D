using UnityEngine;
using System.Collections;
using UnityEditor;

public class ClientDataSupportEditor : MonoBehaviour {

	public const string CONFIG_SUPPORT_XML = "CONFIG_SUPPORT_XML";
	public const string CONFIG_SUPPORT_SQL = "CONFIG_SUPPORT_SQL";
	public const string CONFIG_SUPPORT_JSON = "CONFIG_SUPPORT_JSON";
	public const string CONFIG_SUPPORT_CSV = "CONFIG_SUPPORT_CSV";
	public const string CONFIG_SUPPORT_XLS= "CONFIG_SUPPORT_XLS";

	public const string CLIENT_CONFIG_MENU_ROOT = EditorCommon.EditroMenuHead+"/SteinConfig/ClientData/";
	public const string CLIENT_CONFIG_MENU_SUPPORT_XML =CLIENT_CONFIG_MENU_ROOT +"SupportXML";
	public const string CLIENT_CONFIG_MENU_SUPPORT_SQL=CLIENT_CONFIG_MENU_ROOT +"SupportSQL";
	public const string CLIENT_CONFIG_MENU_SUPPORT_JSON =CLIENT_CONFIG_MENU_ROOT +"SupportJSON";
	public const string CLIENT_CONFIG_MENU_SUPPORT_CSV =CLIENT_CONFIG_MENU_ROOT +"SupportCSV";
	public const string CLIENT_CONFIG_MENU_SUPPORT_XLS =CLIENT_CONFIG_MENU_ROOT +"SupportXLS";

	[MenuItem(CLIENT_CONFIG_MENU_SUPPORT_XML,true)]
	static public bool ValidateSupportXML()
	{
		Menu.SetChecked (CLIENT_CONFIG_MENU_SUPPORT_XML,SteinEditorUtils.HaveSymbol (CONFIG_SUPPORT_XML));
		return true;
	}

	[MenuItem(CLIENT_CONFIG_MENU_SUPPORT_XML)]
	static public void ToggleSupportXML()
	{
		if (Menu.GetChecked (CLIENT_CONFIG_MENU_SUPPORT_XML))
		{	
			//remove check
			SteinEditorUtils.RemoveDefineSymbol ( CONFIG_SUPPORT_XML);
			Menu.SetChecked (CLIENT_CONFIG_MENU_SUPPORT_XML,false);
		} else
		{
			//add check
			SteinEditorUtils.AddDefineSymbol(CONFIG_SUPPORT_XML);
			Menu.SetChecked (CLIENT_CONFIG_MENU_SUPPORT_XML,true);
		}
	}

	[MenuItem(CLIENT_CONFIG_MENU_SUPPORT_SQL,true)]
	static public bool ValidateSupportSQL()
	{
		Menu.SetChecked (CLIENT_CONFIG_MENU_SUPPORT_SQL,SteinEditorUtils.HaveSymbol (CONFIG_SUPPORT_SQL));
		return true;
	}

	[MenuItem(CLIENT_CONFIG_MENU_SUPPORT_SQL)]
	static public void ToggleSupportSQL()
	{
		if (Menu.GetChecked (CLIENT_CONFIG_MENU_SUPPORT_SQL))
		{	
			//remove check
			SteinEditorUtils.RemoveDefineSymbol ( CONFIG_SUPPORT_SQL);
			Menu.SetChecked (CLIENT_CONFIG_MENU_SUPPORT_SQL,false);
		} else
		{
			//add check
			SteinEditorUtils.AddDefineSymbol(CONFIG_SUPPORT_SQL);
			Menu.SetChecked (CLIENT_CONFIG_MENU_SUPPORT_SQL,true);
		}
	}
	[MenuItem(CLIENT_CONFIG_MENU_SUPPORT_JSON,true)]
	static public bool ValidateSupportJSON()
	{
		Menu.SetChecked (CLIENT_CONFIG_MENU_SUPPORT_JSON,SteinEditorUtils.HaveSymbol (CONFIG_SUPPORT_JSON));
		return true;
	}

	[MenuItem(CLIENT_CONFIG_MENU_SUPPORT_JSON)]
	static public void ToggleSupportJSON()
	{
		if (Menu.GetChecked (CLIENT_CONFIG_MENU_SUPPORT_JSON))
		{	
			//remove check
			SteinEditorUtils.RemoveDefineSymbol ( CONFIG_SUPPORT_JSON);
			Menu.SetChecked (CLIENT_CONFIG_MENU_SUPPORT_JSON,false);
		} else
		{
			//add check
			SteinEditorUtils.AddDefineSymbol(CONFIG_SUPPORT_JSON);
			Menu.SetChecked (CLIENT_CONFIG_MENU_SUPPORT_JSON,true);
		}
	}
	[MenuItem(CLIENT_CONFIG_MENU_SUPPORT_XLS,true)]
	static public bool ValidateSupportXLS()
	{
		Menu.SetChecked (CLIENT_CONFIG_MENU_SUPPORT_XLS,SteinEditorUtils.HaveSymbol (CONFIG_SUPPORT_XLS));
		return true;
	}

	[MenuItem(CLIENT_CONFIG_MENU_SUPPORT_XLS)]
	static public void ToggleSupportXLS()
	{
		if (Menu.GetChecked (CLIENT_CONFIG_MENU_SUPPORT_XLS))
		{	
			//remove check
			SteinEditorUtils.RemoveDefineSymbol ( CONFIG_SUPPORT_XLS);
			Menu.SetChecked (CLIENT_CONFIG_MENU_SUPPORT_XLS,false);
		} else
		{
			//add check
			SteinEditorUtils.AddDefineSymbol(CONFIG_SUPPORT_XLS);
			Menu.SetChecked (CLIENT_CONFIG_MENU_SUPPORT_XLS,true);
		}
	}
	[MenuItem(CLIENT_CONFIG_MENU_SUPPORT_CSV,true)]
	static public bool ValidateSupportCSV()
	{
		Menu.SetChecked (CLIENT_CONFIG_MENU_SUPPORT_CSV,SteinEditorUtils.HaveSymbol (CONFIG_SUPPORT_CSV));
		return true;
	}

	[MenuItem(CLIENT_CONFIG_MENU_SUPPORT_CSV)]
	static public void ToggleSupportCSV()
	{
		if (Menu.GetChecked (CLIENT_CONFIG_MENU_SUPPORT_CSV))
		{	
			//remove check
			SteinEditorUtils.RemoveDefineSymbol ( CONFIG_SUPPORT_CSV);
			Menu.SetChecked (CLIENT_CONFIG_MENU_SUPPORT_CSV,false);
		} else
		{
			//add check
			SteinEditorUtils.AddDefineSymbol(CONFIG_SUPPORT_CSV);
			Menu.SetChecked (CLIENT_CONFIG_MENU_SUPPORT_CSV,true);
		}
	}
}
