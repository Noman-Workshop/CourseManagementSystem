using Minio;
using Minio.DataModel;

namespace Services.Storage;

public abstract class IMinioStorageService<T> {
	protected T Client;

	// list buckets
	public abstract Task<List<Bucket>> ListBucketsAsync();

	// list objects
	public abstract Task<List<Item>> ListObjectsAsync(string bucketName);

}
