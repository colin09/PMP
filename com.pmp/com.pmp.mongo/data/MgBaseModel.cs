using com.pmp.common.Config;
using MongoDB.Bson;
using System;

namespace com.pmp.mongo.data
{
    public class MgBaseModel
    {
        //public int Id { set; get; }
        public ObjectId _id { get; set; }

        public string StringId
        {
            get
            {
                return _id.ToString();
            }
        }

        public ObjectId CreateObjectId()
        {
            return new ObjectId(Guid.NewGuid().ToString().Replace("-", "").Substring(0, 32).ToLower());
        }
    }



    public static class MgBaseModelExt
    {
        public static string GetCollectionName(this MgBaseModel model)
        {
            return string.Format("{0}_{1}", AppSettingConfig.MgPrefix, model.GetType().Name.ToLower());
        }
    }

}
