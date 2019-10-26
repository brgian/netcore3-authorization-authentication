using AutoMapper;
using NetCore.Template.DTOs;
using NetCore.Template.Entities;
using NetCore.Template.Repositories;
using System;
using System.Collections.Generic;


namespace NetCore.Template.Services.Implementation
{
    public class MyEntityService : IMyEntityService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MyEntityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public MyEntityDto Get(string key)
        {
            var entity = unitOfWork.MyEntityRepository.GetByKey(key);

            return mapper.Map<MyEntityDto>(entity);
        }

        public IEnumerable<MyEntityDto> GetAll()
        {
            var list = unitOfWork.MyEntityRepository.GetAll();

            return mapper.Map<IEnumerable<MyEntityDto>>(list);
        }

        public MyEntityDto Update(MyEntityDto entityDto)
        {
            var currentEntity = unitOfWork.MyEntityRepository.GetByKey(entityDto.Key);
            currentEntity.Value = entityDto.Value;

            var updatedEntity = unitOfWork.MyEntityRepository.Update(currentEntity);
            unitOfWork.Complete();

            return mapper.Map<MyEntityDto>(updatedEntity);
        }

        public MyEntityDto Create(MyEntityDto entityDto)
        {
            var newEntity = new MyEntity();
            newEntity.Value = entityDto.Value;
            newEntity.Key = Guid.NewGuid().ToString();

            var createdEntity = unitOfWork.MyEntityRepository.Add(newEntity);
            unitOfWork.Complete();

            return mapper.Map<MyEntityDto>(createdEntity);
        }

        public void Delete(string key)
        {
            unitOfWork.MyEntityRepository.RemoveByKey(key);
            unitOfWork.Complete();
        }
    }
}