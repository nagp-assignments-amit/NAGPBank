using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using NAGPBank.CrossCutting.Dto;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace NAGPBank.Data
{
    public class RedisCacheDb
    {
        /// <summary>
        /// Redis connection
        /// </summary>
        private readonly ConnectionMultiplexer redis;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisCacheDb"/> class.
        /// </summary>
        /// <param name="settings"></param>
        public RedisCacheDb(IOptions<ConfigSettings> settings)
        {
            redis = ConnectionMultiplexer.Connect(settings.Value.RedisCacheConnStr);
        }

        /// <summary>
        /// Add key to redis cache for the specified amount
        /// </summary>
        /// <typeparam name="T">the type of value object</typeparam>
        /// <param name="key">the key</param>
        /// <param name="value">the value to store with key</param>
        public void Add<T>(string key, T value)
        {
            string serializedValue = JsonConvert.SerializeObject(value);
            redis.GetDatabase().StringSet(key, serializedValue);
        }

        /// <summary>
        /// Clear/delete the key
        /// </summary>
        /// <param name="key">key</param>
        public void Clear(string key)
        {
            redis.GetDatabase().KeyDelete(key);
        }

        /// <summary>
        /// Does the key exist on redis?
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>true/false</returns>
        public bool Exists(string key)
        {
            return redis.GetDatabase().KeyExists(key);
        }

        /// <summary>
        /// Returns the value and also returns if found or not
        /// </summary>
        /// <typeparam name="T">The type of return value</typeparam>
        /// <param name="key">the key to fetch with</param>
        /// <param name="value">the output value parameter</param>
        /// <returns>exist true/false</returns>
        public bool Get<T>(string key, out T value)
        {
            var redisValue = redis.GetDatabase().StringGet(key);

            if (redisValue.HasValue)
            {
                value = JsonConvert.DeserializeObject<T>(redisValue.ToString());
            }
            else
            {
                value = default(T);
            }

            return redisValue.HasValue;
        }
    }
}
