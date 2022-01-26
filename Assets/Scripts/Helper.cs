

using UnityEngine;

public static class Helper 
{
    public static string SplitString(string str , char chars)
    {
        string[] subs = str.Split(chars);
        return subs[0];
    }
    public static int GetNextIndex(Vector3 vector , Transform[] vectors)
    {
        var result = 0;
        for(int i = 0; i< vectors.Length; i++)
        {
            if(vector == vectors[i].position)
            {
                i++;
                result = i;

            }
        }
        if (result >= vectors.Length) result = vectors.Length - 1;
        return result;
    }
}
