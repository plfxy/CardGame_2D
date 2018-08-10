#if CONFIG_SUPPORT_XML
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mono.Xml;
using System.Security;

public class XMLDataHolder:ASteinGameDataHolder
{
	const string fileNameFormat = "{0}.xml";
	protected SecurityElement root;
	protected SecurityElement _curNode;
	int nodePtr = 0;

	public XMLDataHolder ()
	{
	}

	public XMLDataHolder (string xml)
	{
//		SecurityParser xmlDoc = new SecurityParser ();
//		xmlDoc.LoadXml (xml);
//		root = xmlDoc.ToXml ();
	}
	public override void SetData (TextResource resource)
	{
//		SecurityParser xmlDoc = new SecurityParser ();
//		xmlDoc.LoadXml (resource.Text);
//		root = xmlDoc.ToXml ();
	}
	public override void Release ()
	{
		_curNode = null;
		root = null;
		nodePtr = 0;
	}

	public override bool MoveNext ()
	{

		if (root == null)
			return false;

		if (nodePtr >= root.Children.Count)
			return false;
		else {
			_curNode = root.Children [nodePtr] as SecurityElement;
			nodePtr++;
			return true;		
		}
	}

	public override byte ReadByte (string name)
	{
		return byte.Parse (_curNode.Attribute (name));
	}
	
	public override sbyte ReadSByte (string name)
	{
		return sbyte.Parse (_curNode.Attribute (name));
	}
	
	public override int ReadInt (string name)
	{
		try {
			return int.Parse (_curNode.Attribute (name));
		} catch (System.Exception e) {
			throw new System.Exception ("Read int [" + name + "] Failed " + e.ToString (), e);
			return 0;
		} 
	}
	
	public override long ReadLong (string name)
	{
		return long.Parse (_curNode.Attribute (name));
	}
	
	public override string ReadUTF8 (string name)
	{
		return _curNode.Attribute (name);
	}
	
	public override float ReadFloat (string name)
	{
		try {
			return float.Parse (_curNode.Attribute (name));
		} catch (System.Exception e) {
			throw new System.Exception ("Read float [" + name + "] Failed " + e.ToString (), e);
			return 0;
		} 
	}

	public override byte[] ReadBlob (string name)
	{
		throw new System.NotImplementedException ();
	}
}

#endif