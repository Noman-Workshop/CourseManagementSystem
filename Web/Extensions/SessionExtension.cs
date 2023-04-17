using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace CourseManagementSystem.Extensions {
	public static class SessionExtension {
		public static void Set<T>(this ISession session, string key, T value) {
			session.SetString(key, JsonConvert.SerializeObject(value));
		}

		public static T? Get<T>(this ISession session, string key) {
			var value = session.GetString(key);
			return value == null ? default : JsonConvert.DeserializeObject<T>(value);
		}

		public static async Task Set<T>(this IDistributedCache cache, string key, T value) {
			await cache.SetStringAsync(key, JsonConvert.SerializeObject(value));
		}

		public static async Task<T?> Get<T>(this IDistributedCache cache, string key) {
			var value = await cache.GetStringAsync(key);
			return value == null ? default : JsonConvert.DeserializeObject<T>(value);
		}
	}
}
