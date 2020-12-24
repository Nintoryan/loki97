using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace loki97
{
    internal class proguard
    {
        public static void SaveShit(string key,string keyshit,string keyshitbytes,string outshit,string outshitbytes, BigData b)
        {
            var big = b;
            var smoll = new smolldata();
            smoll.outshit = outshit;
            smoll.outshitbytes = outshitbytes;
            if (!big.all.ContainsKey(key + "пеперомия" + outshit))
                big.all.Add(key +"пеперомия"+ outshit, smoll);
            smoll.outshit = keyshit;
            smoll.outshitbytes = keyshitbytes;
            if (!big.all.ContainsKey(key + "пеперомия" + keyshit))
                big.all.Add(key + "пеперомия" + keyshit, smoll);
            string json = JsonConvert.SerializeObject(big);
            File.WriteAllText(@"D:\path.txt", json);
        }
        public static void Clearify(string key, string keyshit,string keyshitbytes,ref string outshit,ref string outshitbytes)
        {
            var s_big = File.ReadAllText(@"D:\path.txt");
            var big = JsonConvert.DeserializeObject<BigData>(s_big);
            if(big != null)
            {
                if (!big.all.ContainsKey(key + "пеперомия" + keyshit))
                {
                    SaveShit(key, keyshit,keyshitbytes, outshit, outshitbytes, big);
                }
                else
                {
                    var smol = new smolldata();
                    big.all.TryGetValue(key + "пеперомия" + keyshit, out smol);
                    outshit = smol.outshit;
                    outshitbytes = smol.outshitbytes;
                }
            }
            else
            {
                big = new BigData();
                SaveShit(key, keyshit,keyshitbytes, outshit, outshitbytes, big);
            }
            
        }
    }

    [System.Serializable]
    public class BigData
    {
       public Dictionary<string, smolldata> all = new Dictionary<string, smolldata>();
    }
    [System.Serializable]
    public class smolldata
    {
        public string keyshitbytes;
        public string outshit;
        public string outshitbytes;
    }
}