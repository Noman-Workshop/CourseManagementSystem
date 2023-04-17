using AutoMapper;
using DTOs.User;
using Models;

namespace Services.Mappers;

class MapperService : IMapperService {
	private readonly Mapper _mapper;

	public MapperService(MapperConfigurations mapperConfigurations) {
		_mapper = new Mapper(mapperConfigurations.Instance);
	}

	public V Map<T, V>(T data) {
		return _mapper.Map<V>(data);
	}
}
