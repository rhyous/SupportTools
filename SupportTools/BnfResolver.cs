using System;
using System.Collections;
using System.Diagnostics;
using LANDesk.ManagementSuite.Database;

namespace SupportTools
{
    public class BnfResolver
    {
        public string ResolveBnf(string inParameter, int inId)
        {
            int startLoc = 0, loc1, loc2;
            var bnflist = new ArrayList();

            try
            {
                if ((loc1 = inParameter.IndexOf('%')) == -1)
                {
                    return inParameter;
                }
                if ((loc2 = inParameter.IndexOf('%', loc1 + 1)) == -1)
                {
                    return inParameter;
                }
            }
            catch
            {
                return inParameter;
            }

            var temp = inParameter;
            var ret = inParameter;
            var i = 0;

            // Since we have already checked loc1 and loc2, we will always do this one,
            // so we will use 'do' iterator.
            do
            {
                bool isBnf;
                if (temp.Substring(loc1, (loc2 - loc1)).Contains("."))
                {
                    isBnf = true;
                    var bnf = temp.Substring(loc1 + 1, loc2 - loc1 - 1);
                    bnflist.Add(bnf);
                    ret = temp.Substring(0, loc1);
                    ret += string.Format("{{{0}}}", i);
                    ret += temp.Substring(loc2 + 1);
                    temp = ret;
                    startLoc = temp.IndexOf("{" + i++ + "}", StringComparison.Ordinal) + 3;

                }
                else
                {
                    isBnf = false;
                }
                try
                {
                    loc1 = isBnf ? temp.IndexOf('%', startLoc) : temp.IndexOf('%', (startLoc = (loc2 + 1)));
                    loc2 = temp.IndexOf('%', loc1 + 1);
                }
                catch
                {
                    break;
                }
            } while ((loc1 != -1) && (loc2 != -1));

            if (bnflist.Count > 0)
                ret = GetBnfValue(bnflist, ret, inId);
            return ret;
        }

        public string GetBnfValue(ArrayList inBnfList, string inCommand, int inId)
        {
            object sort = new[] { "" };
            object group = new[] { "" };
            inBnfList.TrimToSize();
            object columns = inBnfList.ToArray();
            var filter = "Computer.ID = ";
            filter += inId.ToString();

            try
            {
                var database = LanDeskDatabase.Get();

                var sql = database.IDal.GetSQL(database.ConnectionString, filter, ref sort, ref group, ref columns);
                var row = database.ExecuteRow(sql);
                if (row != null)
                {
                    var strret = new object[row.ItemArray.Length];
                    for (var k = 0; k < row.ItemArray.Length; k++)
                    {
                        var val = row.ItemArray[k].ToString().Trim();
                        val = FixIpAddress(val);
                        strret[k] = val;
                    }

                    inCommand = string.Format(inCommand, strret);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
            return inCommand;
        }

        private string FixIpAddress(string inIpAddress)
        {
            var tempString = "";
            var bytes = inIpAddress.Split('.');
            if (bytes.Length == 4)
            {
                try
                {
                    for (var i = 0; i < bytes.Length; i++)
                    {
                        var u = Convert.ToByte(bytes[i]);
                        bytes[i] = u.ToString();
                    }
                    for (var i = 0; i < bytes.Length; i++)
                    {
                        tempString += bytes[i];
                        if (i < bytes.Length - 1)
                            tempString += ".";
                    }
                }
                catch (Exception)
                {
                    tempString = inIpAddress;
                }
            }
            else
                tempString = inIpAddress;
            return tempString;
        }
    }
}
