using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SimpleJSON;

public static class Tools
{

    public static GameObject LoadResourcesGameObject(string path)
    {
        //Debug.Log("LoadResourceGameObject:"+path);
        UnityEngine.Object obj = Resources.Load(path);
        if (obj == null)
        {
            Debug.LogError("LoadResourcesGameObject Error!;path:" + path);
            return null;
        }
        return path != null ? UnityEngine.Object.Instantiate(obj) as GameObject : null;
    }

    public static GameObject LoadResourcesGameObject(string path, GameObject gobjParent, float x, float y, float z)
    {
        GameObject gobj = null;
        gobj = LoadResourcesGameObject(path);
        if (gobj != null)
        {
            gobj.transform.parent = gobjParent.transform;
            gobj.transform.localPosition = new Vector3(x, y, z);
        }
        return gobj;
    }

    public static GameObject LoadResourcesGameObject(string path, GameObject gobjParent)
    {
        GameObject gobj = null;
        gobj = LoadResourcesGameObject(path);
        if (gobj != null)
        {
            gobj.transform.parent = gobjParent.transform;
        }
        return gobj;
    }

    //	public static string Md5Sum(string strToEncrypt)
    //	{
    //		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
    //		byte[] bytes = ue.GetBytes(strToEncrypt);
    //	 
    //		// encrypt bytes
    //		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
    //		byte[] hashBytes = md5.ComputeHash(bytes);
    //	 
    //		// Convert the encrypted bytes back to a string (base 16)
    //		string hashString = "";
    //	 
    //		for (int i = 0; i < hashBytes.Length; i++)
    //		{
    //			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
    //		}
    //	 
    //		return hashString.PadLeft(32, '0');
    //	}

    public static string GetGUID()
    {
        System.Guid myGUID = System.Guid.NewGuid();
        //Debug.Log("myGUID:" + myGUID.ToString("N"));

        return myGUID.ToString("N");
    }

    public static string[] StringSplit(string txt, string character)
    {
        return Regex.Split(txt, character, RegexOptions.IgnoreCase);
    }

    public static string FormatTime(int seconds)
    {
        string strTime;
        if (seconds < 0)
        {
            strTime = "00:00:00";
        }
        else
        {
            int intH = seconds / 3600;
            string strH = intH < 10 ? "0" + intH.ToString() : intH.ToString();
            int intM = (seconds % 3600) / 60;
            string strM = intM < 10 ? "0" + intM.ToString() : intM.ToString();
            int intS = seconds % 3600 % 60;
            string strS = intS < 10 ? "0" + intS.ToString() : intS.ToString();

            if (intH == 0)
            {
                strTime = strM + ":" + strS;
            }
            else
            {
                strTime = strH + ":" + strM + ":" + strS;
            }

        }
        return strTime;
    }

    //秒转时间（1970后）
    public static string TimeStampToString(int time)
    {
        long ltime = (long)time;
        DateTime t = new DateTime(time * TimeSpan.TicksPerSecond);
        t = t.ToLocalTime();
        int year = t.Year + 1969;
        string syear = "" + year;
        int month = t.Month;
        string smonth = month < 10 ? "0" + month : "" + month;
        int day = t.Day;
        string sday = day < 10 ? "0" + day : "" + day;
        int hour = t.Hour;
        string shour = hour < 10 ? "0" + hour : "" + hour;
        int minute = t.Minute;
        string sminute = minute < 10 ? "0" + minute : "" + minute;
        int second = t.Second;
        string ssecond = second < 10 ? "0" + second : "" + second;

        return syear + "/" + smonth + "/" + sday + " " + shour + ":" + sminute + ":" + ssecond;
    }

    public static int MathfRound(float num)
    {
        return (int)Mathf.Floor(num + 0.5f);
    }

    public static int CashToDiamon(int cash)
    {
        int rate = 10;
        return (int)Mathf.Ceil(cash * 1.0f / rate);
    }

    public static int GetMax(int[] nums)
    {
        int r = 0;
        if (nums == null || nums.Length == 0)
        {
            return r;
        }
        r = nums[0];
        foreach (int item in nums)
        {
            if (item > r)
            {
                r = item;
            }
        }
        return r;
    }

    public static T GetComponentInChildByPath<T>(GameObject gobjParent, string path) where T : Component
    {
        Component r = null;
        if (gobjParent == null)
        {
            return null;
        }
        //		Transform tf = GetTransformInChildByPath(gobjParent, path);
        Transform tf = GetTransformInChildByPathSimple(gobjParent, path);
        if (tf != null)
        {
            r = tf.gameObject.GetComponent<T>();
        }
        else
        {
            Debug.Log("!!!Can't get transform by path: " + path + " in GetComponentInChildByPath");
        }
        return r as T;
    }

    // path , split by "/"
    public static Transform GetTransformInChildByPath(GameObject gobjParent, string path)
    {
        Transform r = null;
        if (gobjParent == null)
        {
            return r;
        }
        string[] strArr = path.Split('/');
        r = gobjParent.transform;
        foreach (string strPathItem in strArr)
        {
            if (r == null)
            {
                continue;
            }
            r = r.FindChild(strPathItem);
        }
        if (r == null)
        {
            Debug.Log("!!!Can't get transform by path : " + path + ". in Tools.GetTransformInChildByPath");
        }
        return r;
    }

    public static bool DestoreAllChildren(GameObject gobjParent)
    {
        Transform t = null;
        t = gobjParent.transform;
        if (t = null)
            return false;
        foreach (Transform child in gobjParent.transform)
        {
            NGUIDebug.DestroyObject(child.gameObject);
        }

        return true;
    }

    public static Transform GetTransformInChildByPathSimple(GameObject gobjParent, string path)
    {
        Transform r = null;
        if (gobjParent != null)
        {
            r = gobjParent.transform.FindChild(path);
        }
        return r;
    }

    public static GameObject GetGameObjectInChildByPathSimple(GameObject gobjParent, string path)
    {
        if (gobjParent == null)
        {
            Debug.LogError("Error.Parent Is Null");
            return null;
        }
        GameObject gobj = null;
        Transform tf = GetTransformInChildByPathSimple(gobjParent, path);
        if (tf != null)
        {
            gobj = tf.gameObject;
        }
        return gobj;
    }

    public static int GetIntLength(int i)
    {
        int length = 0;
        length = i.ToString().Length;
        return length;
    }

    public static void ChangeLayersRecursively(Transform trans, string name)
    {
        try
        {
            trans.gameObject.layer = LayerMask.NameToLayer(name);
            foreach (Transform child in trans)
            {
                child.gameObject.layer = LayerMask.NameToLayer(name);
                ChangeLayersRecursively(child, name);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }
    }

    public static void ChangeLayersRecursively(GameObject gobj, string name)
    {
        try
        {

            gobj.layer = LayerMask.NameToLayer(name);
            foreach (Transform child in gobj.transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer(name);
                ChangeLayersRecursively(child, name);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }
    }

    public static int FindIntValInArr(int[] arr, int ivalue)
    {
        int r = -1;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == ivalue)
            {
                r = i;
                break;
            }
        }
        return r;
    }

    public static string FloatToPercent(float fVal)
    {
        string per = "";
        per = Mathf.Round(fVal * 100).ToString() + "%";
        return per;
    }

    public static Hashtable Hash(params object[] args)
    {
        Hashtable hashTable = new Hashtable(args.Length / 2);
        if (args.Length % 2 != 0)
        {
            return null;
        }
        else
        {
            int i = 0;
            while (i < args.Length - 1)
            {
                hashTable.Add(args[i], args[i + 1]);
                i += 2;
            }
            return hashTable;
        }
    }

    public static void SetGameObjMaterial(GameObject gobj, string materialName)
    {
        Material changMaterial = UnityEngine.Object.Instantiate(Resources.Load("Materials/" + materialName)) as Material;
        if (changMaterial != null)
        {
            gobj.renderer.material = changMaterial;
        }
        else
        {
            Debug.LogError("can't find material:" + materialName);
        }
    }
    /// <summary>
    /// Instantiate an object by path and add it to the specified parent.
    /// </summary>
    public static GameObject AddNGUIChild(GameObject gobjParent, string path)
    {
        GameObject r = null;
        GameObject gobjPrefab = Resources.Load(path) as GameObject;
        if (gobjParent != null)
        {
            if (gobjPrefab != null)
            {
                r = NGUITools.AddChild(gobjParent, gobjPrefab);
            }
            else
            {
                Debug.LogError("Error In AddNGUIChild:无法加载预制:" + path);
            }
        }
        else
        {
            Debug.LogError("Error In AddNGUIChild:gobjParent is null!");
        }
        return r;
    }

    public static string Num2RomanTxt(int n)
    {
        int[] arabic = new int[13];
        string[] roman = new string[13];
        int i = 0;
        string o = "";

        arabic[0] = 1000;
        arabic[1] = 900;
        arabic[2] = 500;
        arabic[3] = 400;
        arabic[4] = 100;
        arabic[5] = 90;
        arabic[6] = 50;
        arabic[7] = 40;
        arabic[8] = 10;
        arabic[9] = 9;
        arabic[10] = 5;
        arabic[11] = 4;
        arabic[12] = 1;

        roman[0] = "M";
        roman[1] = "CM";
        roman[2] = "D";
        roman[3] = "CD";
        roman[4] = "C";
        roman[5] = "XC";
        roman[6] = "L";
        roman[7] = "XL";
        roman[8] = "X";
        roman[9] = "IX";
        roman[10] = "V";
        roman[11] = "IV";
        roman[12] = "I";

        while (n > 0)
        {
            while (n >= arabic[i])
            {
                n = n - arabic[i];
                o = o + roman[i];
            }
            i++;
        }
        return o;
    }

    public static void SendCommand(ECommand mCommand, JSONNode mData)
    {
        if (mData == null)
        {
            GameNetEngine.SendCommand((int)mCommand, null);
        }
        else
            GameNetEngine.SendCommand((int)mCommand, mData.ToString());
    }
    public static float StrToFloat(object FloatString)
    {
        float result;
        if (FloatString != null)
        {
            if (float.TryParse(FloatString.ToString(), out result))
                return result;
            else
            {
                return (float)0.00;
            }
        }
        else
        {
            return (float)0.00;
        }
    }

    public static void SetUIPosBy3DGameObj(GameObject gobj2d, GameObject gobj3d, Camera camer3d, Camera camera2d, Vector3 offset)
    {
        Vector3 v1 = camer3d.WorldToViewportPoint(gobj3d.transform.position);
        Vector3 v2 = camera2d.ViewportToWorldPoint(v1);
        v2.z = 0;
        gobj2d.transform.position = v2 + offset;
    }

    public static Vector3 GetUIPosBy3DGameObj(GameObject gobj3d, Camera camer3d, Camera camera2d, Vector3 offset)
    {
        Vector3 v1 = camer3d.WorldToViewportPoint(gobj3d.transform.position);
        Vector3 v2 = camera2d.ViewportToWorldPoint(v1);
        v2.z = 0;
        return v2 + offset;
    }

    public static Vector3 GetUIPosBy3DGameObj(GameObject gobj3d, Camera camer3d, Camera camera2d)
    {
        return GetUIPosBy3DGameObj(gobj3d, camer3d, camera2d, Vector3.zero);
    }
    public static bool IsTouchLayer(Camera cameraSeeTheLayer, string layer)
    {
        bool r = false;
        string strLayer = "";
        Vector3 posMouse = Input.mousePosition;
        posMouse.z = 10;

        Ray ray = cameraSeeTheLayer.ScreenPointToRay(posMouse);

        RaycastHit[] rhs;
        rhs = Physics.RaycastAll(ray);
        if (rhs != null)
        {
            foreach (RaycastHit rh in rhs)
            {
                GameObject gobjHit = rh.collider.gameObject;
                if (gobjHit != null)
                {
                    strLayer += LayerMask.LayerToName(gobjHit.layer);
                }
            }
        }

        if (!string.IsNullOrEmpty(strLayer))
        {
            if (strLayer.Contains(layer))
            {
                r = true;
            }
        }
        return r;
    }

    public static Collider GetColliderInDirection(GameObject ori, Vector3 direction, float distance, string layermaskname)
    {
        Collider c = null;
        Debug.DrawRay(ori.transform.position, direction);
        RaycastHit[] rhs = Physics.RaycastAll(ori.transform.position, direction, distance, 1 << LayerMask.NameToLayer(layermaskname));
        float minDistance = 1000;
        foreach (RaycastHit rh in rhs)
        {
            if (rh.distance < minDistance)
            {
                minDistance = rh.distance;
                c = rh.collider;
            }
        }
        return c;
    }
    /// <summary>
    /// 获取多物体边界
    /// </summary>
    /// <param name="gobjs"></param>
    /// <returns></returns>
    public static Bounds GetBoundsOfMultiGobj(GameObject[] gobjs)
    {
        GameObject gobjOri = gobjs[0];
        Bounds boundOri = gobjOri.renderer.bounds;
        foreach (GameObject gobjTemp in gobjs)
        {
            boundOri.Encapsulate(gobjTemp.renderer.bounds);
        }
        return boundOri;
    }

    /// <summary>
    /// 将字符串分割成float数组。以"_"分割
    /// </summary>
    /// <param name="strFloats"></param>
    /// <returns></returns>
    public static float[] stringToFloats(string strFloats)
    {
        string[] strs = strFloats.Split('_');
        float[] f = new float[strs.Length];
        for (int i = 0; i < strs.Length; i++)
        {
            string str = strs[i];
            if (string.IsNullOrEmpty(str))
            {
                Debug.LogError("Error:stringToFloats");
            }
            f[i] = float.Parse(str);
        }
        return f;
    }

    /// <summary>
    /// 判断输入的字符串是否全是英文（不区分大小写）
    /// </summary>
    /// <param name="objString">所要匹配的字符串</param>
    /// <returns>返回真假值，true：匹配；false：不匹配</returns>
    public static bool isEnglishString(String objString)
    {
        Regex myReg = new Regex(@"^[a-zA-Z]+$");
        return myReg.IsMatch(objString);
    }


    public static void ActiveBoxCollider(GameObject gobj, bool active)
    {
        if (gobj == null)
        {
            return;
        }

        Collider[] cs = gobj.GetComponentsInChildren<Collider>();
        for (int i = 0; i < cs.Length; i++)
        {
            cs[i].enabled = active;
        }
    }

    public static void AddDataCache(GameObject gobj, object data)
    {
        DataCache dc = gobj.AddComponent<DataCache>();
        dc.data = data;
    }

    public static object GetDataCache(GameObject gobj)
    {
        object data = gobj.GetComponent<DataCache>().data;
        return data;
    }

    public static void RepositionGrid(GameObject gobj)
    {
        UIGrid uiGrid = gobj.GetComponent<UIGrid>();
        if (uiGrid != null)
        {
            uiGrid.Reposition();
        }
        else
        {
            Debug.LogError("Error In RepositionGrid:UIGrid is null");
        }
    }

    public static Color CreateColor(int r, int g, int b)
    {
        return new Color(r / 255f, g / 255f, b / 255f);
    }

    /// <summary>
    /// 添加适应物体大小的碰撞盒
    /// </summary>
    /// <param name="tfItem"></param>
    public static void AddGobjCollider(Transform tfItem)
    {
        BoxCollider bc = tfItem.gameObject.AddComponent<BoxCollider>();
        Renderer render = tfItem.GetComponentInChildren<Renderer>();
        Bounds bounds = render.bounds;
        bc.size = bounds.size;
        bc.center = new Vector3(0f, bc.size.y / 2f, 0f);
    }

  // Turn on the bit using an OR operation:
    public static void ShowCameraCullingMask(Camera camera, string someLayer)
    {
        camera.cullingMask |= 1 << LayerMask.NameToLayer(someLayer);
    }

    // Turn off the bit using an AND operation with the complement of the shifted int:
    public static void HideCameraCullingMask(Camera camera, string someLayer)
    {
        camera.cullingMask &= ~(1 << LayerMask.NameToLayer(someLayer));
    }

    // Toggle the bit using a XOR operation:
    public static void ToggleCameraCullingMask(Camera camera, string someLayer)
    {
        camera.cullingMask ^= 1 << LayerMask.NameToLayer(someLayer);
    }

	  public static void SetGobjPosX(GameObject gobj, float x, bool local = false) 
    {
        Vector3 pos = Vector3.zero;
        if (local)
        {
            pos = gobj.transform.localPosition;
            pos.x = x;
            gobj.transform.localPosition = pos;
        }
        else
        {
            pos = gobj.transform.position;
            pos.x = x;
            gobj.transform.position = pos;
        }
    }
    public static void SetGobjPosY(GameObject gobj, float y, bool local = false)
    {
        Vector3 pos = Vector3.zero;
        if (local)
        {
            pos = gobj.transform.localPosition;
            pos.y = y;
            gobj.transform.localPosition = pos;
        }
        else
        {
            pos = gobj.transform.position;
            pos.y = y;
            gobj.transform.position = pos;
        }
    }

    public static void SetGobjPosZ(GameObject gobj, float z, bool local = false)
    {
        Vector3 pos = Vector3.zero;
        if (local)
        {
            pos = gobj.transform.localPosition;
            pos.z = z;
            gobj.transform.localPosition = pos;
        }
        else
        {
            pos = gobj.transform.position;
            pos.z = z;
            gobj.transform.position = pos;
        }
    }

    /// <summary>
    /// 通过名字查找后代物体
    /// </summary>
    /// <param name="gobj"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static GameObject GetDescendantGobjByName(GameObject gobj, string name)
    {
        GameObject gobjToGet = null;
        Transform[] trans = gobj.GetComponentsInChildren<Transform>();
        for (int i = 0; i < trans.Length; i++)
        {
            Transform tf = trans[i];
            if (tf.name.Equals(name))
            {
                gobjToGet = tf.gameObject;
                break;
            }
        }
        if (gobjToGet == null)
        {
            Debug.LogWarning("查找后代物体失败:" + name);
        }
        return gobjToGet;
    }

    /// <summary>
    /// 取两个数的最大公约数
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int GetFactor(int a, int b)
    {
        if (a < b) { int t = a; a = b; b = t; }
        while (b > 0)
        {
            int t = a % b;
            a = b;
            b = t;
         }
        return a;
    }

    /// <summary>
    /// 不改的精灵的原始比例的前提下调整其宽度
    /// </summary>
    /// <param name="sprite"></param>
    /// <param name="width"></param>
    public static void MakeSpritePerfectByWidth(UISprite sprite, int width) 
    {
        UISpriteData usd = sprite.GetAtlasSprite();
        int perfectWidth = usd.width;
        int perfectHeight = usd.height;
        sprite.width = width;
        sprite.height = Mathf.RoundToInt((float)perfectHeight / perfectWidth * width);
    }
}

public class BtnAction
{
    public string btn_head { get; set; }
    public int state { get; set; }
    public string request { get; set; }
}