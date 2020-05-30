﻿
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace AutoRest.Mongo
{
    public class StringObjectIdConvention : ConventionBase, IPostProcessingConvention
    {
        public void PostProcess(BsonClassMap classMap)
        {
            var idMap = classMap.IdMemberMap;
            if (idMap != null && idMap.MemberName == "Id" && idMap.MemberType == typeof(string))
            {

                idMap.SetSerializer(new StringSerializer(BsonType.ObjectId));
                idMap.SetIdGenerator(new StringObjectIdGenerator());
            }
        }

    }
}
