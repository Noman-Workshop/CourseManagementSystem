namespace Services.Mappers;

public interface IMapperService {
	public V Map<T, V>(T data);
}
