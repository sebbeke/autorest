using System;
using System.Threading.Tasks;
using AutoRest.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace AutoRest.Mongo
{
    public class MongoFileManager : IFileManager
    {
        private readonly MongoConfiguration _configuration;
        private GridFSBucket Bucket { get; set; }

        public MongoFileManager(MongoConfiguration configuration)
        {
            _configuration = configuration;
            MongoClient client;
            if (!string.IsNullOrEmpty(configuration.Username) && !string.IsNullOrEmpty(configuration.Password))
            {
                var credentials = MongoCredential.CreateCredential(configuration.Database, configuration.Username, configuration.Password);
                var settings = new MongoClientSettings()
                {
                    Credential = credentials,
                    Server = MongoServerAddress.Parse(configuration.Server.Replace("mongodb://", ""))
                };
                client = new MongoClient(settings);
            }
            else
            {
                client = new MongoClient(configuration.Server);
            }
            var db = client.GetDatabase(configuration.Database);

            Bucket = new GridFSBucket(db);
        }

        /// <summary>
        /// verwijder een bestand uit gridfs
        /// </summary>
        /// <param name="id">let op waarde van gridfs, niet van een ander document</param>
        public virtual async Task Delete(string id)
        {
            await Bucket.DeleteAsync(new ObjectId(id));
        }

        /// <summary>
        /// download een bestand uit gridfs
        /// </summary>
        /// <param name="bson">let op waarde van gridfs, niet van een ander document</param>
        /// <returns>byte array van gridfs bestand</returns>
        public virtual async Task<byte[]> Download(string id)
        {
            try
            {
                return await Bucket.DownloadAsBytesAsync(new ObjectId(id), new GridFSDownloadOptions
                {
                    CheckMD5 = true
                });
            }
            catch
            {
                return await Task.FromResult(new byte[0]);
            }

        }

        /// <summary>
        /// Upload een file naar mongo gridfs
        /// </summary>
        /// <param name="content">Byte array met inhoud van afbeelding</param>
        /// <param name="filename"></param>
        /// <returns>Bson waarde van Gridfs. Sla dit op om het bestand in de toekomst op te halen.</returns>
        public virtual async Task<string> Upload(byte[] content, string filename)
        {
            return (await Bucket.UploadFromBytesAsync(filename, content)).ToString();
        }
    }
}
