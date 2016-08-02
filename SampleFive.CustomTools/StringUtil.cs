using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleFive.CustomTools
{
    public class StringUtil
    {
        #region Split String And Join Array List

        public static Guid[] ConvertStringArrayToGuidArray(char splitChar, string valuelist)
        {
            return valuelist.Split(splitChar).Select(Guid.Parse).ToArray();
        }

        public static Guid ConvertStringToGuid(string strGuid)
        {
            var newGuid = Guid.Parse(strGuid);
            return newGuid;
        }

        public static Guid[] ConvertStringsToGuids(string[] strGuids)
        {
            return !strGuids.Any() ? null : strGuids.Select(Guid.Parse).ToArray();
        }

        public static string[] SplitString(char splitChar, string valuelist)
        {
            return valuelist.Split(new char[] { splitChar });
        }

        public static string[] SplitString(string valuelist)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t', '\n', '،', '|' };
            return valuelist.Split(delimiterChars);
        }


        public static string JoinSplitString(string splitChar, ArrayList sections)
        {
            var selected = (string[])sections.ToArray(typeof(string));
            return string.Join(splitChar, selected);
        }

        public static string JoinSplitString(string splitChar, string[] sections)
        {
            return string.Join(splitChar, sections);
        }

        public static string JoinSplitString(string splitChar, Guid[] sections)
        {
            return string.Join(splitChar, sections);
        }

        public static string[] ConvertArrayListToStrings(ArrayList sections)
        {
            return (string[])sections.ToArray(typeof(string));
        }

        #endregion

        #region اندازه متن رشته ای
        /// <summary>
        /// اندازه متن رشته ای
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int StringLength(string str)
        {
            var strLen = str.Length;
            return strLen;
        }
        #endregion

        #region  برش بخشی از متن - Substring
        /// <summary>
        /// برش بخشی از متن - Substring
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public string SubStringText(string inputText, int startIndex, int length)
        {
            var strText = inputText;

            if (strText.Length > length)
            {
                return strText.Substring(startIndex, length) + " ... ";
            }
            else
            {
                return strText;
            }
        }
        #endregion
    }
}
