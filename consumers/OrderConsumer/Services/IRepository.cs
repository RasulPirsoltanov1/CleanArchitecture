﻿namespace OrderConsumer.Services;

public interface IRepository<T> where T : class
{
    Task AddAsync(T entity);
}
