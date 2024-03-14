using Funcionarios.Model;

namespace Funcionarios.Interface;

public interface ICargoService
{
    public Task<IEnumerable<Cargo>> GetAll();
    public Task<Cargo> Get(int codigo);
    public Task Save(Cargo funcionario);
    public Task Update(Cargo funcionario);
    public Task Delete(int codigo);
}