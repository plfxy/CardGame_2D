#if CONFIG_SUPPORT_XLS
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

public class XLSDataHolder:ASteinGameDataHolder
{
    HSSFWorkbook wk;
    ISheet mCurSheet;
    IRow mCurRow;
    int mRowPtr;
    List<string> columnNameList = new List<string> ();

    public XLSDataHolder ()
    {
    }

    public XLSDataHolder (FileStream xlsStream)
    {
        wk = new HSSFWorkbook (xlsStream);   //把xls文件中的数据写入wk中               
    }

    public void ReadSheet (string name)
    {
        ReleaseSheet ();
        if (!WorkbookReady)
            return;
        mCurSheet = wk.GetSheet (name);
        SetupSheet (mCurSheet);
    }

    public void ReadSheet (int index)
    {
        ReleaseSheet ();

        if (!WorkbookReady)
            return;
        mCurSheet = wk.GetSheetAt (index);
        SetupSheet (mCurSheet);
    }

    private void ReleaseSheet ()
    {
        Debug.LogWarning ("Release Sheet");
        if (mCurSheet != null)
        {
            mCurSheet = null;
        }
        if (columnNameList.Count > 0)
            columnNameList.Clear ();
    }

    private void SetupSheet (ISheet sheet)
    {
        //HACK row0 is param name row
        //HACK row 1 is param display name row, ignore
        IRow row0 = mCurSheet.GetRow (0);
        for (int i = 0; i < row0.LastCellNum; ++i)
        {
            ICell cell = row0.GetCell (i);
            if (cell != null)
            {
                columnNameList.Add (cell.StringCellValue);
            }
        }
        mRowPtr = 2;// valid row start from 2
    }

    private int NameToIndex (string name)
    {
        int result = columnNameList.IndexOf (name);
        if (result < 0)
            Debug.LogError ("No collum name is [" + name + "]");
        return result; 
    }

    private bool WorkbookReady
    {
        get
        {
            if (wk == null)
            {
                Debug.LogWarning ("Workbook is not initialized!!");
                return false;
            }
            return true;
        }
    }

    private bool SheetReady
    {
        get
        {

            if (mCurSheet == null)
            {
                Debug.LogWarning ("Sheet is not initialized!!");
                return false;
            }
            return true;
        }
    }

#region implemented abstract members of ASteinGameDataHolder


    public override void SetData (ResourceBase resource)
    {
        using (Stream fs = new MemoryStream ((resource as TextResource).Bytes))
        {
            wk = new HSSFWorkbook (fs); 
            ReadSheet (0);
        }
    }

    public override IClientData ReadDataObject ()
    {
        throw new System.NotImplementedException ();
    }



//  public override void SetData (TextResource resource)
//  {
//      using (Stream fs = new MemoryStream (resource.Bytes))
//      {
//          wk = new HSSFWorkbook (fs); 
//          ReadSheet (0);
//      }
//  }

    public override string DataName {
        get;
        set;
    }

    //  public override void SetData (ClientDataResource resource)
    //  {
    //      wk = new HSSFWorkbook (resource.RawFileStream);
    //      ReadSheet (0);
    //  }

    public override void Release ()
    {
        ReleaseSheet ();
        if (wk != null)
        {
            wk = null;
        }
    }

    public override bool MoveNext ()
    {
        if (!WorkbookReady)
            return false;
        if (!SheetReady)
            return false;

        if (mRowPtr > mCurSheet.LastRowNum)
            return false;
        else
        {
            do
            {
                mCurRow = mCurSheet.GetRow (mRowPtr);
                mRowPtr++;
                if (mCurRow == null)
                {
                    Debug.LogWarning ("Row [" + mRowPtr + "] is NULL");
                }
            } while(mCurRow == null);



            return true;        
        }
    }

    public override byte ReadByte (string name)
    {
        int column = NameToIndex (name);
        if (column >= 0)
        {
            ICell cell = mCurRow.GetCell (column);
            if (cell == null)
                return 0;
            else
            {
                return (byte)mCurRow.GetCell (column).NumericCellValue;
            }
        }
        else
            return 0;
    }

    public override sbyte ReadSByte (string name)
    {
        int column = NameToIndex (name);
        if (column >= 0)
            return (sbyte)mCurRow.GetCell (column).NumericCellValue;
        else
            return 0;
    }

    public override int ReadInt (string name)
    {
        int column = NameToIndex (name);
        if (column >= 0)
            return (int)mCurRow.GetCell (column).NumericCellValue;
        else
            return 0;
    }

    public override long ReadLong (string name)
    {
        int column = NameToIndex (name);
        if (column >= 0)
            return (long)mCurRow.GetCell (column).NumericCellValue;
        else
            return 0;
    }

    public override string ReadUTF8 (string name)
    {
        int column = NameToIndex (name);
        if (column >= 0)
        {
            ICell cell = mCurRow.GetCell (column);
            if (cell == null)
                return string.Empty;
            else
            {
                switch (cell.CellType)
                {
                    case CellType.STRING:
                        return cell.StringCellValue;
                    case CellType.NUMERIC:
                        return cell.NumericCellValue.ToString ();
                    default:
                        return cell.ToString ();
                }
            }
        }
        else
            return string.Empty;
    }

    public override float ReadFloat (string name)
    {
        int column = NameToIndex (name);
        if (column >= 0)
        {
            ICell cell = mCurRow.GetCell (column);
            if (cell == null)
                return 0;
            else
                return (float)mCurRow.GetCell (column).NumericCellValue;
        }
        else
            return 0;
    }

    public override byte[] ReadBlob (string name)
    {
        throw new System.NotImplementedException ();
    }

#endregion
}
#endif