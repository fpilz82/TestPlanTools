using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TPTools.Services
{
    public class PatchDataList
    {
        private List<PatchData> _patchDataList = new List<PatchData>();
        List<PatchData> Data { get => _patchDataList; }

        public void Add(PatchData patchData)
        {
            _patchDataList.Add(patchData);
        }

        public void Add(string op, string path, string value)
        {
            _patchDataList.Add(new PatchData
            {
                Op = op,
                Path = path,
                Value = value
            });

        }

        public void Reset()
        {
            _patchDataList = new List<PatchData>();
        }

        public string ToJson()
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            return JsonConvert.SerializeObject(_patchDataList, serializerSettings);
        }
    }

    public class PatchData
    {
        public string Op { get; set; }
        public string Path { get; set; }
        public string Value { get; set; }
    }
}