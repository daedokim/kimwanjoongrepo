using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace com.dug.UI.util
{
    public class GameUtil
    {
        public static DateTime executeDateTime = System.DateTime.Now;

        public static int m_LastTick = -1;
        public static int m_LastTickOffset = 0;

        private static bool enableDebugLog = true;

        // 1000000 식으로 숫자 변환
        public static string MakePriceString(double number)
        {
            string str = number.ToString();
            string newStr = "";
            int cnt = 0;

            for (int i = str.Length - 1; i >= 0; i--)
            {
                cnt++;
                if (cnt == 3 && i > 0)
                {
                    newStr = "," + str.Substring(i, 3) + newStr;
                    cnt = 0;
                }
                else if (i == 0)
                {
                    newStr = str.Substring(i, cnt) + newStr;
                }

            }
            return newStr;
        }


        public static string MakeShortPriceFractionString(double num)
        {
            double fractionCheckNum = 0;

            if (num < 10000)
            {
                return MakePriceString(num);
            }
            else if (num < 10000000)
            {
                fractionCheckNum = num / 1000;
                if (fractionCheckNum % 1 > 0)
                {
                    return MakePriceString(num);
                }
                else
                {
                    num = System.Math.Floor(num / 1000);
                    return MakePriceString(num) + "K";
                }
            }
            else if (num < 10000000000)
            {
                fractionCheckNum = num / 1000000;
                if (fractionCheckNum % 1 > 0)
                {
                    num = num / 1000;
                    return MakePriceString(num) + "K";
                }
                else
                {
                    num = System.Math.Floor(num / 1000000);
                    return MakePriceString(num) + "M";
                }
            }
            else
            {
                fractionCheckNum = num / 1000000000;
                if (fractionCheckNum % 1 > 0)
                {
                    num = System.Math.Floor(num / 1000000);
                    return MakePriceString(num) + "M";
                }
                else
                {
                    num = System.Math.Floor(num / 1000000000);
                    return MakePriceString(num) + "B";
                }
            }
        }

        public static void DebugLog(params object[] messages)
        {
            if (enableDebugLog == false)
                return;
            string message = "";

            for (int i = 0; i < messages.Length; i++)
            {
                if (i > 0)
                {
                    message += " ";
                }
                message += messages[i].ToString();
            }

            UnityEngine.Debug.Log(message);
        }

        public static void ResetExecuteTime()
        {
            executeDateTime = System.DateTime.Now;

            m_LastTick = -1;
            m_LastTickOffset = 0;
        }

        public static string ToOrdinal(long number)
        {
            if (number < 0) return number.ToString();
            long rem = number % 100;
            if (rem >= 11 && rem <= 13) string.Format("{0}th", number);
            switch (number % 10)
            {
                case 1:
                    return string.Format("{0}st", number);
                case 2:
                    return string.Format("{0}nd", number);
                case 3:
                    return string.Format("{0}rd", number);
                default:
                    return string.Format("{0}th", number);
            }
        }

        public static string ToOrdinal(int number)
        {
            if (number < 0) return number.ToString();
            int rem = number % 100;
            if (rem >= 11 && rem <= 13) string.Format("{0}th", number);
            switch (number % 10)
            {
                case 1:
                    return string.Format("{0}st", number);
                case 2:
                    return string.Format("{0}nd", number);
                case 3:
                    return string.Format("{0}rd", number);
                default:
                    return string.Format("{0}th", number);
            }
        }

        /// <summary>
		/// Changes the long to number string.
		/// </summary>
		/// <returns>The long to number string.</returns>
		/// <param name="number">Number.</param>
		public static string ChangeNumberToNumberString(long number)
        {
            return number.ToString("#,##0");
        }
        /// <summary>
        /// Changes the long to number string.
        /// </summary>
        /// <returns>The long to number string.</returns>
        /// <param name="number">Number.</param>
        public static string ChangeNumberToNumberString(int number)
        {
            return number.ToString("#,##0");
        }
        public static int SafeRandom()
        {
            System.Random random = new System.Random();

            int temp1 = random.Next();
            int temp2 = random.Next();

            int result = random.Next();

            if (temp1 == temp2)
            {
                result = random.Next() + System.DateTime.Now.Millisecond;
            }
            return result % 1000000;
        }

        public static int RandomRange(int min, int max)
        {
            System.Random random = new System.Random();
            int result = random.Next(min, max);

            return result;
        }

        public static IList<T> ShuffleListObject<T>(IList<T> list)
        {
            for (int i = 0; i < list.Count * 10; i++)
            {
                var temp = list[0];
                int rand = SafeRandom() % list.Count;
                list[0] = list[rand];
                list[rand] = temp;
            }
            return list;
        }

        public static void Shuffle<T>(T[] array)
        {
            System.Random rng = new System.Random();
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }

        public static double GetTimeSecond()
        {
            TimeSpan span = new TimeSpan(System.DateTime.Now.Ticks);
            return span.TotalSeconds;
        }

        // millisecond
        public static int SafeGetTimer()
        {
            TimeSpan span = System.DateTime.Now - executeDateTime;
            return (int)span.TotalMilliseconds;
        }

        // second
        public static int SafeGetTimerSecond()
        {
            TimeSpan span = System.DateTime.Now - executeDateTime;
            return (int)span.TotalSeconds;
        }

        public static int SafeGetTimerNetwork()
        {
            int tick = SafeGetTimer();
            int return_tick;

            if (tick == 0)
            {
                tick = 1; // 0은 없다고 가정		
            }

            if (tick <= m_LastTick)
            {
                m_LastTickOffset += m_LastTick; // 리셋 보정 로직
            }

            return_tick = tick + m_LastTickOffset;
            m_LastTick = tick;

            return return_tick;
        }

        public static string SetTimeString(int ss)
        {
            int _hh = (int)(ss / 3600);
            int _mm = (int)(ss % 3600 / 60);
            int _ss = (int)(ss % 60);

            string timeStr = string.Format("{0:D2}:{1:D2}:{2:D2}", _hh, _mm, _ss);
            return timeStr;
        }

        public static string SetTimeMinuteString(int ss)
        {
            int _hh = (int)(ss / 3600);
            int _mm = (int)(ss % 3600 / 60);
            int _ss = (int)(ss % 60);

            string timeStr = string.Format("{0:D2}:{1:D2}", _mm, _ss);
            return timeStr;
        }

        public static string SetSecondToDayHour(int ss)
        {
            int _dd = (int)(ss / 86400);
            int _hh = (int)(ss / 3600) - _dd * 24;

            string timeStr = string.Format("{0:D2}:{1:D2}", _dd, _hh);
            return timeStr;
        }

        public static DateTime FromServerTime(int serverTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(serverTime);
        }

        public static string GetMonthDate(DateTime _time)
        {
            string str = "";
            switch (_time.Month)
            {
                case 1:
                    str = "Jan";
                    break;

                case 2:
                    str = "Feb";
                    break;

                case 3:
                    str = "Mar";
                    break;

                case 4:
                    str = "Apr";
                    break;

                case 5:
                    str = "May";
                    break;

                case 6:
                    str = "Jun";
                    break;

                case 7:
                    str = "Jul";
                    break;

                case 8:
                    str = "Aug";
                    break;

                case 9:
                    str = "Sep";
                    break;

                case 10:
                    str = "Oct";
                    break;

                case 11:
                    str = "Nov";
                    break;

                case 12:
                    str = "Dec";
                    break;
            }

            return str;
        }

        public static string GetConvertCurrency(int value)
        {
            return string.Format("{0:#,0}", value);
        }

        public static string GetConvertCurrency(float value)
        {
            return string.Format("{0:0.##}", value);
        }

        public static string GetConvertCurrency(long value)
        {
            return string.Format("{0:#,0}", value);
        }

        public static string GetRankText(int rank)
        {
            if (rank > 3 && rank < 21)
            {
                return string.Format("{0}th", rank);
            }
            else
            {
                int value = rank % 10;

                if (value == 1)
                {
                    return string.Format("{0}st", rank);
                }
                else if (value == 2)
                {
                    return string.Format("{0}nd", rank);
                }
                else if (value == 3)
                {
                    return string.Format("{0}rd", rank);
                }
                else
                {
                    return string.Format("{0}th", rank);
                }
            }
        }

        public static string GetRankText(long rank)
        {
            if (rank > 3 && rank < 21)
            {
                return string.Format("{0}th", rank);
            }
            else
            {
                long value = rank % 10;

                if (value == 1)
                {
                    return string.Format("{0}st", rank);
                }
                else if (value == 2)
                {
                    return string.Format("{0}nd", rank);
                }
                else if (value == 3)
                {
                    return string.Format("{0}rd", rank);
                }
                else
                {
                    return string.Format("{0}th", rank);
                }
            }
        }

        private static string GetProperDisplayNameForce(string firstname, string lastname, string nickname, int maxLength)
        {
            int firstLength = 0;
            if (firstname.Length < (maxLength - 2))
            {
                firstLength = firstname.Length;
            }
            else
            {
                firstLength = maxLength - 2;
            }

            return string.Format("{0}.{1}", firstname.Substring(0, firstLength), lastname.Substring(0, 1).ToUpper());
        }

        public static string GetProperDisplayNameByNickname(string nickname, int maxLength = 11)
        {
            if (string.IsNullOrEmpty(nickname))
            {
                return "";
            }

            if (nickname.Length < maxLength)
            {
                maxLength = nickname.Length;
            }

            string result = "";
            string[] nameArray = nickname.Split(' ');

            if (nameArray.Length > 1)
            {
                result = GetProperDisplayNameForce(nameArray[0], nameArray[nameArray.Length - 1], nickname, maxLength);
            }
            else
            {
                string[] nameArray2 = nickname.Split('.');
                string first_tmp = "";

                if (nameArray2.Length > 1)
                {
                    first_tmp = nameArray2[0].ToLower();
                    if (first_tmp.Length > 1)
                    {
                        string middleName = "";
                        if (first_tmp.Length > (maxLength - 3))
                        {
                            middleName = first_tmp.Substring(1, (maxLength - 3));
                        }
                        else
                        {
                            middleName = first_tmp.Substring(1, (first_tmp.Length - 1));
                        }

                        result = string.Format("{0}{1}.{2}", first_tmp.Substring(0, 1).ToUpper(), middleName, nameArray2[1].Substring(0, 1).ToUpper());
                    }
                    else
                    {
                        result = nickname.Substring(0, maxLength);
                    }
                }
                else
                {
                    result = nickname.Substring(0, maxLength);
                }
            }

            return result;
        }

        /// <summary>
        /// 카메라의 비율. 16:10 => 1.6
        /// </summary>
        /// <returns>The screen aspect.</returns>
        /// <param name="camera">Camera.</param>
        public static float GetScreenAspect(Camera camera)
        {
            Vector3 v = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));

            return v.x;
        }

        /// <summary>
        /// 카메라 코너의 트랜스폼 XY 포지션.
        /// </summary>
        /// <returns>The camera corner.</returns>
        /// <param name="camera">Camera.</param>
        public static Vector2 GetCameraCorner(Camera camera)
        {
            //			Vector3 v = camera.ViewportToWorldPoint(new Vector3(1,1,camera.nearClipPlane));
            float screenAspect = GetScreenAspect(camera);
            float standardAspect = 1.6f;

            //1.6 : 1280 = v.x : result.x
            Vector2 size = new Vector2(640, 400);

            // 5 : 6 = 400 : 480
            // 1.3 : 1280  = 1.0 : result.y
            if (screenAspect > standardAspect)
            {
                size.x = size.x * screenAspect * 0.625f;    // x / 1.6 = x * 0.625
            }
            else if (screenAspect < standardAspect)
            {
                size.y = size.x / screenAspect;
            }
            //너비는 가로 비율이 1.6보다 작거나 같으면 640 고정.
            //높이는 가로 비율이 1.6보다 크거나 같으면 400 고정.
            return size;

        }

        public static List<int> StrToListInt(string str, int offset = 0)
        {
            List<int> _result = new List<int>();
            foreach (string _itemStr in str.Split(','))
            {
                _result.Add(int.Parse(_itemStr) + offset);
            }
            return _result;
        }



        public static byte[] StrToByteArray(string readString)
        {
            string[] stringArray = readString.Split(',');
            byte[] intArray = new byte[stringArray.Length];

            for (int i = 0; i < stringArray.Length; i++)
            {
                intArray[i] = byte.Parse(stringArray[i]);
            }

            return intArray;
        }

        public static int[] StrToArrayInt(string readString)
        {
            string[] stringArray = readString.Split(',');
            int[] intArray = new int[stringArray.Length];

            for (int i = 0; i < stringArray.Length; i++)
            {
                intArray[i] = int.Parse(stringArray[i]);
            }

            return intArray;
        }

        public static long GetDirectorySize(string _path)
        {
            long _size = 0;
            DirectoryInfo _dirInfo = new DirectoryInfo(_path);

            if (_dirInfo.Exists == false)
            {
                return _size;
            }

            foreach (DirectoryInfo _directoryInfo in _dirInfo.GetDirectories())
            {
                _size += GetDirectorySize(_directoryInfo.FullName);
            }

            //C:\abc 폴더의 용량을 검사
            foreach (FileInfo _fileInfo in _dirInfo.GetFiles())
            {
                _size += (long)_fileInfo.Length;
            }

            return _size;
        }

        public static long GetFileSize(string _path)
        {
            long _size = 0;
            FileInfo _fileInfo = new FileInfo(_path);

            if (_fileInfo.Exists == false)
            {
                return _size;
            }

            _size = _fileInfo.Length;

            return _size;
        }

        public static void RemoveDirecotry(string _path)
        {
            DirectoryInfo _dirInfo = new DirectoryInfo(_path);

            if (_dirInfo.Exists == false)
            {
                return;
            }

            Directory.Delete(_path, true);
        }

        /// <summary>
        /// Removes the sub direcotry And File.
        /// </summary>
        /// <param name="_path">_path.</param>
        /// <param name="excludePattern">Exclude pattern. | 로 구분, File은 확장자까지, 폴더는 이름만.</param>
        public static void RemoveSubDirecotry(string _path, string excludePattern = "")
        {
            DirectoryInfo _dirInfo = new DirectoryInfo(_path);

            if (_dirInfo.Exists == false)
            {
                return;
            }
            List<string> patterns = new List<string>(excludePattern.Split('|'));

            foreach (DirectoryInfo _subDirectoryInfo in _dirInfo.GetDirectories())
            {
                if (patterns.Contains(_subDirectoryInfo.Name) == false)
                {
                    Directory.Delete(_subDirectoryInfo.FullName, true);
                }
            }

            foreach (FileInfo _fileInfo in _dirInfo.GetFiles())
            {
                if (!patterns.Contains(_fileInfo.Name))
                {
                    File.Delete(_fileInfo.FullName);
                }
            }
        }

        public static void RemoveFile(string _path)
        {
            FileInfo _fileInfo = new FileInfo(_path);

            if (_fileInfo.Exists == false)
            {
                return;
            }

            File.Delete(_path);
        }

        public static bool BetweenInt(int _target, int _min, int _max, bool _includeMin = true, bool _includeMax = true)
        {
            _min = _includeMin ? _min : _min + 1;
            _max = _includeMax ? _max : _max - 1;
            return _target >= _min && _target <= _max;
        }

        public static int GetRandomIntUnder(int n)
        {
            return UnityEngine.Random.Range(0, n);
        }

        public static string GetRemainTimeFormat(long TimeValue, bool isTrim = false, bool isSmartCheck = false, int strBaseHour = 24)
        {
            int hour = (int)TimeValue / (60 * 60);
            int min = (int)TimeValue % 3600 / 60;
            int sec = (int)TimeValue % 60;
            string hourString = "";
            string minString = "";
            string secString = "";

            if (hour > strBaseHour)
            {
                int days = (int)(hour / 24);
                string ret = " Days";
                if (days == 1)
                {
                    ret = " Day";
                }
                return days + ret;
            }
            else
            {
                if (hour < 10)
                {
                    hourString = "0" + hour;
                }
                else
                {
                    hourString = hour.ToString();
                }

                if (min < 10)
                {
                    minString = "0" + min;
                }
                else
                {
                    minString = min.ToString();
                }

                if (sec < 10)
                {
                    secString = "0" + sec;
                }
                else
                {
                    secString = sec.ToString();
                }

                if (isTrim)
                {
                    if (hourString == "00")
                    {
                        return minString + ":" + secString;
                    }
                    else
                    {
                        if (isSmartCheck)
                        {
                            return hourString + ":" + minString;
                        }
                    }
                }
            }
            return hourString + ":" + minString + ":" + secString;
        }

        public static string GetDeviceId()
        {
            return SystemInfo.deviceUniqueIdentifier;
        }
    }

}
