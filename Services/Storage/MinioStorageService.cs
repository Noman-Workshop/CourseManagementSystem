using System.Reactive.Linq;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel;
using Minio.Exceptions;

namespace Services.Storage;

class MinioStorageService : IMinioStorageService<MinioClient> {
	public MinioStorageService(IConfiguration configuration) {
		var minioClient = new MinioClient()
			.WithEndpoint(configuration["Minio:Endpoint"])
			.WithCredentials(configuration["Minio:AccessKey"], configuration["Minio:SecretKey"])
			.Build();
		Client = minioClient;
	}

	public override async Task<List<Bucket>> ListBucketsAsync() {
		var buckets = await Client.ListBucketsAsync();
		return buckets.Buckets;
	}

	public override async Task<List<Item>> ListObjectsAsync(string bucketName) {
		List<Item> items = new List<Item>();
		try {
			var bucketExistsArgs = new BucketExistsArgs()
				.WithBucket(bucketName);
			bool found = await Client.BucketExistsAsync(bucketExistsArgs);
			if (found) {
				// List objects from 'my-bucketname'
				var args = new ListObjectsArgs()
					.WithBucket("mybucket")
					.WithPrefix("prefix")
					.WithRecursive(true);
				IObservable<Item> observable = Client.ListObjectsAsync(args);
				IDisposable subscription = observable.Subscribe(
					item => {
						items.Add(item);
						Console.WriteLine("OnNext: {0}", item.Key);
					},
					ex => Console.WriteLine("OnError: {0}", ex.Message),
					() => Console.WriteLine("OnComplete: {0}"));
			}
			else {
				Console.WriteLine("mybucket does not exist");
			}
		} catch (MinioException e) {
			Console.WriteLine("Error occurred: " + e);
		}

		return items;
	}
}
